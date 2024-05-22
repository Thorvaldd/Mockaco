namespace Mockaco.Templating.Generating.Providers
{
    internal interface IGeneratedTemplateProvider
    {
        Task<IEnumerable<GeneratedTemplate>> GetTemplatesAsync(Stream sourceStream, CancellationToken cancellationToken = default);
    }
}