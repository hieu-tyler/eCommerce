using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.SystemConstants.cs
{
    public class SystemConstants
    {
        public const string MainConnectionString = "ECommerceDb"; // Name of the connection string in appsettings.json
        public const string AdminRole = "Admin"; // Role name for admin users
        public const string UserRole = "User"; // Role name for regular users
        public const string SystemName = "ECommerce"; // Name of the system
        public const string SystemVersion = "1.0.0"; // Version of the system

        public class AppSettings
        {
            public const string DefaultLanguageId = "DefaultLanguageId"; // Key for default language ID in appsettings.json
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress"; // Key for base address of the API in appsettings.json
        }
    }
}
