using System.Collections.Generic;
using Mockaco.Templating.Models;
using Mockaco.Templating.Scripting;

namespace Mockaco
{
    internal interface IMockacoContext
    {
        IScriptContext ScriptContext { get; }

        Template TransformedTemplate { get; set; }

        Mock Mock { get; set; }

        List<Error> Errors { get; set; }
    }
}