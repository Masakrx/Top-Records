using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Top_lista_vremena.Models;

namespace Top_lista_vremena.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IRecordsRepository _topRecordsRepository;
        private static IList<Record> TopRecordsList;

        NetworkCredential login = new NetworkCredential("TopRecordsApp@outlook.com", "toprecords123");
        SmtpClient client = new SmtpClient("smtp.office365.com", 587);
        MailMessage message;

        public HomeController(IRecordsRepository topListRepository)
        {
            _topRecordsRepository = topListRepository;
            TopRecordsList = _topRecordsRepository.GetRecords();
        }

        public ViewResult Index()
        {
            return View(TopRecordsList.Where(y => y.Approved == true).OrderBy(y => y.Time));
        }

        public PartialViewResult TopRecords()
        {
            return PartialView(TopRecordsList.Where(y => y.Approved == true).OrderBy(y => y.Time));
        }

        [HttpGet]
        public ViewResult AddRecord()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRecord(Record record)
        {
            if (ModelState.IsValid)
            {
                TopRecordsList = _topRecordsRepository.AddRecord(record);
                EmailData email = new EmailData(record);
                return View("Index");
            }
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }
            return View(record);
        }

        [Authorize]
        public IActionResult UnapprovedRecords()
        {
            return View(TopRecordsList.Where(y=>y.Approved==false));
        }

        public IActionResult UpdateRecord(int Id, bool isApproved, string viewName)
        {
            TopRecordsList = _topRecordsRepository.UpdateRecord(Id, isApproved);

            return View(viewName, TopRecordsList.Where(y => y.Approved == false));
        }

        public IActionResult SendEmail(Record record)
        {
            try
            {
                client.EnableSsl = true;
                client.Credentials = login;
                message = new MailMessage(from: "TopRecordsApp@outlook.com", to: record.Email);
                message.Subject = "Test subject";
                message.Body = "Ovo je testna poruka";
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;
                client.SendAsync(message, "Šaljem...");
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return Json(true);
        }
    }
}
