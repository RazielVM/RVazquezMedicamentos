using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DL_OleDB
{
    public class DataBaseAcces
    {
        public static string GetStringConnection()
        {
            return ConfigurationManager.ConnectionStrings["MyConnectionOleDB"].ConnectionString; 
        }
    }
}
