using Bug_Tracker.Helpers;
using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bug_Tracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectsHelper projectsHelper = new ProjectsHelper();

        // GET: ManageRoles
        public ActionResult ManageRoles()
        {
            ViewBag.UserIds = new MultiSelectList(db.Users,"Id","Email");
            ViewBag.Role = new SelectList(db.Roles,"Name","Name");

            var users = new List<ManageRolesViewModel>();
            foreach(var user in db.Users.ToList())
            {
                users.Add(new ManageRolesViewModel
                {
                    UserName = $"{user.LastName},{user.FirstName}",
                    RoleName = roleHelper.ListUserRoles(user.Id).FirstOrDefault()
                });
            }

            return View(users);
        }


        //Post: ManageRoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> userIds, string role) 
        {
            //Step 1: Unenroll all the selected Users from ANY roles
            //they may currently occupy
            foreach(var userId in userIds) 
            {
                //What is the Role of this person??
                var userRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
                if(userRole != null) 
                {
                    roleHelper.RemoveUserFromRole(userId, userRole);
                }
            }

            //Step 2: Add them back to the selected Role
            if (!string.IsNullOrEmpty(role))
            {
                foreach (var userId in userIds)
                {
                    roleHelper.AddUserToRole(userId, role);
                }
            }

            return RedirectToAction("ManageRoles", "Admin");
        }


        // GET: ManageProjects
        public ActionResult ManageProjects()
        {
            ViewBag.Projects = new MultiSelectList(db.Projects,"Id","Name");
            ViewBag.Developers = new MultiSelectList(roleHelper.UsersInRole("Developer"),"Id","Email");
            ViewBag.Developers = new MultiSelectList(roleHelper.UsersInRole("Submitter"), "Id", "Email");

            if (User.IsInRole("Admin"))
            {
                ViewBag.ProjectManagerId = new SelectList(roleHelper.UsersInRole("Manager"), "Id", "Email");
            }

            var myData = new List<ManageProjectsViewModel>();
            ManageProjectsViewModel userVm = null;
            foreach(var user in db.Users.ToList())
            {
                userVm = new ManageProjectsViewModel
                {
                    UserName = $"{user.LastName},{user.FirstName}",
                    ProjectNames = projectsHelper.ListUserProjects(user.Id).Select(p => p.Name).ToList()
                };

                if(userVm.ProjectNames.Count() == 0)
                {
                    userVm.ProjectNames.Add("N/A");
                }

                myData.Add(userVm);
            }

            return View(myData);
        }

        // POST: MangeProjects
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjects(List<int> projects, string ManagerId, List<string> developers, List<string> submitters)
        {
            return RedirectToAction("ManageProjects", "Admin");
        }
    }
}