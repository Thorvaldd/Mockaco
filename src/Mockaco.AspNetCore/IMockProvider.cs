using Mockaco.Templating.Models;

namespace Mockaco
{
    internal interface IMockProvider
    {
        List<Mock> GetMocks();

        IEnumerable<(string TemplateName, string ErrorMessage)> GetErrors();

        Task WarmUp();
    }
}