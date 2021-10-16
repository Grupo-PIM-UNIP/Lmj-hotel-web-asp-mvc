using LmjHotelWebApplication.Services.Contratos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Controllers
{
    public class ReservaController : Controller
    {
        //private readonly IReservaService _reservaService;
        //private readonly IQuartoService _quartoService;
        //private readonly IPagamentoService _pagamentoService;

        //public ReservaController(IReservaService reservaService,
        //                         IQuartoService quartoService,
        //                         IPagamentoService pagamentoService)
        //{
        //    _reservaService = reservaService;
        //    _quartoService = quartoService;
        //    _pagamentoService = pagamentoService;
        //}

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Cadastrar));
        }

        public IActionResult Cadastrar()
        {
            return View();
        }
    }
}
