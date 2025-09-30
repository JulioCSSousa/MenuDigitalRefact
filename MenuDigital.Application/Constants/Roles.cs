namespace MenuDigital.Application.Contants
{
    public static class Roles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Tenant = "Tenant";
        public const string Client = "Client";

        public static readonly string[] AllRoles = { Admin, Tenant, Client };
    }

}
