using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Top_Records.Models;

namespace Top_Records.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IRecordsRepository _topRecordsRepository;
        private static IList<Record> TopRecordsList;

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
                Thread.Sleep(5000);
                return View("Index");
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
        public IActionResult UpdateRecord(int Id, bool isApproved, string viewName)
        {
            TopRecordsList = _topRecordsRepository.UpdateRecord(Id, isApproved, viewName);

            return RedirectToAction(viewName,"Home");
        }
    }
}
