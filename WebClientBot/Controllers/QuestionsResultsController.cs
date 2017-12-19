using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebClientBot.Models;

namespace WebClientBot.Controllers
{
    public class QuestionsResultsController : Controller
        {
            public ActionResult Index()
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(UrlContacts.BaseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/QuestionsResults").Result;

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.result = response.Content.ReadAsAsync<IEnumerable<QuestionsResult>>().Result;
                }
                else
                {
                    ViewBag.result = "Error";
                }
                return View();
            }
            [HttpGet]
            public ActionResult Search()
            {

                return View("Index");
            }

            [HttpPost]
            public ActionResult Search(FormCollection fc)
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(UrlContacts.BaseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/QuestionsResults/" + fc["id"]).Result;

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.result = response.Content.ReadAsAsync<Patient>().Result;
                }
                else
                {
                    ViewBag.result = "Error";
                }

                return View("Search");
            }

            public ActionResult Delete(int id)

            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(UrlContacts.BaseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string adress = "/api/QuestionsResults/" + id;

                HttpResponseMessage response = client.DeleteAsync(adress).Result;

                return RedirectToAction("Index");
            }

        //[HttpPost]
        //public ActionResult FindResultForName(string name)
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(UrlContacts.BaseUrl);

        //    client.DefaultRequestHeaders.Clear();

        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    string adress = $"api/QuestionsResults?Name={name}&Id=0";

        //    HttpResponseMessage response = client.GetAsync(adress).Result;

        //    QuestionsResult res = null;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        res = response.Content.ReadAsAsync<QuestionsResult>().Result;

        //        return RedirectToAction("Index");
        //    }

        //        ViewBag.result = "Error";

        //    return View(ViewBag);

        //}

    }
    }