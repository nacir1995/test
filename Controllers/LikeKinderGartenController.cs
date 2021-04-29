using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class LikeKinderGartenController : Controller
    {
        // GET: LikeKinderGarten
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();

            Client.BaseAddress = new Uri("jdbc:mysql://localhost:3306/Kindergarten");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("api/LikeKinderGarten").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<LikeKinderGarten>>().Result;

            }
            else
            {
                ViewBag.result = "erreur";
            }
            return View();

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(LikeKinderGarten pst)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("jdbc:mysql://localhost:3306/Kindergarten");
            Client.PostAsJsonAsync<LikeKinderGarten>("api/LikeKinderGarten", pst).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }
    }
}