using LmjHotelWebApplication.Models;
using LmjHotelWebApplication.Services.Contratos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Controllers
{
    public class HospedeController : Controller
    {
        // Injentando a dependência com a interface IHospedeService do pacote de Services
        private readonly IHospedeService _hospedeService;

        public HospedeController(IHospedeService hospedeService)
        {
            _hospedeService = hospedeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Hospede hospede)
        {
            await _hospedeService.Cadastrar(hospede);
            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
