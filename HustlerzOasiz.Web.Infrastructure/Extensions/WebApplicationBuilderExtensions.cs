using MarauderzOasiz.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static HustlerzOasiz.Common.GeneralApplicationConstants;

namespace HustlerzOasiz.Web.Infrastructure.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            if (serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid service type provided!");
            }

            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();

            foreach (Type st in serviceTypes)
            {
                Type? interfaceType = st.GetInterface($"I{st.Name}");
                if (interfaceType == null)
                {
                    throw new InvalidOperationException($"No interface is provided for the service with name {st.Name}");
                }
                services.AddScoped(interfaceType, st);
            }
        }

        //Very important, learn each component, later
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder applicationBuilder, string email)
        {
            using IServiceScope scopedServices = applicationBuilder.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole<Guid>> roleManager
                = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }
                IdentityRole<Guid> identityRole = new IdentityRole<Guid>(AdminRoleName);
                await roleManager.CreateAsync(identityRole);

                AppUser admin = await userManager.FindByEmailAsync(email);

                await userManager.AddToRoleAsync(admin, AdminRoleName);
            })
                .GetAwaiter()
                .GetResult();

            return applicationBuilder;
        }
    }
}
