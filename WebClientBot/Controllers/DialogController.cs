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
    public class DialogController : Controller
    {
        static List<Question> queisions;
        static Dialog dialog;
        static Dictionary<Question, string> answers;
        // GET: Dialog
        public ActionResult OpenDialog(int id)
        {
            dialog = dialog!=null && dialog.Id == id ? dialog : null;
            queisions = dialog == null ? null : queisions;

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string adress = "/api/Dialogs/" + id;

            HttpResponseMessage response = client.GetAsync(adress).Result;


            if(response.IsSuccessStatusCode)
            {
                dialog = response.Content.ReadAsAsync<Dialog>().Result;
            }
            else
            {
                ViewBag.Error = "Error";
            }
            if (queisions != null && queisions.Count < dialog.Questions.Count)
                queisions.Add(GetNextQuestion(dialog.Questions.ToList(), queisions.Last()));

            queisions = queisions ?? new List<Question>() { dialog.Questions.FirstOrDefault() };
            ViewBag.Answers = answers;
            return View(queisions);
        }

        [HttpPost]
        public RedirectResult OpenDialog(string answer)
        {
            answers = answers ?? new Dictionary<Question, string>();
            answers[queisions.Last()] = answer;
            return Redirect("/Dialog/OpenDialog/"+dialog.Id);
        }

        Question GetNextQuestion(List<Question> list,Question question)
        {
            bool flag = false;
            foreach(var q in list)
            {
                if (flag == true)
                    return q;
                flag = q.Id == question.Id;
            }
            return null;
        }

        [HttpPost]
        public ActionResult BeforeDialog(string Name)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string adress = $"api/Dialogs?Name={Name}&Id=0";

            HttpResponseMessage response = client.GetAsync(adress).Result;

            Dialog res = null;
            if (response.IsSuccessStatusCode)
            {
                res = response.Content.ReadAsAsync<Dialog>().Result;
            }
            else
            {
                ViewBag.result = "Error";
            }
            return RedirectToAction("OpenDialog", new {id = res.Id});

        }

        [HttpGet]
        public ActionResult BeforeDialog()
        {
            return View();
        }
    }
}