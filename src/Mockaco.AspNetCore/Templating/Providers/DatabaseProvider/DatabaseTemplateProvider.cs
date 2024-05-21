using System.Reactive.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Mockaco.Templating.Models;
using Mockaco.Templating.Providers.DatabaseProvider.Context;

namespace Mockaco.Templating.Providers.DatabaseProvider;

public class DatabaseTemplateProvider<TKey> : ITemplateProvider, IDisposable where TKey : IEquatable<TKey>
{
    public event EventHandler OnChange;
    private readonly IMemoryCache _memoryCache;
    private ILogger<DatabaseTemplateProvider<TKey>> _logger;
    private readonly MockakoDatabaseContext<TKey> _databaseContext;
    
    private readonly string _cacheKey = "_Mockaco_database_mock_provider";

    private CancellationTokenSource _resetCacheToken = new ();


    public DatabaseTemplateProvider(IMemoryCache memoryCache,
        ILogger<DatabaseTemplateProvider<TKey>> logger,
        MockakoDatabaseContext<TKey> databaseContext)
    {
        _memoryCache = memoryCache;
        _logger = logger;
        _databaseContext = databaseContext;


        Observable.Interval(TimeSpan.FromSeconds(30))
            .Subscribe(_ => { IsUpdatesAppeared(); });
    }

    public void IsUpdatesAppeared()
    {
        var templates = _databaseContext.MockakoRestConfigs
            .Where(x => x.IsActive)
            .ToList();

        foreach (var template in templates)
        {
            var lastUpdate = _memoryCache.Get<DateTime?>($"{_cacheKey}_{template.Id}");

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
        throw new NotImplementedException();
    }

    private async Task<IEnumerable<IRawTemplate>> LoadTemplatesFromDatabaseAsync()
    {
        List<RawTemplate> rawTemplates = new();

        try
        {
            var tmpls = await _databaseContext.MockakoRestConfigs
                // TODO filter by the application type
                //.Where(x=>x.IsActive && x.AppId == _)
                .ToListAsync();
            
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

    public IEnumerable<IRawTemplate> GetTemplates()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IRawTemplate>> GetTemplatesAsync()
    {
        return _memoryCache
            .GetOrCreateAsync(
                _cacheKey,
                async ci =>
                {
                    ci.RegisterPostEvictionCallback(PostEvictionCallback);
                    ci.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));

                    return await LoadTemplatesFromDatabaseAsync();
                });
    }



    public void Dispose()
    {
        _databaseContext.Dispose();
    }
}