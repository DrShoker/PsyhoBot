using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PsyhoBotAnalizer.Models;
using System.Data;

namespace BookingApp.Controllers
{
    public class ValuesController : ApiController
    {
        MainDBContext db = new MainDBContext();

        public IEnumerable<Patient> GetPatients()
        {
            return db.Patients;
        }

        public Patient GetPatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            return patient;
        }

        [HttpPost]
        public void CreatePatient([FromBody]Patient patient)
        {
            db.Patients.Add(patient);
            db.SaveChanges();
        }

        [HttpPut]
        public void EditPatient(int id, [FromBody]Patient patient)
        {
            if (id == patient.Id)
            {
                db.Entry(patient).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void DeletePatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient != null)
            {
                db.Patients.Remove(patient);
                db.SaveChanges();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}