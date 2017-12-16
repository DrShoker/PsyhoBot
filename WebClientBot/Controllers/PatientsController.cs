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
    public class PatientsController : Controller
    {



        public ActionResult Index()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Patients").Result;

            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Patient>>().Result;
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

            return View();
        }

        [HttpPost]
        public ActionResult Search(FormCollection fc)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Patients/" +fc["id"]).Result;

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

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {

            return View(new Patient());
        }
        [HttpPost]
        public ActionResult AddOrEdit(Patient pat)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/Patients", pat).Result;

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(Patient p)
        
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string adress = "/api/Patients/" + p.Id;

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

            string adress = "/api/Patients/" + id;

            HttpResponseMessage response = client.GetAsync(adress).Result;

            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<Patient>().Result;
            }
            else
            {
                ViewBag.result = "Error";
            }

            return View("Edit");
        }


        [HttpPost]
        public ActionResult Edit(int id, Patient pat)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string adress = "/api/Patients/" + id;

            HttpResponseMessage response = client.PutAsJsonAsync(adress, pat).Result;

            return RedirectToAction("Index");

        }

        //public ViewResult ViewDelete()
        //{
        //    return View(new Patient());
        //}
    }
}