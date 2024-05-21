namespace Mockaco.Templating.Models
{
    public interface IRawTemplate
    {
        string Content { get; }

        string Name { get; }

        string Hash { get; }
    }
}