using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PsyhoBotAnalizer.Models;

namespace PsyhoBotAnalizer.Controllers
{
    public class DialogsController : ApiController
    {
        public static Dialog risingdialog;

        List<int> QuestionsId = new List<int>();

        Question GetNextQuestion(int id)
        {
            bool identify = false;

            foreach(var n in risingdialog.Questions)
            {
                if (identify)
                {
                    return n;
                }
                if(n.Id == id)
                {
                    identify = true;
                }
            }
            return null;
        }
        [HttpGet]
        [ResponseType(typeof(Dialog))]
        public IHttpActionResult GetStartDialog(string Name, int id)
        {
            Patient pat = db.Patients.Where(p => p.FullName == Name).FirstOrDefault();

            if (pat != null)
            {

                Dialog dialog = new Dialog();

                PostDialog(dialog);

                risingdialog = dialog;
            
                dialog.PatientId = pat.Id;

                dialog.Questions.FirstOrDefault();

                return Ok(dialog);
            }

            else
            {
                return NotFound();
            }

        }

        private MainDBContext db = new MainDBContext();

        // GET: api/Dialogs
        [HttpGet]
        public IQueryable<Dialog> GetDialogs()
        {
            return db.Dialogs;
        }

        // GET: api/Dialogs/5
        [HttpGet]
        [ResponseType(typeof(Dialog))]
        public IHttpActionResult GetDialog(int id)
        {
            Dialog dialog = db.Dialogs.Find(id);
            db.Entry(dialog).Collection("Questions").Load();
            foreach(Question q in dialog.Questions)
            {
                db.Entry(q).Collection("Answers").Load();
            }
            if (dialog == null)
            {
                return NotFound();
            }

            return Ok(dialog);
        }

        // PUT: api/Dialogs/5
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDialog(int id, Dialog dialog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dialog.Id)
            {
                return BadRequest();
            }

            db.Entry(dialog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DialogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Dialogs
        [HttpPost]
        [ResponseType(typeof(Dialog))]
        public IHttpActionResult PostDialog(Dialog dialog)
        {
            dialog.Date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (Question q in db.Questions.ToList())
            {
                dialog.Questions.Add(q);

            }

            db.Dialogs.Add(dialog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dialog.Id }, dialog);
        }

        // DELETE: api/Dialogs/5
        [HttpDelete]
        [ResponseType(typeof(Dialog))]
        public IHttpActionResult DeleteDialog(int id)
        {
            Dialog dialog = db.Dialogs.Find(id);
            if (dialog == null)
            {
                return NotFound();
            }

            db.Dialogs.Remove(dialog);
            db.SaveChanges();

            return Ok(dialog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DialogExists(int id)
        {
            return db.Dialogs.Count(e => e.Id == id) > 0;
        }
    }
}