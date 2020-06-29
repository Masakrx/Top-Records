using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Top_lista_vremena.Models;

namespace Top_lista_vremena.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ITopListRepository _topListRepository;
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private static IList<TopTime> TopTimes;

        public HomeController(IConfiguration config, ITopListRepository topListRepository)
        {
            this.configuration = config;
            connectionString = configuration.GetConnectionString("TopListConnection");
            _topListRepository = topListRepository;
            TopTimes = _topListRepository.GetTopList();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TopListView()
        {
            return PartialView("TopListView", TopTimes.OrderBy(x => x.Time));
        }

        public IActionResult NewRecord()
        {
            return View("NewRecordView");
        }

        public IActionResult AddRecord(TopTime record)
        {
            _topListRepository.AddTopListRecord(record);

            return View("Index");
        }

        [Authorize]
        public IActionResult NewRecordsList()
        {
            return View("newRecordsListView");
        }

    }
}
