using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Email.Services;
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

        public HomeController(IEmailService emailService, IGuestRepository<Guest> guestRepository)
        {
            _emailService = emailService;
            _guestRepository = guestRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Index(string language)
        {
            ViewBag.language = language;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RSVP(Guest model)
        {

            if(ModelState.IsValid)
            {
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
                List<string> email = new List<string>();
                email.Add("dewit.ivan777@gmail.com");
                string subject = "Mone & Ivan's wedding";
                string message = "Test";
                await _emailService.SendEmail(email, subject, message);

                return Json(new { success = true }); ;
            }
            else
            return View("~/Views/Home/Index.cshtml",model);

        }
    }
}
