using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace IOCContainer.UnityTest
{
    public class ApplePhone : IPhone
    {
        /*三种注入方式的执行顺序：
         * 先执行构造函数注入，
         * 在执行属性注入，
         * 最后执行方法注入。*/

        [Dependency]  //属性注入
        public IHeadphone iHeadphone { get; set; }
        [Dependency]  //属性注入
        public IPower iPower { get; set; }

        public ApplePhone()
        {
            System.Diagnostics.Debug.WriteLine($"{this.GetType().Name}构造函数");
        }

        [InjectionConstructor]  //构造函数注入
        public ApplePhone(IHeadphone headphone)
        {
            this.iHeadphone = headphone;
            System.Diagnostics.Debug.WriteLine($"{this.GetType().Name}带参数构造函数");
        }
        [InjectionMethod]  //方法注入
        public void Init(IPower power)
        {
            this.iPower = power;
        }

        public void Call()
        {
            System.Diagnostics.Debug.WriteLine($"{this.GetType().Name}打电话");
        }



        public void Text()
        {
            throw new NotImplementedException();
        }
    }
}