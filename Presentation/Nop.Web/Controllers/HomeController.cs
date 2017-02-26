using System.Web.Mvc;
using Nop.Web.Framework.Security;
using System.Linq;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        //This wasnt workign when i added my link list to front page.  not sure why
        [NopHttpsRequirement(SslRequirement.No)]


        public virtual ActionResult Index()
        {

            return View();
        }
    }
}
