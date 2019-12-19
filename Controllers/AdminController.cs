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
            ViewBag.UserIds = new MultiSelectList(db.Users,"Id","DisplayName");
            ViewBag.Role = new SelectList(db.Roles,"Name","Name");

            var users = new List<ManageRolesViewModel>();
            foreach(var user in db.Users.ToList())
            {
                users.Add(new ManageRolesViewModel
                {
                    UserName = $"{user.DisplayName}",
                    RoleName = roleHelper.ListUserRoles(user.Id).FirstOrDefault()
                });
            }

            return View(users);
        }


        //Post: ManageRoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles([Bind(Include = "UserIds, Role")]List<string> userIds, string role) 
        {

            if (ModelState.IsValid)
            {
                //Step 1: Unenroll all the selected Users from ANY roles
                //they may currently occupy
                foreach (var userId in userIds)
                {
                    //What is the Role of this person??
                    var userRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
                    if (userRole != null)
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
            }

            return RedirectToAction("ManageRoles", "Admin");
        }


        // GET: ManageProjects
        public ActionResult ManageProjects()
        {
            ViewBag.Projects = new MultiSelectList(db.Projects,"Id","Name");
            ViewBag.Developers = new MultiSelectList(roleHelper.UsersInRole("Developer"),"Id","DisplayName");
            ViewBag.Submitters = new MultiSelectList(roleHelper.UsersInRole("Submitter"), "Id", "DisplayName");


            if (User.IsInRole("Admin"))
            {
                ViewBag.UserIds = new SelectList(db.Users, "Id", "DisplayName");
            }
            else
            {
                List<ApplicationUser> users = new List<ApplicationUser>();
                users.AddRange(roleHelper.UsersInRole("Developer").ToList());
                users.AddRange(roleHelper.UsersInRole("Submitter").ToList());

                ViewBag.UserIds = new SelectList(users, "Id", "DisplayName");
            }

            var myData = new List<ManageProjectsViewModel>();
            ManageProjectsViewModel userVm = null;
            foreach(var user in db.Users.ToList())
            {
                userVm = new ManageProjectsViewModel
                {
                    UserName = $"{user.DisplayName}",
                    ProjectNames = projectsHelper.ListUserProjects().Select(p => p.Name).ToList()
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
        public ActionResult ManageProjects(List<int> projects, string UserIds)
        {
            if(projects != null)
            {
                foreach(var projectId in projects)
                {

                    if (!string.IsNullOrEmpty(UserIds))
                    {
                        projectsHelper.AddUserToProject(UserIds, projectId);
                    }

                }
            }
            return RedirectToAction("ManageProjects", "Admin");
        }
    }
}