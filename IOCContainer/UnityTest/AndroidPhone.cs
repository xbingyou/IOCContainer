using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOCContainer.UnityTest
{
    public class AndroidPhone : IPhone
    {
        public IHeadphone iHeadphone
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public IPower iPower
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public AndroidPhone()
        {
            System.Diagnostics.Debug.WriteLine($"{this.GetType().Name}构造函数");
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