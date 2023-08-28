using Microsoft.Extensions.DependencyInjection;
using Quizzer.Core;
using Quizzer.Providers.OpenTrivia.Options;

namespace Quizzer.Providers.OpenTrivia
{
    public static class Extensions
    {
        public static IServiceCollection AddOpenTriviaProvider(this IServiceCollection services, Action<OpenTriviaOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            services.Configure<OpenTriviaOptions>(configureOptions);
            services.AddHttpClient("OpenTriviaProvider");
            services.Add(new ServiceDescriptor(typeof(OpenTriviaProvider), typeof(OpenTriviaProvider), lifetime));
            services.Add(new ServiceDescriptor(typeof(IQuestionsProvider), typeof(OpenTriviaProvider), lifetime));

            return services;
        }
    }
}
