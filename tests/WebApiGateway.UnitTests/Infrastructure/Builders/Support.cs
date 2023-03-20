namespace WebApiGateway.UnitTests.Infrastructure.Builders
{
    public static class Support
    {
        public static List<string> GenerateRoleIds(int rolesListSize)
        {
            var roleIds = new List<string>();
            while (rolesListSize > 0)
            {
                roleIds.Add(Guid.NewGuid().ToString());
                rolesListSize--;
            }

            return roleIds;
        }
    }
}