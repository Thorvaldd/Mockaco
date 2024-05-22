using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace Mockaco.Templating.Generating.Cli
{
    internal class GeneratorRunner
    {
        private readonly IServiceProvider _serviceProvider;

        public GeneratorRunner(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<int> ExecuteAsync(GeneratingOptions options, IConsole console, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<TemplatesGenerator>().GenerateAsync(options, cancellationToken);
            return 0;
        }
    }
}