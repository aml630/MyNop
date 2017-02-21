﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Controllers
{
    public class FeedController : Controller
    {
        // GET: Feed
        public ActionResult Index()
        {
            return View();
        }

        // GET: Feed/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Feed/Create
        public ActionResult Create(string url, string img)
        {
            using (NopComAddOnEntities context = new NopComAddOnEntities())
            {

                Feed Feed = new Feed
                {
                    FeedUrl = url,
                    FeedImg = img
                };

                context.Feeds.Add(Feed);
                context.SaveChanges();

            }

            return RedirectToAction("Home", "Link");
        }

        // POST: Feed/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Home", "Link");
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Home", "Link");
        //    }
        //}

        // GET: Feed/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Feed/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Feed/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Feed/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
