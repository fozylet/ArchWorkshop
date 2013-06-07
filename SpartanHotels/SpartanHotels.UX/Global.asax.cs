using SpartanHotels.Domain.Contracts;
using SpartanHotels.Repository.Contracts;
using StructureMap;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SpartanHotels.Ux
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    //scan.AssembliesFromApplicationBaseDirectory();
                    scan.Assembly("SpartanHotels.Domain");
                    scan.Assembly("SpartanHotels.Repository.Web");
                    scan.Assembly("SpartanHotels.Repository.Worker");
                    scan.AddAllTypesOf<IAvailability>();
                    scan.AddAllTypesOf<IStatus>();
                    scan.AddAllTypesOf<IBooking>();
                    scan.AddAllTypesOf<ICancellation>();
                    scan.AddAllTypesOf<ISnapshotRepository>();
                    scan.AddAllTypesOf<IMasterRepository>();
                    scan.AddAllTypesOf<IEventRepository>();
                });
            });
        }
    }

    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return ((requestContext == null) || (controllerType == null)) ? null : (Controller) ObjectFactory.GetInstance(controllerType);
        }
    }
}