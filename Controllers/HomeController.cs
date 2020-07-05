using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Top_Records.Models;

namespace Top_Records.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecordsRepository _topRecordsRepository;
        private static IList<Record> TopRecordsList;

        public HomeController(IRecordsRepository topListRepository)
        {
            _topRecordsRepository = topListRepository;
            TopRecordsList = _topRecordsRepository.GetRecords();
        }

        [AllowAnonymous]
        public ViewResult Index()
        { 
            return View(TopRecordsList.Where(y => y.Approved == true).OrderBy(y => y.Time));
        }

        [AllowAnonymous]
        public PartialViewResult TopRecords()
        {
            return PartialView(TopRecordsList.Where(y => y.Approved == true).OrderBy(y => y.Time));
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult AddRecord()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddRecord(Record record)
        {
            if (ModelState.IsValid)
            {
                TopRecordsList = _topRecordsRepository.AddRecord(record);
                Thread.Sleep(5000);
                return RedirectToAction("Index","Home");
            }
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }
            return View();
        }

        [Authorize]
        public IActionResult UnapprovedRecords()
        {
            return View(TopRecordsList.Where(y=>y.Approved==false).OrderBy(y=>y.Time));
        }

        [HttpGet]
        [Authorize]
        public IActionResult UpdateRecord(int Id, bool isApproved, string viewName)
        {
            Record updatedRecord = TopRecordsList.FirstOrDefault(x => x.ID == Id);
            updatedRecord.Approved = isApproved;

            if (!isApproved)
            {
                var confirmURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{this.ControllerContext.RouteData.Values["controller"]}/Validation?IsApproved=false&recordID=" + Id;
                _topRecordsRepository.ConfirmationURL(confirmURL, updatedRecord, viewName);
                
            }
            else
            {
                TopRecordsList = _topRecordsRepository.UpdateRecord(updatedRecord);
            }

            return RedirectToAction(viewName,"Home");
        }

        public IActionResult Validation(bool isApproved, int recordID)
        {
            Record updatedRecord = TopRecordsList.FirstOrDefault(x => x.ID == recordID);
            updatedRecord.Approved = isApproved;
            TopRecordsList = _topRecordsRepository.UpdateRecord(updatedRecord);
            return RedirectToAction("Index", "Home");
        }
    }
}
