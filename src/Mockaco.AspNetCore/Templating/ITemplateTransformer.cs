using Mockaco.Templating.Models;
using Mockaco.Templating.Scripting;

namespace Mockaco.Templating
{
    internal interface ITemplateTransformer
    {
        //TODO Improve this abstraction
        Task<Template> TransformAndSetVariables(IRawTemplate rawTemplate, IScriptContext scriptContext);

        Task<Template> Transform(IRawTemplate rawTemplate, IScriptContext scriptContext);
    }
}