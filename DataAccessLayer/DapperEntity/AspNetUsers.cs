namespace ApplicationCore.DapperEntity
{
    public class AspNetUsers
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public DateTimeOffset? LockOutEnd { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public AspNetRoles? AspNetRoles { get; set; }
        public AspNetUserRoles? AspNetUserRoles { get; set; }



    }
}
