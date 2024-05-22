namespace Mockaco.Templating.Generating.Source
{
    internal class LocalFileContentProvider : ISourceContentProvider
    {
        public Task<Stream> GetStreamAsync(Uri sourceUri, CancellationToken cancellationToken)
        {
            return Task.FromResult((Stream)File.OpenRead(sourceUri.LocalPath));
        }
    }
}
