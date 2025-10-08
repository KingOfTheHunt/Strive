using MedTheMediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Strive.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMedTheMediator(typeof(DependencyInjection).Assembly);
    }
}