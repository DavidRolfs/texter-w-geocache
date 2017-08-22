using Microsoft.AspNetCore.Mvc;
using Texter.Models;


namespace Texter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMessages()
        {
            var allMessages = Message.GetMessages();
            return View(allMessages);
        }

        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message newMessage)
        {
            newMessage.Send();
            return RedirectToAction("Index");
        }


        public IActionResult FindCoords()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FindCoords(double Lat, double Lng)
        {
            //Geocache thisGeocache = new Geocache();
            var foundAddress = Geocache.FindLocation(Lat, Lng);
            return RedirectToAction("Show", foundAddress);
        }

        public IActionResult Show(Geocache geocache)
        {
             
             return View(geocache);
        }

    }
}