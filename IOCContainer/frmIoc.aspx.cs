using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using IOCContainer.UnityTest;

namespace IOCContainer
{
    public partial class frmIoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            A();
            B();
            C();
            D();
            E();
        }
        void A()
        {
            IUnityContainer container = new UnityContainer();  //定义一个容器
            container.RegisterType<IPhone, AndroidPhone>();  //注册类型，表示遇到IPhone类型，创建AndroidPhone的实例
            IPhone phone = container.Resolve<IPhone>();  //创建实例
            phone.Call();   //调用实例方法
        }
        void B()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IPhone, ApplePhone>();
            container.RegisterType<IHeadphone, Headphone>();
            container.RegisterType<IPower, Power>();
            IPhone phone = container.Resolve<IPhone>();
        }
        void C()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IPhone, ApplePhone>();   //注册IPhone的第一个实现类
            container.RegisterType<IPhone, AndroidPhone>("Android");//Iphone的第二个实现类
            container.RegisterType<IHeadphone, Headphone>();
            container.RegisterType<IPower, Power>();
            IPhone phone = container.Resolve<IPhone>();     //构造了ApplePhone
            IPhone Android = container.Resolve<IPhone>("Android");	//构造了AndroidPhone
        }
        void D()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IPhone, AndroidPhone>();
            container.RegisterType<IPhone, AndroidPhone>(new TransientLifetimeManager());//每一次都是全新生成
            container.RegisterType<IPhone, AndroidPhone>(new ContainerControlledLifetimeManager());//容器单例  单例就不要自己实现
            var phone1 = container.Resolve<IPhone>();
            var phone2 = container.Resolve<IPhone>();
            System.Diagnostics.Debug.WriteLine($"object.ReferenceEquals(phone1, phone2) = {object.ReferenceEquals(phone1, phone2)}");
            //PerThreadLifetimeManager 在同一线程内的对象是单一的，而不同线程之间是不共享对象的
            container.RegisterType<IPhone, AndroidPhone>(new PerThreadLifetimeManager());
            //线程单例：相同线程的实例相同 不同线程的实例不同   web请求/多线程操作
            IPhone iphone1 = null;
            Action act1 = new Action(() =>
            {
                iphone1 = container.Resolve<IPhone>();
                System.Diagnostics.Debug.WriteLine($"iphone1由线程id={Thread.CurrentThread.ManagedThreadId}");
            });
            var result1 = act1.BeginInvoke(null, null);

            IPhone iphone2 = null;
            Action act2 = new Action(() =>
            {
                iphone2 = container.Resolve<IPhone>();
                System.Diagnostics.Debug.WriteLine($"iphone2由线程id={Thread.CurrentThread.ManagedThreadId}");
            });

            IPhone iphone3 = null;
            var result2 = act2.BeginInvoke(t =>
            {
                iphone3 = container.Resolve<IPhone>();
                System.Diagnostics.Debug.WriteLine($"iphone3由线程id={Thread.CurrentThread.ManagedThreadId}");
                System.Diagnostics.Debug.WriteLine($"object.ReferenceEquals(iphone2, iphone3) = {object.ReferenceEquals(iphone2, iphone3)}");
            }, null);

            act1.EndInvoke(result1);
            act2.EndInvoke(result2);

            System.Diagnostics.Debug.WriteLine($"object.ReferenceEquals(iphone1, iphone2) = {object.ReferenceEquals(iphone1, iphone2)}");
        }

        void E()
        {
            //使用配置文件
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Configs\\Unity.config");//找配置文件的路径
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);

            IUnityContainer container = new UnityContainer();
            section.Configure(container, "ContainerOne");
            IPhone phone = container.Resolve<IPhone>();
            phone.Call();

            IPhone android = container.Resolve<IPhone>("Android");
            android.Call();
        }
    }
}