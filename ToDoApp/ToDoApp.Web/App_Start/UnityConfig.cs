using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Web.Http;
using ToDoApp.Web.Services.Interfaces;
using ToDoApp.Web.Services;
using Unity.WebApi;

namespace ToDoApp.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IToDoService, ToDoService>();

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
        }
    }
}