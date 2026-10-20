using FifteenGame.Business.Services;
using FifteenGame.Common.Contracts;
using Ninject.Modules;

namespace FifteenGame.WebApi
{
    public class FifteenGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameService>().To<GameService>();
        }
    }
}