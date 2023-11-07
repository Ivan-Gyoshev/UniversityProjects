using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Project.NASA.Client
{
    public static class NasaClientServiceExtension
    {
        public static IServiceCollection AddNasaClient(this IServiceCollection services, IConfiguration configuration)
        {
            Uri nasaAddress = new Uri(configuration["Nasa:Address"]);

            services.AddHttpClient<INasaClient,NasaClient>($"nasa_client", client => client.BaseAddress = nasaAddress);

            return services;
        }
    }
}
