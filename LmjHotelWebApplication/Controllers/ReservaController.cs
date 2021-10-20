using LmjHotelWebApplication.Models;
using LmjHotelWebApplication.Models.ViewModels;
using LmjHotelWebApplication.Services.Contratos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LmjHotelWebApplication.Controllers
{
    [Authorize]
    public class ReservaController : Controller
    {

        // Injentando a dependência com os serviços a qual a ReservaController interage no sistema
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
                            message = "Erro de identificação, Tente fazer login novamente," +
                            "caso o erro persista, favor entrar em contato com a administração do hotel"
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
            var validarReserva = _reservaService.ValidarReserva(obj.Reserva.DataInicio, obj.Reserva.DataFim);

            /* Caso as datas da reserva não seja validada, o sistema encaminhara o usuário para a tela
               de cadastro da reserva novamente para que este faça correção nos devidos campos */
            if (!validarReserva)
            {
                /* Caso ocorra algum erro de identificação de usuário nesse processo, o usuário será direcionado 
                   para uma tela de erro, falando do possível problema ocorrido */
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new
                    {
                        message = "Erro de identificação, Tente fazer login novamente," +
                         "caso o erro persista, favor entrar em contato com a administração do hotel"
                    });
                }
                ModelState.AddModelError("", "A data de início e fim da reserva tem que ser superior a data atual " +
                    " e a data final tem que ser superior a data de início");
                var quartos = await _quartoService.ListarQuartosDisponiveis();
                var formReserva = new ReservaFormViewModel { Id = id.Value, Quartos = quartos };
                return View(formReserva);
            }

            try
            {
                var reserva = new Reserva()
                {
                    DataInicio = obj.Reserva.DataInicio,
                    DataFim = obj.Reserva.DataFim,
                    PrecoPorDiaria = obj.Reserva.PrecoPorDiaria,
                    HospedeId = id.Value,
                    QuartoId = obj.Reserva.QuartoId,
                };

                double valorTotal = reserva.CalcularValorTotalDaHospedagem();
                var pagamento = new Pagamento()
                {
                    Instante = DateTime.Now,
                    Valor = (obj.Pagamento.Valor == valorTotal) ? obj.Pagamento.Valor : reserva.CalcularValorTotalDaHospedagem(),
                    QtdParcelas = obj.Pagamento.QtdParcelas,
                    Reserva = reserva
                };

                await _quartoService.Alugar(reserva.QuartoId);
                await _reservaService.SalvarReserva(reserva);
                await _reservaService.SalvarPagamento(pagamento);

                return RedirectToAction(nameof(Success), new { message = "Reserva efetuada com sucesso" });
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Success(string message)
        {
            var successViewModel = new SuccessViewModel
            {
                Message = message,
            };
            return View(successViewModel);
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
