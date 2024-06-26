namespace Mockaco.Templating.Generating.Store
{
    internal interface IGeneratedTemplateStore
    {
        Task SaveAsync(GeneratedTemplate template, CancellationToken cancellationToken = default);
        
        Task SaveAsync(IEnumerable<GeneratedTemplate> templates, CancellationToken cancellationToken = default);
    }
}