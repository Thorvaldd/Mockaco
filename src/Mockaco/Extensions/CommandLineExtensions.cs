using System.CommandLine.Parsing;

namespace Mockaco.Extensions
{
    public static class CommandLineExtensions
    {
        public static bool IsUsingCommand(this Parser commandLine, string[] args)
        {
            var parseResult = commandLine.Parse(args);

            return parseResult.CommandResult != parseResult.RootCommandResult;
        }
    }
}
