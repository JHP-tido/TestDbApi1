using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDbApi
{
    public class MySettings
    {
        public MySettings() { }
        public string DbString { get; set; }
        public string OtherDbString { get; set; }
    }

    public class JwtValues
    {
        public JwtValues() { }
        public string Key { get; set; }
        public string Issuer { get; set; }
    }
}
