namespace Bug_Tracker.Migrations
{
    using Bug_Tracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<Bug_Tracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Bug_Tracker.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            //Roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if(!context.Roles.Any(r => r.Name == "Admin")) 
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Manager" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            //User Creation
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var demoPassword = WebConfigurationManager.AppSettings["DemoPassword"];

            if(!context.Users.Any(u => u.Email == "DemoAdmin@Mailinator.com")) 
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@Mailinator.com",
                    Email = "DemoAdmin@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "DemoAdmin",
                    AvatarPath = "/Avatar/avatarPlaceholder.png"
                },demoPassword);
            }

            if (!context.Users.Any(u => u.Email == "DemoManager@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoManager@Mailinator.com",
                    Email = "DemoManager@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Manager",
                    DisplayName = "DemoManager",
                    AvatarPath = "/Avatar/avatarPlaceholder.png"
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == "DemoDeveloper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@Mailinator.com",
                    Email = "DemoDeveloper@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "DemoDeveloper",
                    AvatarPath = "/Avatar/avatarPlaceholder.png"
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == "DemoSubmitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@Mailinator.com",
                    Email = "DemoSubmitter@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "DemoSubmitter",
                    AvatarPath = "/Avatar/avatarPlaceholder.png"
                }, demoPassword);
            }

            //Assign Roles
            var adminId = userManager.FindByEmail("DemoAdmin@Mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var managerId = userManager.FindByEmail("DemoManager@Mailinator.com").Id;
            userManager.AddToRole(managerId, "Manager");

            var devId = userManager.FindByEmail("DemoDeveloper@Mailinator.com").Id;
            userManager.AddToRole(devId, "Developer");

            var subId = userManager.FindByEmail("DemoSubmitter@Mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");

            //Ticket Type
            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                    new TicketType { Name = "Bug", Description =""},
                    new TicketType { Name = "Feature Request", Description = "" }
                );

            //Ticket Priority
            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                    new TicketPriority { Name = "High", Description = "" },
                    new TicketPriority { Name = "Mid", Description = "" },
                    new TicketPriority { Name = "Low", Description = "" }
                );

            //Ticket Status
            context.TicketStatus.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Name = "Open", Description = "" },
                    new TicketStatus { Name = "Assigned", Description = "" },
                    new TicketStatus { Name = "Resolved", Description = "" },
                    new TicketStatus { Name = "Archived", Description = "" }
                );

            //Preload Data
            context.Projects.AddOrUpdate(
                new Project {

                }

                );

            context.Tickets.AddOrUpdate(
                new Ticket { 

                }

                );

            context.TicketAttachments.AddOrUpdate(
                new TicketAttachment {

                }

                );

            context.TicketComments.AddOrUpdate(
                
                new TicketComment {

                }

                );
        }
    }
}
