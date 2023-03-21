namespace WebApiGateway.UnitTests.Infrastructure.Builders.Helpers
{
    public static class GuidHelper
    {
        public static IEnumerable<Guid> GenerateGuidList(int rolesListSize)
        {
            var list = new List<Guid>();
            while (rolesListSize > 0)
            {
                list.Add(Guid.NewGuid());
                rolesListSize--;
            }
            return list;
        }
    }
}