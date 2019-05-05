using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWedding.Models;
using MyWedding.Repository;

namespace MyWedding.Controllers
{

    public class GuestController : Controller
    {
        private readonly GuestRepository<Guest> _guestRepository;

        public GuestController(GuestRepository<Guest> guestRepository)
        {
            _guestRepository = guestRepository;
        }

        [Route("~/Guest")]
        public IActionResult list()
        {
            return View(_guestRepository.ListAsync());
        }

        [Route("~/Guest/Add")]
        public IActionResult add()
        {
            return View();
        }


        [HttpPost]
        [Route("~/Guest/Add")]
        public IActionResult add(Guest model)
        {
            return View();
        }

        [Route("~/Guest/Edit")]
        public IActionResult edit()
        {
            return View();
        }


        [HttpPut]
        [Route("~/Guest/Edit")]
        public IActionResult edit(Guest model)
        {
            return View();
        }

        [Route("~/Guest/Delete")]
        public IActionResult delete()
        {
            return View();
        }


        [HttpDelete]
        [Route("~/Guest/Delete")]
        public IActionResult delete(Guest model)
        {
            return View();
        }
    }
}