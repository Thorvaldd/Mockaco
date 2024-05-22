namespace Mockaco.Templating.Generating.Source
{
    internal class HttpContentProvider : ISourceContentProvider
    {
        public async Task<Stream> GetStreamAsync(Uri sourceUri, CancellationToken cancellationToken)
        {
            return await new HttpClient().GetStreamAsync(sourceUri, cancellationToken);
        }
    }
}