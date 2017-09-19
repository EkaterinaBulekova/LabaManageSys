using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.Concrete;
using Ninject.Modules;

namespace LabaManageSys.Domain
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEFDbContext>().To<EFDbContext>();
        }
    }
}
