using Mockaco.Templating.Models;

namespace Mockaco.Templating.Providers
{
    public interface ITemplateProvider
    {
        event EventHandler OnChange;

        IEnumerable<IRawTemplate> GetTemplates();

        Task<IEnumerable<IRawTemplate>> GetTemplatesAsync();
    }
}
