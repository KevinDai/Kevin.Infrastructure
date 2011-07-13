using System.Collections.Generic;
using System.Linq;

namespace Kevin.Infrastructure.IoC
{
    public static class ContainerExtensions
    {
        public static TService GetService<TService>(this IContainer resolver)
        {
            return (TService)resolver.GetService(typeof(TService));
        }

        public static IEnumerable<TService> GetServices<TService>(this IContainer resolver)
        {
            return resolver.GetServices(typeof(TService)).Cast<TService>();
        }
    }
}
