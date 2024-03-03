namespace kvaksy_backend
{
    public static class Globals
    {
        public static bool IsUser = false;
        public static bool IsAdmin = false;

        public static void CheckForUserLevelPermission()
        {
            if (IsAdmin)
            {
                return;
            }
            if (!IsUser)
            {
                throw new UnauthorizedAccessException("You are not authorized to perform this action.");
            }
        }

        public static void CheckForAdminLevelPermission()
        {
            if (!IsAdmin)
            {
                throw new UnauthorizedAccessException("You are not authorized to perform this action.");
            }
        }
    }
}
