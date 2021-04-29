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
    public class ParentsController : Controller
    {
        // GET: Parents
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8081/SpringMVC/servlet/");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("retrieve-all-Parents").Result;
            IList<Parents> parents;
            if (response.IsSuccessStatusCode)
            {
                parents = response.Content.ReadAsAsync<IList<Parents>>().Result;
            }
            else
            {
                return View("Index");

            }

            return View(parents);
        }

    }
}