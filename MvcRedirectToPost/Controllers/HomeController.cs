using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fluentx.Mvc;
using MvcRedirectToPost.Helper.Results;

namespace MvcRedirectToPost.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // TODO:  加入套件 Fluentx.Mvc
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            //return View();

            // TODO 修改Return方法
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("id", Guid.NewGuid().ToString());
            postData.Add("name", "Kerry");

            // TODO 使用 Fluentx.Mvc 方法回傳
            //return this.RedirectAndPost("http://localhost:53642/Home/Contact", postData);

            // TODO 利用內容物件回傳
            return Content(new RedirectToPostContentResult("http://localhost:53642/Home/Contact", postData).GetResult());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string id, string name)
        {
            ViewBag.Message = string.Format("Your id is {0}. {1}", id, name);

            return View();
        }
    }
}