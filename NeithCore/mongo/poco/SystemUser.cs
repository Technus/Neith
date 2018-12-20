using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NeithCore.MongoDB.poco
{
    class SystemUser
    {
        [BsonIgnoreIfNull]
        public readonly string domainName, userName, systemName;

        public SystemUser()
        {
            string name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string[] pieces = name.Split('\\');
            if (pieces.Length == 2)
            {
                domainName = pieces[0];
                userName = pieces[1];
            }
            else
            {
                domainName = null;
                userName = name;
            }
            domainName = pieces.Length > 0 ? pieces[0] : null;
            userName = pieces.Length > 0 ? pieces[1] : name;
            StringBuilder nameBuilder = new StringBuilder(260);
            uint size = 260;
            bool success = GetComputerNameEx(COMPUTER_NAME_FORMAT.ComputerNameDnsHostname, nameBuilder, ref size);
            if (success)
            {
                systemName = nameBuilder.ToString();
            }
            else
            {
                systemName = System.Net.Dns.GetHostName();
            }
        }

        [BsonConstructor]
        public SystemUser(string domainName, string userName, string systemName)
        {
            this.domainName = domainName;
            this.userName = userName;
            this.systemName = systemName;
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool GetComputerNameEx(COMPUTER_NAME_FORMAT NameType, StringBuilder lpBuffer, ref uint lpnSize);

        private enum COMPUTER_NAME_FORMAT
        {
            ComputerNameNetBIOS,
            ComputerNameDnsHostname,
            ComputerNameDnsDomain,
            ComputerNameDnsFullyQualified,
            ComputerNamePhysicalNetBIOS,
            ComputerNamePhysicalDnsHostname,
            ComputerNamePhysicalDnsDomain,
            ComputerNamePhysicalDnsFullyQualified,
            ComputerNameMax
        }
    }
}
