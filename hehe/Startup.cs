using hehe.Models;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;

[assembly: OwinStartupAttribute(typeof(hehe.Startup))]
namespace hehe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "ProductManager";
                roleManager.Create(role);


                //Here we create a Admin super user who will maintain the website                  
                var user = new ApplicationUser();
                user.UserName = "stefan.holmberg@nackademin.se";
                user.Email = "stefan.holmberg@nackademin.se";
                string userPWD = "Hejsan123#";
                var chkUser = UserManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                    UserManager.AddToRole(user.Id, "Admin");

                user = new ApplicationUser();
                user.UserName = "per.persson@nackademin.se";
                user.Email = "per.persson@nackademin.se";
                userPWD = "Hejsan123#";
                chkUser = UserManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                    UserManager.AddToRole(user.Id, "ProductManager");


            }
        }

    }
}
