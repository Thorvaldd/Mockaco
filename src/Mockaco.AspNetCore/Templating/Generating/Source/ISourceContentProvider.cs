namespace Mockaco.Templating.Generating.Source
{
    internal interface ISourceContentProvider
    {
        Task<Stream> GetStreamAsync(Uri sourceUri, CancellationToken cancellationToken);
    }
}
