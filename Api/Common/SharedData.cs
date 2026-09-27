using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Common
{
    public static class SharedData
    {
        public static class Roles
        {
            public const string Admin = "admin";
            public const string Moderator = "moderator";
            public const string Consumer = "consumer";

            public static IReadOnlyList<string> AllRoles
            {
                get=> new List<string> {Admin, Moderator, Consumer};
            }
        }
    }
}
