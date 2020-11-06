using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Mojito.ServiceDesk.Application.Common.Mappings
{
	public static class RegisterAutoMapperService
	{
		public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
		{
			IMapper automapper = registerAutoMapperService();
			services.AddSingleton(automapper);
			return services;
		}

		private static IMapper registerAutoMapperService()
		{
			// Auto Mapper Configurations
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});

			IMapper mapper = mappingConfig.CreateMapper();

			return mapper;
		}
	}
}
