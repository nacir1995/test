using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class CommentController : Controller
    {

        HttpClient httpClient;
        string baseAddress;
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        public CommentController()
        {

            baseAddress = "http://localhost:8081/SpringMVC/servlet/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        }



        public async Task<ActionResult> ListFront()
        {
            {

                List<PostForum> events = new List<PostForum>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    HttpResponseMessage Res = await client.GetAsync("AllPost");
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        events = JsonConvert.DeserializeObject<List<PostForum>>(EmpResponse);

                    }
                    //returning the employee list to view  
                    return View(events);

                }
            }
        }



        public async Task<ActionResult> List(int id)
        {
            {

                List<Comment> events = new List<Comment>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    HttpResponseMessage Res = await client.GetAsync("retrieve-all-Comment/"+id);
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        events = JsonConvert.DeserializeObject<List<Comment>>(EmpResponse);

                    }
                    //returning the employee list to view  
                    ViewBag.result = id;
                    return View(events);

                }
            }
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.result = id;
            return View("Create");
        }
        [HttpPost]
        // GET: Event/Create
        public ActionResult Create(Comment cm,int id)
        {
            HttpClient Client = new HttpClient();
            Client.PostAsJsonAsync<Comment>("http://localhost:8080/SpringMVC/servlet/add-Comment/"+id, cm).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("List", "Comment", new { id = id });
        }


        public ActionResult Delete(int id,int ide)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync("http://localhost:8080/SpringMVC/servlet/remove-Comment/" + id).Result;

            return RedirectToAction("List", "Comment", new { id = ide });
        }

        public async Task<Comment> findCommentById(int id)
        {
            {

                Comment c = new Comment();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    HttpResponseMessage Res = await client.GetAsync("retrieve-Comment/" + id);
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        c = JsonConvert.DeserializeObject<Comment>(EmpResponse);

                    }
                    //returning the employee list to view  
                    return c;

                }
            }
        }


        public async Task<ActionResult> Edit(int id)
        {
            Comment e = new Comment();
            e = await findCommentById(id);
            return View("Edit", e);
        }

        // POST: Commentaire/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Comment cmt,int ide)
        {
            try
            {

                ViewBag.result = ide;
                HttpClient Client = new System.Net.Http.HttpClient();
                Client.BaseAddress = new Uri(baseAddress);
                Client.PutAsJsonAsync<Comment>("http://localhost:8080/SpringMVC/servlet/modify-Comment/" + id, cmt).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("List", "Comment", new { id = ide });
            }
            catch
            {
                return View("List", "Comment", new { id = ide });
            }
        }


    }
}