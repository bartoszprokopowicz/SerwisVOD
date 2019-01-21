using System;
using System.Threading.Tasks;
using VOD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VOD.Data
{
    public static class Seed
    {
        public static async Task CreateRoles(UserManager<Uzytkownicy> UserManager, RoleManager<ApplicationRole> RoleManager, IConfiguration Configuration)
        {
            //adding customs roles
            string[] roleNames = { "Admin", "Pracownik", "Uzytkownik" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }

            //creating a super user who could maintain the web app
            var poweruser = new Uzytkownicy
            {
                UserName = Configuration.GetSection("AppSettings")["UserEmail"],
                Email = Configuration.GetSection("AppSettings")["UserEmail"],
                PhoneNumber = Configuration.GetSection("AppSettings")["NumerTelefonu"],
                Daneosobowe = new Daneosobowe
                {
                    Imie = Configuration.GetSection("AppSettings")["Imie"],
                    Nazwisko = Configuration.GetSection("AppSettings")["Nazwisko"],
                    DataUrodzin = System.Convert.ToDateTime(Configuration.GetSection("AppSettings")["DataUrodzin"])             
                },
                DataUtworzenia = DateTime.Now
            };

            string userPassword = Configuration.GetSection("AppSettings")["UserPassword"];
            var user = await UserManager.FindByEmailAsync(Configuration.GetSection("AppSettings")["UserEmail"]);

            if(user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
        }
    }
}