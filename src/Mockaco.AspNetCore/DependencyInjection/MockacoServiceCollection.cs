﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mockaco.HealthChecks;
using Mockaco.Options;
using Mockaco.Settings;
using Mockaco.Templating;
using Mockaco.Templating.Generating;
using Mockaco.Templating.Request;
using Mockaco.Templating.Response;
using Mockaco.Templating.Scripting;
using Mockaco.WarmUps;

namespace Mockaco.DependencyInjection
{
    public static class MockacoServiceCollection
    {
        public static IServiceCollection AddMockaco(this IServiceCollection services) =>
            services.AddMockaco(_ => { });

        public static IServiceCollection AddMockaco(this IServiceCollection services, Action<MockacoOptions> config) =>
            services
                .AddOptions<MockacoOptions>().Configure(config).Services
                .AddOptions<TemplateFileProviderOptions>()
                    .Configure<IOptions<MockacoOptions>>((options, parent) => options = parent.Value.TemplateFileProvider)
                    .Services
                .AddCommonServices();


        public static IServiceCollection AddMockaco(this IServiceCollection services, IConfiguration config) =>
            services
                .AddConfiguration(config)
                .AddCommonServices();

        private static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config) =>
            services
                .AddOptions()
                .Configure<MockacoOptions>(config)
                .Configure<TemplateFileProviderOptions>(config.GetSection("TemplateFileProvider"));

        private static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services
                .AddMemoryCache()
                .AddHttpClient()
                .AddInternalServices()
                .AddHostedService<MockProviderWarmUp>();

            services
                .AddSingleton<StartupHealthCheck>()
                .AddHealthChecks()
                    .AddCheck<StartupHealthCheck>("Startup", tags: new[] { "ready" });

            return services;
        }

        private static IServiceCollection AddInternalServices(this IServiceCollection services) =>
            services
                .AddSingleton<VerificationRouteValueTransformer>()
                .AddScoped<IMockacoContext, MockacoContext>()
                .AddScoped<IScriptContext, ScriptContext>()
                .AddTransient<IGlobalVariableStorage, ScriptContextGlobalVariableStorage>()

                .AddSingleton<IScriptRunnerFactory, ScriptRunnerFactory>()

                .AddSingleton<IFakerFactory, LocalizedFakerFactory>()
                .AddSingleton<IMockProvider, MockProvider>()
                //.AddSingleton<ITemplateProvider, TemplateFileProvider>()
              //  .AddSingleton<ITemplateProvider, DatabaseTemplateProvider<>>()

                .AddScoped<IRequestMatcher, RequestMethodMatcher>()
                .AddScoped<IRequestMatcher, RequestRouteMatcher>()
                .AddScoped<IRequestMatcher, RequestConditionMatcher>()

                .AddTransient<IRequestBodyFactory, RequestBodyFactory>()

                .AddTransient<IRequestBodyStrategy, JsonRequestBodyStrategy>()
                .AddTransient<IRequestBodyStrategy, XmlRequestBodyStrategy>()
                .AddTransient<IRequestBodyStrategy, FormRequestBodyStrategy>()

                .AddTransient<IResponseBodyFactory, ResponseBodyFactory>()

                .AddTransient<IResponseBodyStrategy, BinaryResponseBodyStrategy>()
                .AddTransient<IResponseBodyStrategy, JsonResponseBodyStrategy>()
                .AddTransient<IResponseBodyStrategy, XmlResponseBodyStrategy>()
                .AddTransient<IResponseBodyStrategy, DefaultResponseBodyStrategy>()

                .AddTransient<ITemplateTransformer, TemplateTransformer>()

                .AddTemplatesGenerating();
    }
}
