using System.Reactive.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Mockaco.Templating.Models;
using Mockako.DAL.Context;
using Mockako.DAL.Entities;


namespace Mockaco.Templating.Providers.DatabaseProvider;

public class MockakoDatabaseTemplateProvider<TKey> : ITemplateProvider where TKey : IEquatable<TKey>
{
    public event EventHandler OnChange;
    private readonly IMemoryCache _memoryCache;
    private ILogger<MockakoDatabaseTemplateProvider<TKey>> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly MockakoDatabaseTemplateProviderOptions _options;
    
    private readonly string _cacheKey = "_Mockaco_database_mock_provider";

    private CancellationTokenSource _resetCacheToken = new ();


    public MockakoDatabaseTemplateProvider(IMemoryCache memoryCache,
        ILogger<MockakoDatabaseTemplateProvider<TKey>> logger,
        IServiceProvider serviceProvider,
        MockakoDatabaseTemplateProviderOptions options)
    {
        _memoryCache = memoryCache;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _options = options;


        Observable.Interval(TimeSpan.FromSeconds(30))
            .Subscribe(_ => { IsUpdatesAppeared(); });
    }
    
    public IEnumerable<IRawTemplate> GetTemplates()
    {
        return _memoryCache
            .GetOrCreate(
                _cacheKey,
                ci =>
                {
                    ci.RegisterPostEvictionCallback(PostEvictionCallback);
                    ci.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));

                    return LoadTemplatesFromDatabase();
                });
    }
    

    #region Helper methods

    private void ClearCache()
    {
        if (_resetCacheToken?.IsCancellationRequested == false && _resetCacheToken.Token.CanBeCanceled)
        {
            _resetCacheToken.Cancel();
            _resetCacheToken.Dispose();
        }

        _resetCacheToken = new CancellationTokenSource();
    }
    
    private void PostEvictionCallback(object key, object value, EvictionReason reason, object state)
    {
        _logger.LogDebug("Mock files cache invalidated because of {reason}", reason);
    }
    
    private void IsUpdatesAppeared()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        MockakoDatabaseContext<TKey> databaseContext = scope.ServiceProvider.GetRequiredService<MockakoDatabaseContext<TKey>>();
        
        List<MockakoRestConfig<TKey>> templates = databaseContext.MockakoRestConfigs
            .Where(x => x.IsActive)
            .ToList();

        foreach (MockakoRestConfig<TKey> template in templates)
        {
            DateTime? lastUpdate = _memoryCache.Get<DateTime?>($"{_cacheKey}_{template.Id}");

            if (!lastUpdate.HasValue) //new config in db
            {
                ClearCache();
                GetTemplates();
                
                OnChange?.Invoke(this, EventArgs.Empty);
                break;
            }

            if (template.ModifiedDateTime != lastUpdate)
            {
                ClearCache();
                GetTemplates();
                
                OnChange?.Invoke(this, EventArgs.Empty);
                break;
            }
        }
    }

    private IEnumerable<IRawTemplate> LoadTemplatesFromDatabase()
    {
        List<RawTemplate> rawTemplates = new();

        try
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            MockakoDatabaseContext<TKey> databaseContext = scope.ServiceProvider.GetRequiredService<MockakoDatabaseContext<TKey>>();
            
            List<MockakoRestConfig<TKey>> tmpls =  databaseContext.MockakoRestConfigs
                // TODO filter by the application type
                .Where(x=>x.IsActive && x.ApplicationId == _options.ApplicationId)
                .ToList();
            
            tmpls.ForEach(x =>
            {
                try
                {
                    _memoryCache.Remove($"{_cacheKey}_{x.Id}");
                    _memoryCache.GetOrCreate($"{_cacheKey}_{x.Id}", f => x.ModifiedDateTime);
                    
                    rawTemplates.Add(new RawTemplate(x.Id.ToString(), x.Config));
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Unable to load template {id}", x.Id);
                }
            });

            return rawTemplates;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "");
        }

        return Enumerable.Empty<IRawTemplate>();
    }

    #endregion
}
