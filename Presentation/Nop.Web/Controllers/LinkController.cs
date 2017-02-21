using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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

         

                context2.SaveChanges();

            }
            return RedirectToAction("Home");
        }

        public ActionResult UpVote(int linkId)
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            var link = db.Links.Where(x => x.LinkId == linkId).FirstOrDefault();

            Vote votey = new Vote
            {
                LinkId = link.LinkId,
                IpAddress = ipList,
                DateAdded = DateTime.UtcNow
            };

            db.Votes.Add(votey);

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

        public ActionResult GetFeeds()
        {
            try
            {
                string url = "https://feedly.com/i/subscription/feed/http://droneblog.com/feed/";
                XmlReader reader = XmlReader.Create(url);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();
                foreach (SyndicationItem item in feed.Items)
                {
                    Create(item.Title.ToString(), item.BaseUri.ToString());
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Home");
        }
    }
}