using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicR8r.Data;
using MusicR8r.Models;
using AutoMapper;
using MusicR8r.Services.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MusicR8r.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;


        public ProfileController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        // GET: Profile
        //public ActionResult Index()
        //{
        //    return View(/*db.ProfileViewModels.ToList()*/);
        //}

        // GET: Profile/Details/5
        public ActionResult Details()
        {
            var id = new Guid(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = this.userService.GetUserById(id);
            ProfileViewModel profileViewModel = this.mapper.Map<ProfileViewModel>(user);
            if (profileViewModel == null)
            {
                return HttpNotFound();
            }
            return View(profileViewModel);
        }

        // GET: Profile/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public ActionResult AddAlbum(Guid albumId)
        {
            return null;
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Email,UserName,FirstName,LastName,FullName,Bio")] ProfileViewModel profileViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        profileViewModel.Id = Guid.NewGuid();
        //        //db.ProfileViewModels.Add(profileViewModel);
        //        //db.SaveChanges();
        //        return RedirectToAction("Details");
        //    }

        //    return View(profileViewModel);
        //}

        // GET: Profile/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.userService.GetUserById((Guid)id);
            ProfileViewModel profileViewModel = this.mapper.Map<ProfileViewModel>(user);

            if (profileViewModel == null)
            {
                return HttpNotFound();
            }

            return View(profileViewModel);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,UserName,FirstName,LastName,FullName,Bio")] ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                this.userService.Update(profileViewModel.Id, profileViewModel.Email, profileViewModel.UserName, profileViewModel.FirstName, profileViewModel.LastName, profileViewModel.Bio);
                return RedirectToAction("Details");
            }
            return View(profileViewModel);
        }

        // GET: Profile/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var user = this.userService.GetUserById((Guid)id);
        //    ProfileViewModel profileViewModel = this.mapper.Map<ProfileViewModel>(user);
        //    if (profileViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(profileViewModel);
        //}

        // POST: Profile/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    ProfileViewModel profileViewModel = null;
        //    //db.ProfileViewModels.Remove(profileViewModel);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
