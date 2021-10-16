using LmjHotelWebApplication.Models;
using LmjHotelWebApplication.Models.Enums;
using LmjHotelWebApplication.Models.ViewModels;
using LmjHotelWebApplication.Services.Contratos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Controllers
{
    [Authorize]
    public class ReservaController : Controller
    {
        private readonly IReservaService _reservaService;
        private readonly IQuartoService _quartoService;
        private readonly IHospedeService _hospedeService;

        public ReservaController(IReservaService reservaService,
            IQuartoService quartoService,
            IHospedeService hospedeService)
        {
            _reservaService = reservaService;
            _quartoService = quartoService;
            _hospedeService = hospedeService;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Cadastrar));
        }

        public async Task<IActionResult> Cadastrar()
        {
            long idHospede = 0;
            foreach (var claim in User.Claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                {
                    idHospede = long.Parse(claim.Value);
                    var hospede = await _hospedeService.BuscaPorId(idHospede);

                    if (hospede == null)
                    {
                        return RedirectToAction(nameof(Error), new
                        {
                            message = "Erro de identificação, Tente novamente mais tarde," +
                            "persistindo o erro, favor entrar em contato com a administração do hotel"
                        });
                    }
                }
            }
            var quartos = await _quartoService.ListarQuartosDisponiveis();
            var formReserva = new ReservaFormViewModel { Id = idHospede, Quartos = quartos };
            return View(formReserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(long? id, ReservaFormViewModel obj)
        {
            var validarReserva = _reservaService.ValidarReserva(obj.Reserva.Inicio, obj.Reserva.Fim);
            if (!validarReserva)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "A data de inicio e fim da reserva tem que ser superior a data atual " +
                    " e a data término não pode ser inferior a data atual"
                });
            }

            try
            {
                var pagamento = new Pagamento()
                {
                    Instante = DateTime.Now,
                    QtdParcelas = obj.Pagamento.QtdParcelas
                };

                var reserva = new Reserva()
                {
                    Inicio = obj.Reserva.Inicio,
                    Fim = obj.Reserva.Fim,
                    HospedeId = id.Value,
                    QuartoId = obj.Reserva.QuartoId,
                    Pagamento = pagamento
                };

                await _quartoService.Alugar(reserva.QuartoId);
                await _reservaService.SalvarPagamento(pagamento);
                await _reservaService.SalvarReserva(reserva);
                return RedirectToAction(nameof(Success));
            }

            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
