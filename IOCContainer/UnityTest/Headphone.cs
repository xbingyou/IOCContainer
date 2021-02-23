using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOCContainer.UnityTest
{
    public class Headphone : IHeadphone
    {
        public Headphone()
        {
            System.Diagnostics.Debug.WriteLine("Headphone 被构造");
        }
    }
}