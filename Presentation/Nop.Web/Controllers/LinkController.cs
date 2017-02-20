using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Controllers
{
    public class LinkController : Controller
    {
        public NopComAddOnEntities db = new NopComAddOnEntities();
        // GET: Link
        public ActionResult Home()
        {
            var links = db.Links.Where(x => x.Title != null && x.Author != null && x.Url != null).OrderByDescending(x=>x.Votes).ToList();


            return View("Index", links);
        }

        public ActionResult Create(string stuff, string url)
        {
            using (NopComAddOnEntities context2 = new NopComAddOnEntities())
            {

                Link link = new Link
                {
                    Title = stuff,
                    Url = url,
                    Votes = 0,
                    Author = "-Alex",
                    DateAdded = DateTime.UtcNow
                };
              

            context2.Links.Add(link);

                Vote votey = new Vote
                {
                    LinkId = link.LinkId,
                    IpAddress = "123456789",
                    DateAdded = DateTime.UtcNow
                };

                context2.Votes.Add(votey);


                context2.SaveChanges();

            }
            return RedirectToAction("Home");
        }

        public ActionResult UpVote(int linkId)
        {
           

            var link = db.Links.Where(x => x.LinkId == linkId).FirstOrDefault();

            link.Votes = link.Votes + 1;
            db.SaveChanges();
            return RedirectToAction("Home");
        }

        public ActionResult DownVote(int linkId)
        {
            var link = db.Links.Where(x => x.LinkId == linkId).FirstOrDefault();

            link.Votes = link.Votes - 1;
            db.SaveChanges();
            return RedirectToAction("Home");
        }

        public ActionResult AddComment(int linkId, int customerId, string comment)
        {

            using (NopComAddOnEntities context2 = new NopComAddOnEntities())
            {
                LinkComment linkComment = new LinkComment
                {
                    CustomerId = customerId,
                    LinkId = linkId,
                    AddedDate = DateTime.UtcNow,
                    CommentText = comment,
                    Likes = 0
                };
                context2.LinkComments.Add(linkComment);
                context2.SaveChanges();
            }
            return RedirectToAction("Home");
        }
    }
}