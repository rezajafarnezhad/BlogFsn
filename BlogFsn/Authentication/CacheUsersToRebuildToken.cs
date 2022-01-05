using System.Collections.Generic;
using System.Linq;

namespace BlogFsn.Authentication
{
    public static class CacheUsersToRebuildToken
    {

        private static List<string> UserIds { get; set; }

        public static void AddRenge(List<string> UsersId)
        {
            if (UserIds == null)
                UserIds = new List<string>();

            UserIds.AddRange(UsersId);
        }
        public static void Add(string UsersId)
        {
            if (UserIds == null)
                UserIds = new List<string>();

            UserIds.Add(UsersId);
        }
        public static bool Any(string UserId)
        {
            if (UserIds == null)
                return false;

            return UserIds.Any(c => c == UserId);
        }

        public static void Remove(string UserId)
        {
            if (UserIds != null)
            {
                UserIds.Remove(UserId);

            }
        }
    }
}