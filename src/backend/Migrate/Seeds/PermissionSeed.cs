using System.Linq;
using System.Threading.Tasks;
using Domain.AggregatesModel.PermissionAggregate;
using Microsoft.EntityFrameworkCore;
using Migrate.Configuration.Permission;

namespace Migrate.Seeds
{
    public static class PermissionSeed
    {
        public static async Task EnsureSeedData<TDbContext>(TDbContext context)
            where TDbContext : DbContext
        {
            var permissionDbSet = context.Set<Permission>();
            await permissionDbSet.AddRangeAsync(Permissions.GetPermissions().Select(x => new Permission(x)));
            await context.SaveChangesAsync();
        }
    }
}
