using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spencer.NET;

namespace SpotifyShuffler.Interface
{
    public class SpotifyService
    {
        protected IContainer Container { get; set; }
        protected Authorization Authorization { get; set; }

        public SpotifyService()
        {
            Container = ContainerFactory.Container();
        }

        public async Task<TService> GetAsync<TService>(Authorization authorization) where TService : ServiceBase
        {
            return await Task.Run(() =>
            {
                TService service = (TService) Container.ResolveOrAuto<InstancesCreator>().CreateInstance(typeof(TService));
                service.Authorization = authorization;

                return service;
            });
        }
    }
}