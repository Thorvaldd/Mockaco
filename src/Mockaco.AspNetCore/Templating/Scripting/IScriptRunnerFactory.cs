using Microsoft.CodeAnalysis.Scripting;

namespace Mockaco.Templating.Scripting
{
    internal interface IScriptRunnerFactory
    {
        ScriptRunner<TResult> CreateRunner<TContext, TResult>(string code);

        Task<TResult> Invoke<TContext, TResult>(TContext context, string code);
    }
}