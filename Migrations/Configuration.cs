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
    using Bug_Tracker.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<Bug_Tracker.Models.ApplicationDbContext>
    {
        private UserRolesHelper rolesHelper = new UserRolesHelper();
        private ProjectsHelper projectsHelper = new ProjectsHelper();
        private Random random = new Random();

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

            //Projects
            context.Projects.AddOrUpdate(
                p => p.Name,
                    new Project() { Name = "First Demo", Description = "The first demo project seed", Created = DateTime.Now.AddDays(-3) },
                    new Project() { Name = "Second Demo", Description = "The second demo project seed", Created = DateTime.Now.AddDays(-7) },
                    new Project() { Name = "Third Demo", Description = "The third demo project seed", Created = DateTime.Now.AddDays(-15) },
                    new Project() { Name = "Fourth Demo", Description = "The fourth demo project seed", Created = DateTime.Now.AddDays(-45) },
                    new Project() { Name = "Fifth Demo", Description = "The fifth demo project seed", Created = DateTime.Now.AddDays(-60) } 
                );

            context.SaveChanges();

            var projects = context.Projects.ToList();
            var statuses = context.TicketStatus.ToList();
            var priorities = context.TicketPriorities.ToList();
            var types = context.TicketTypes.ToList();
            var users = context.Users;
            var roles = context.Roles;
            var maxRandomSubmitter = rolesHelper.UsersInRole("Submitter").ToList().Count();
            var maxRandomDeveloper = rolesHelper.UsersInRole("Developer").ToList().Count();

            foreach (var project in projects)
            {
                var count = 1;
                foreach (var status in statuses)
                {
                    foreach (var type in types)
                    {
                        int randomNumber = random.Next(0, 2);
                        if (randomNumber == 0)
                        {
                            var allSubmitters = rolesHelper.UsersInRole("Submitter").ToList();
                            var allDevelopers = rolesHelper.UsersInRole("Developer").ToList();
                            foreach (var priority in priorities)
                            {
                                int randomDays = random.Next(0, 31);
                                var randomSubmitter = allSubmitters[random.Next(0, maxRandomSubmitter)];
                                var randomDeveloper = allDevelopers[random.Next(0, maxRandomDeveloper)];
                                context.Tickets.AddOrUpdate(
                                t => t.Title,
                                new Ticket
                                {
                                    ProjectId = project.Id,
                                    TicketTypeId = type.Id,
                                    TicketPriorityId = priority.Id,
                                    TicketStatusId = status.Id,
                                    SubmiterID = randomSubmitter.Id,
                                    DeveloperID = randomDeveloper.Id,
                                    Title = project.Name + " Demo Ticket " + count,
                                    Discription = "A demo ticket of priority '" + priority.Name + "', type '" + type.Name + "', status '" + status.Name + "'",
                                    Created = DateTime.Now.AddDays(-randomDays),
                                });
                                count++;
                                context.SaveChanges();
                            }
                        }
                    }
                }
                foreach (var user in rolesHelper.UsersInRole("Manager").Concat(rolesHelper.UsersInRole("Developer")).Concat(rolesHelper.UsersInRole("Submitter")))
                {
                    int randomNumber = random.Next(0, 2);
                    if (randomNumber == 0)
                    {
                        projectsHelper.AddUserToProject(user.Id, project.Id);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}


            //context.TicketAttachments.AddOrUpdate(
                //new TicketAttachment {

                //}

               // );

            //context.TicketComments.AddOrUpdate(
                
               // new TicketComment {

               // }

               // );
        