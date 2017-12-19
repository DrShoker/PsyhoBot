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
    public class DoctorsController : Controller
    {
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Doctors").Result;

            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Doctor>>().Result;
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

            return View(new Doctor());
        }

        [HttpPost]
        public ActionResult Add(Doctor doc)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/Doctors", doc).Result;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)

        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string adress = "/api/Doctors/" + id;

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

            string adress = "/api/Doctors/" + id;

            HttpResponseMessage response = client.GetAsync(adress).Result;

            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<Doctor>().Result;
            }
            else
            {
                ViewBag.result = "Error";
            }

            return View("Edit");
        }

        [HttpPost]
        public ActionResult Edit(int id, Doctor doc)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string adress = "/api/Doctors/" + id;

            HttpResponseMessage response = client.PutAsJsonAsync(adress, doc).Result;

            return RedirectToAction("Index");

        }

    }
}