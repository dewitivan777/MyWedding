using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Email.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyWedding.Models;
using MyWedding.Repository;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWedding.Controllers
{
    

    public class HomeController : Controller
    {
        private IEmailService _emailService;
        private readonly IGuestRepository<Guest> _guestRepository;
        private readonly IHostingEnvironment _env;

        public HomeController(IEmailService emailService, IGuestRepository<Guest> guestRepository, IHostingEnvironment env)
        {
            _emailService = emailService;
            _guestRepository = guestRepository;
            _env = env;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.language = "Afrikaans";
            return View();
        }


        [HttpPost]
        public IActionResult Index(string language)
        {
            ViewBag.language = language;
            return View();
        }


        [HttpPost]
        [Route("~/Home/RSVP/{language}")]
        public async Task<IActionResult> RSVP(Guest model, string language)
        {
            ViewBag.language = language;
            if (ModelState.IsValid)
            {
                try { 
                var entity = await _guestRepository.GetByNameAsync(model.Name,model.Surname);

                if(entity != null)
                {
                    var Updateentity = await _guestRepository.GetByIdAsync(entity.id);
                    _guestRepository.Update(Updateentity);
                }
                else
                {
                    _guestRepository.Add(model);
                }

                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Could not connect to database");
                }
                List<string> email = new List<string>();
                string attending = "";
                if (model.IsAttending == true)
                    attending = "attending";
                else
                    attending = "not attending";

               //To Me
                email.Add("dewit.troue@gmail.com");
                string subject = "Mone & Ivan's wedding";
                string stringformat = @"The following person is {0} your wedding:

Name & Surname: {1}
Email:{2}
Contact number:{3}
Personal message:{4}";
                String message = string.Format(stringformat,attending,model.Name +" "+model.Surname,model.Email,model.mobile,model.Message);
                await _emailService.SendEmail(email, subject, message);

                //To Guest
                email.Clear();
                email.Add(model.Email);
                 subject = "Mone & Ivan's wedding RSVP";
                stringformat = @"You're {0} the wedding";
                message = string.Format(stringformat, attending);
                await _emailService.SendEmail(email, subject, message, _env.WebRootPath +"/content/images/IMG_6228.jpg");
                return Json(new { success = true }); 
            }
            else
            return View("~/Views/Home/Index.cshtml",model);

        }
    }
}
