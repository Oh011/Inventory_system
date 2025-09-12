using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces.Repositories;

namespace Infrastructure.Identity.DataSeeding
{
    internal class IdentityDbInitializer(



      ApplicationDbContext _dbContext,

     RoleManager<IdentityRole> _roleManager,

     UserManager<ApplicationUser> _userManager,

     IUnitOfWork unitOfWork


        ) : IdentityInitializer
    {








        public async Task InitializeAsync()
        {
            try
            {



                if (_dbContext.Database.GetPendingMigrations().Any())
                {

                    await _dbContext.Database.MigrateAsync();
                }




                if (!_roleManager.Roles.Any())
                {

                    string[] roleNames = { "Admin", "Manager", "Warehouse", "Salesperson" };

                    foreach (var roleName in roleNames)
                    {
                        if (!await _roleManager.RoleExistsAsync(roleName))
                            await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }



                if (!_userManager.Users.Any())
                {


                    var Admin = new ApplicationUser()
                    {

                        Email = "Admin@Gmail.com",
                        FullName = "AdminUser",
                        UserName = "AdminUser"


                    };




                    var result = await _userManager.CreateAsync(Admin, "Admin#123");

                    if (result.Succeeded)
                    {
                        Console.WriteLine("Yes");
                    }

                    await _userManager.AddToRoleAsync(Admin, "Admin");



                    var employee = new Employee
                    {
                        ApplicationUserId = Admin.Id,
                        FullName = Admin.FullName,
                        Role = "Admin",
                        JobTitle = "System Administrator",
                        Address = "Head Office",
                        NationalId = "00000000000000",
                    };


                    await unitOfWork.GetRepository<Employee, int>().AddAsync(employee);
                    await unitOfWork.Commit();
                }


            }




            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());

            }
        }
    }
}
