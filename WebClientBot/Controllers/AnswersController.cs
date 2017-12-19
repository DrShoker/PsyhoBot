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
    public class AnswersController : Controller
    {
        // GET: Answers
        public ActionResult Index()
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Answers").Result;

            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Answer>>().Result;
            }
            else
            {
                ViewBag.result = "Error";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Add(int id = 0)
        {

            return View(new Answer());
        }
        [HttpPost]
        public ActionResult Add(Answer anw)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/Answers", anw).Result;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)

        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string adress = "/api/Answers/" + id;

            HttpResponseMessage response = client.DeleteAsync(adress).Result;

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string adress = "/api/Answers/" + id;

            HttpResponseMessage response = client.GetAsync(adress).Result;

            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<Answer>().Result;
            }
            else
            {
                ViewBag.result = "Error";
            }

            return View("Edit");
        }


        [HttpPost]
        public ActionResult Edit(int id, Answer anw)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string adress = "/api/Answers/" + id;

            HttpResponseMessage response = client.PutAsJsonAsync(adress, anw).Result;

            return RedirectToAction("Index");

        }
    }
}