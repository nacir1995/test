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
    public class PostForumController : Controller
    {
        // GET: PostForum
        HttpClient httpClient;
        string baseAddress;
        public PostForumController()
        {
            baseAddress = "http://localhost:8081/SpringMVC/servlet/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // var _AccessToken = Session["AccessToken"];
            // httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer{0}",_AccessToken));
        }

        public async Task<ActionResult> List()
        {
            {

                List<PostForum> posts = new List<PostForum>();

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
                        posts = JsonConvert.DeserializeObject<List<PostForum>>(EmpResponse);

                    }
                    //returning the employee list to view  
                    return View(posts);

                }
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        // GET: Event/Create
        public ActionResult Create(PostForum post)
        {
            HttpClient Client = new HttpClient();
            Client.PostAsJsonAsync<PostForum>("http://localhost:8081/SpringMVC/servlet/add-post/", post).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("List", "PostForum");
        }


        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync("http://localhost:8081/SpringMVC/servlet/post-rec/" + id).Result;
            return RedirectToAction("List", "PostForum");
        }


        public async Task<ActionResult> Edit(int id)
        {
            PostForum e = new PostForum();
            e = await findPostById(id);
            return View("Edit", e);
        }


        public async Task<PostForum> findPostById(int id)
        {
            {

                PostForum post = new PostForum();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    HttpResponseMessage Res = await client.GetAsync("retrieve-post/" + id);
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        post = JsonConvert.DeserializeObject<PostForum>(EmpResponse);

                    }
                    //returning the employee list to view  
                    return post;

                }
            }
        }




        // POST: Commentaire/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PostForum cmt)
        {
            try
            {
                HttpClient Client = new System.Net.Http.HttpClient();
                Client.BaseAddress = new Uri(baseAddress);
                Client.PutAsJsonAsync<PostForum>("http://localhost:8081/SpringMVC/servlet/modify-post/" + id, cmt).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("List", "PostForum");
            }
            catch
            {
                return View("List", "PostForum");
            }
        }





















        }
    }