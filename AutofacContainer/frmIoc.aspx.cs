using Autofac;
using Autofac.Configuration;
using AutofacContainer.AutofacTest;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutofacContainer
{
    public partial class frmIoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
 
        }
        void A()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DatabaseManager>();
            builder.RegisterType<SqlDatabase>().As<IDatabase>();
            using (var container = builder.Build())
            {
                var manager = container.Resolve<DatabaseManager>();
                manager.Search("SELECT * FORM USER");
            }
        }
        void B()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SqlDatabase>().Named<IDatabase>("sql");
            builder.RegisterType<OracleDatabase>().Named<IDatabase>("oracle");
            IContainer container = builder.Build();
            SqlDatabase sqlDAL = (SqlDatabase)container.ResolveNamed<IDatabase>("sql");
            OracleDatabase oracleDAL = (OracleDatabase)container.ResolveNamed<IDatabase>("oracle");
        }
        void C()
        {
            //var builder = new ContainerBuilder();
            //builder.RegisterType<DatabaseManager>();
            //builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            //using (var container = builder.Build())
            //{
            //    var manager = container.Resolve<DatabaseManager>();
            //    manager.Search("SELECT * FORM USER");
            //}
        }
        void D()
        {
            ////MVC中需要在函数Application_Start() 注册自己的控制器类，一定要引入Autofac.Integration.Mvc.dll
            
            //var builder = new ContainerBuilder();

            //builder.RegisterControllers(Assembly.GetCallingAssembly())//注册mvc的Controller
            //    .PropertiesAutowired();//属性注入


            ////1、无接口类注入
            ////builder.RegisterType<BLL.newsClassBLL>().AsSelf().InstancePerRequest().PropertiesAutowired();


            ////2、有接口类注入
            ////注入BLL，UI中使用
            //builder.RegisterAssemblyTypes(typeof(PBMS.Bll.BaseService<>).Assembly)
            //    .AsImplementedInterfaces()  //是以接口方式进行注入
            //    .InstancePerRequest()       //每次http请求
            //    .PropertiesAutowired();     //属性注入

            ////注入DAL，BLL层中使用
            ////builder.RegisterAssemblyTypes(typeof(PBMS.Dal.BaseDal<>).Assembly).AsImplementedInterfaces()
            ////    .InstancePerRequest().PropertiesAutowired();     //属性注入

            ////Cache的注入，使用单例模式
            ////builder.RegisterType<RedisCacheManager>()
            ////    .As<ICacheManager>()
            ////    .SingleInstance()
            ////    .PropertiesAutowired();

            ////移除原本的mvc的容器，使用AutoFac的容器，将MVC的控制器对象实例交由autofac来创建
            //var container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}