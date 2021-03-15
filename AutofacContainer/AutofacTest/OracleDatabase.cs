using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AutofacContainer.AutofacTest
{
    public class OracleDatabase : IDatabase
    {
        public string Name
        {
            get { return "oracle"; }
        }

        public void Select(string commandText)
        {
            Debug.WriteLine(string.Format("'{0}' is a query sql in {1}!", commandText, Name));
        }

        public void Insert(string commandText)
        {
            Debug.WriteLine(string.Format("'{0}' is a insert sql in {1}!", commandText, Name));
        }

        public void Update(string commandText)
        {
            Debug.WriteLine(string.Format("'{0}' is a update sql in {1}!", commandText, Name));
        }

        public void Delete(string commandText)
        {
            Debug.WriteLine(string.Format("'{0}' is a delete sql in {1}!", commandText, Name));
        }
    }
}