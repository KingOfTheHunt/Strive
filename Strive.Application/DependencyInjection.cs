using MedTheMediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Strive.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMedTheMediator(typeof(DependencyInjection).Assembly);

        return services;
    }
}