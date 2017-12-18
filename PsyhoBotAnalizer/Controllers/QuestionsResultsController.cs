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
    public class QuestionsResultsController : ApiController
    {
        private MainDBContext db = new MainDBContext();

        // GET: api/QuestionsResults
        public IQueryable<QuestionsResult> GetQuestionsResults()
        {
            return db.QuestionsResults;
        }

        // GET: api/QuestionsResults/5
        [ResponseType(typeof(QuestionsResult))]
        public IHttpActionResult GetQuestionsResult(int id)
        {
            QuestionsResult questionsResult = db.QuestionsResults.Find(id);
            if (questionsResult == null)
            {
                return NotFound();
            }

            return Ok(questionsResult);
        }

        // PUT: api/QuestionsResults/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestionsResult(int id, QuestionsResult questionsResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questionsResult.Id)
            {
                return BadRequest();
            }

            db.Entry(questionsResult).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsResultExists(id))
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

        // POST: api/QuestionsResults
        [ResponseType(typeof(QuestionsResult))]
        public IHttpActionResult PostQuestionsResult(QuestionsResult questionsResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.QuestionsResults.Add(questionsResult);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = questionsResult.Id }, questionsResult);
        }

        // DELETE: api/QuestionsResults/5
        [ResponseType(typeof(QuestionsResult))]
        public IHttpActionResult DeleteQuestionsResult(int id)
        {
            QuestionsResult questionsResult = db.QuestionsResults.Find(id);
            if (questionsResult == null)
            {
                return NotFound();
            }

            db.QuestionsResults.Remove(questionsResult);
            db.SaveChanges();

            return Ok(questionsResult);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionsResultExists(int id)
        {
            return db.QuestionsResults.Count(e => e.Id == id) > 0;
        }
    }
}