using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spencer.NET;

namespace SpotifyShuffler.Interface
{
    public class SpotifyService
    {
        protected IContainer Container { get; set; }
        protected SpotifyAuthorization Authorization { get; set; }

        public SpotifyService()
        {
            Container = ContainerFactory.Container();
        }

        public async Task<TService> GetAsync<TService>(SpotifyAuthorization authorization) where TService : ServiceBase
        {
            return await Task.Run(() =>
            {
                TService service = (TService) Container.ResolveOrAuto<InstancesCreator>().CreateInstance(typeof(TService), Container);
                service.SpotifyAuthorization = authorization;

                return service;
            });
        }
    }
}