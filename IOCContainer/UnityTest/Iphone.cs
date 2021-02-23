using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOCContainer.UnityTest
{
    public interface IPhone
    {
        void Call();
        void Text();

        IHeadphone iHeadphone { get; set; }
        IPower iPower { get; set; }
    }
    public interface IHeadphone
    {
    }
    public interface IPower
    {
    }
}