using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using InvoiceTest2.Models;

namespace InvoiceTest2.Controllers
{
    public class JobCardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobCards
        public ActionResult Index()
        {
            return View(db.Stocks.ToList());
        }

        public ActionResult Invoice (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCard jobCard = db.Stocks.Find(id);
            if (jobCard == null)
            {
                return HttpNotFound();
            }
            return View(jobCard);
        }

        // GET: JobCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCard jobCard = db.Stocks.Find(id);
            if (jobCard == null)
            {
                return HttpNotFound();
            }
            return View(jobCard);
        }

        // GET: JobCards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,PartsUsed,FeesIncluded,TotalAmount,PhoneNo,Email")] JobCard jobCard)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                WebMail.EnableSsl = true;
                WebMail.UserName = "specialisedscannedsolutions@gmail.com";
                WebMail.Password = "Solution@1";
                WebMail.From = "specialisedscannedsolutions@gmail.com";
                WebMail.Send(to: jobCard.Email,
                             subject: "Good day Customer",
                             body: "Your Invoice has been successfully completed." + "<br/>" + "Thank You for purchasing at Specialised Scanned Solutions." + "<br/>" +
                             "Parts Used:  " + jobCard.PartsUsed + "<br/>"+
                             "Fees Included:  R" + jobCard.FeesIncluded + "<br/>" +
                             "Total Amount:  R" + jobCard.TotalAmount + "<br/>" +
                             "Phone Number:  " + jobCard.PhoneNo + "<br/>" +
                             "Email Address:  " + jobCard.Email,
                             isBodyHtml: true);

            }
            catch (Exception)
            {

            }

            if (ModelState.IsValid)
            {
                db.Stocks.Add(jobCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobCard);
        }

        // GET: JobCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCard jobCard = db.Stocks.Find(id);
            if (jobCard == null)
            {
                return HttpNotFound();
            }
            return View(jobCard);
        }

        // POST: JobCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobID,PartsUsed,FeesIncluded,TotalAmount,PhoneNo,Email")] JobCard jobCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobCard);
        }

        // GET: JobCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCard jobCard = db.Stocks.Find(id);
            if (jobCard == null)
            {
                return HttpNotFound();
            }
            return View(jobCard);
        }

        // POST: JobCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobCard jobCard = db.Stocks.Find(id);
            db.Stocks.Remove(jobCard);
            db.SaveChanges();
            return RedirectToAction("Index");
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
