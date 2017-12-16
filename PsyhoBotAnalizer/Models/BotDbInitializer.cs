using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PsyhoBotAnalizer.Models
{
    public class BotDbInitializer : DropCreateDatabaseIfModelChanges<MainDBContext>
    {
        protected override void Seed(MainDBContext db)
        {
            Doctor d1 = new Doctor { FullName = "Семен В.В.", Birthday = new DateTime(2012, 1, 1), Sex = "Муж", Category = "Высшая", Charasteristic = "Норм тип" };
            db.Doctors.Add(d1);

            Patient p1 = new Patient { FullName = "Виктор Г.Г.", Birthday = new DateTime(2013, 2, 3), Diagnosis = "Псих", Sex = "Муж.", DiseasePeriod = "4 года", NormalCondition = "Дружелюбный", Characteristic = "Нету", Doctor = d1 };
            db.Patients.Add(p1);

            Dialog dialog1 = new Dialog { Date = new DateTime(2014, 5, 5), Patient = p1 };
            db.Dialogs.AddRange(new List<Dialog> { dialog1 });


            Question q1 = new Question { Body = "Здравствуйте, довайте пройдем тест" };
            q1.Dialogs.Add(dialog1);
            db.Questions.Add(q1);

            Answer a1 = new Answer { QuestionId = 1, Body = "Давайте", Question = q1 };
            db.Answers.Add(a1);

            db.SaveChanges();

            base.Seed(db);
        }
    }
}