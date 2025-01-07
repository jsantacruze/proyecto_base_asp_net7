using domain_layer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access
{
    public class UserIdentityDataInicializer
    {
        public static async Task Inicialize(DatabaseContext context, UserManager<SystemUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var usuario = new SystemUser { FullName = "Jhovany Santacruz", UserName = "jsantacruze", Email = "jsantacruze@hotmail.com" };
                await userManager.CreateAsync(usuario, "Digitalsoft2023-");
            }
        }

    }
}
