using LibraryAppWithDapper.APIClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LibraryAppWithDapper.APIClient.IoC
{
	public static class ServiceCollectionExtension
	{
		public static void AddApiClientService(this IServiceCollection services,
			Action<ApiClientOptions> options)
		{
			services.Configure(options);
			services.AddSingleton(provider =>
			{
				var options = provider.GetRequiredService<IOptions<ApiClientOptions>>().Value;
				return new ApiClientService(options);
			});
		}
	}
}
