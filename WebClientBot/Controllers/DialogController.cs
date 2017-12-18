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
        [HttpGet]
        public ActionResult OpenDialog(int id)
        {
            if (dialog == null || dialog.Id != id)
                SetDialog(id);
            queisions = dialog == null ? null : queisions;
            answers = answers ?? new Dictionary<Question, string>();
            if (queisions != null && queisions.Count < dialog.Questions.Count)
                queisions.Add(GetNextQuestion(dialog.Questions.ToList(), queisions.Last()));

            queisions = queisions ?? new List<Question>() { dialog.Questions.FirstOrDefault() };
            ViewBag.Answers = answers;
            ViewBag.Flag = answers?.Count != dialog.Questions.Count;
            return View(queisions);
        }

        void SetDialog(int id)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string adress = "/api/Dialogs/" + id;

            HttpResponseMessage response = client.GetAsync(adress).Result;


            if (response.IsSuccessStatusCode)
            {
                dialog = response.Content.ReadAsAsync<Dialog>().Result;
            }
            else
            {
                ViewBag.Error = "Error";
            }
            queisions = null;
            answers = null;
        }


        [HttpPost]
        public RedirectResult OpenDialog(string answer)
        {
            answers[queisions.Last()] = answer;
            return Redirect("/Dialog/OpenDialog/"+dialog.Id);
        }

        [HttpPost]
        public RedirectResult EndDialog()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            foreach(KeyValuePair<Question,string> keyValue in answers)
            {
                QuestionsResult questionsResult = new QuestionsResult() { Question = keyValue.Key.Body,
                    Answer = keyValue.Value, DialogId = dialog.Id
                };
                HttpResponseMessage response = client.PostAsJsonAsync("api/QuestionsResults/",questionsResult).Result;
            }

            dialog = null;
            queisions = null;
            answers = null;

            return Redirect("/Dialog/BeforeDialog");
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
            return RedirectToAction("OpenDialog", new { id = res.Id });
            

        }

        [HttpGet]
        public ActionResult BeforeDialog()
        {
            return View();
        }
    }
}