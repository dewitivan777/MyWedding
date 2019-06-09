using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Email.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWedding.Models;
using MyWedding.Repository;


namespace MyWedding.Controllers
{
    [Authorize]
    public class GuestController : Controller
    {
        private readonly IGuestRepository<Guest> _guestRepository;
        private readonly IEmailService _messageService;
        private readonly UserManager<IdentityUser> _userManager;

        public GuestController(IGuestRepository<Guest> guestRepository, IEmailService messageService, UserManager<IdentityUser> userManager)
        {
            _guestRepository = guestRepository;
            _messageService = messageService;
            _userManager = userManager;
        }

        [Route("~/Guest")]
        public IActionResult ShowGrid()
        {
            return View("~/Views/Guest/list.cshtml");
        }

        public async Task<IActionResult> LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)
                var searchValue = Request.Form["search[value]"].FirstOrDefault().ToLower();

                //Paging Size (10,20,50,100)
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data
                var customerData = await _guestRepository.ListAsync();


                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData.AsQueryable().OrderBy(x => x.Surname);
                }
                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.Name.ToLower().Contains(searchValue) || m.Surname.ToLower().Contains(searchValue) || m.Email.ToLower().Contains(searchValue));
                }

                //total number of rows count 
                recordsTotal = customerData.Count();
                //Paging 
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [Route("~/Guest/add")]
        public async Task<IActionResult> add([FromForm]Guest model, int page = 1)
        {
            if (ModelState.IsValid)
            {
                _guestRepository.Add(model);

                return Json(new { success = true });
            }

            return PartialView("~/Views/Guest/_GuestAdd.cshtml", model);
        }

        [HttpGet]
        [Route("~/Guest/edit/{id}")]
        public IActionResult edit(string id)
        {
            var entity = _guestRepository.GetByIdAsync(id);

            var model = new Guest()
            {
                id = entity.Result.id,
                Name= entity.Result.Name,
                Surname = entity.Result.Surname,
                Email = entity.Result.Email,
                mobile = entity.Result.mobile,
                IsAttending= entity.Result.IsAttending
            };


            return PartialView("_GuestEdit", model);
        }

        [HttpPost]
        [Route("~/Guest/edit")]
        public IActionResult edit(Guest model)
        {

            if (ModelState.IsValid)
            {
                _guestRepository.Update(model);
                return Json(new { success = true });

            }

            return PartialView("_GuestEdit", model);
        }

        [HttpPost]
        [Route("~/Guest/delete")]
        public async Task<IActionResult> delete(string deleteID)
        {
            _guestRepository.Delete(deleteID);

            return RedirectToAction("ShowGrid");
        }

        [HttpPost]
        [Route("~/Guest/email")]
        public async Task<IActionResult> email(string emailID, string subject, string message, FormFile attach)
        {

            var result = await _guestRepository.GetEmail(c => c.id == emailID);

            await _messageService.SendEmail(result, subject, message);

            return RedirectToAction("ShowGrid");
        }

        [HttpPost]
        [Route("~/Guest/emailAll")]
        public async Task<IActionResult> emailAll(string emailID, string subject, string message, FormFile attach)
        {

            var result = await _guestRepository.GetEmail();

            await _messageService.SendEmail(result, subject, message);

            return RedirectToAction("ShowGrid");
        }

        [HttpGet]
        [Route("~/Guest/details")]
        public IActionResult details()
        {
            var entities = _guestRepository.ListAsync().Result;

            var model = new Details()
            {
                Total = entities.Count(),
                Attending = entities.Where(c => c.IsAttending == true).Count(),
                NotAttending = entities.Where(c => c.IsAttending == false).Count()
            };


            return PartialView("_GuestDetails", model);
        }
    }
}