using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spencer.NET;

namespace SpotifyShuffler.Interface
{
    public class SpotifyService
    {
        protected Dictionary<string, List<ServiceBase>> Services { get; set; }
        protected IContainer Container { get; set; }

        protected SpotifyService()
        {
            Container = ContainerFactory.Container();
            Services = new Dictionary<string, List<ServiceBase>>();
        }

        public async Task<TService> GetAsync<TService>(string accessToken) where TService : ServiceBase
        {
            return await Task.Run(() =>
            {
                TService service = Services[accessToken]
                    .Where(x => x != null)
                    .Where(x => x is TService)
                    .FirstOrDefault() as TService;

                if (service == null)
                {
                    return (TService) Container
                        .ResolveOrAuto<InstancesCreator>()
                        .CreateInstance(typeof(TService));
                }

                else return service;
            });
        }
    }
}