using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWedding.Models;
using MyWedding.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWedding.Controllers
{
    

    public class HomeController : Controller
    {
        private IEmailService _emailService;

        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

     
        public async Task<IActionResult> RSVP(RSVP model)
        {

            if(ModelState.IsValid)
            {
                string email = "dewit.ivan777@gmail.com";
                string subject = "Mone & Ivan's wedding";

                string message = "Test";

                await _emailService.SendEmail(email, subject, message);

                return View("~/Views/Home/Index.cshtml", model);
            }
            else
            return View("~/Views/Home/Index.cshtml",model);

        }
    }
}
