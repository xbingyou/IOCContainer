using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOCContainer.UnityTest
{
    public class Power : IPower
    {
        public Power()
        {
            System.Diagnostics.Debug.WriteLine("Power 被构造");
        }
    }
}