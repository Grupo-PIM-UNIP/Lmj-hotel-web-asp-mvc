using LmjHotelWebApplication.Models;
using LmjHotelWebApplication.Models.ViewModels;
using LmjHotelWebApplication.Services.Contratos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            return RedirectToAction(nameof(Login));
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

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginHospedeViewModel loginHospede)
        {
            var hospede = await _hospedeService.BuscaPorEmail(loginHospede.Email);
            var validaHospede = await _hospedeService.ValidarAcesso(hospede.Id, loginHospede.Email, loginHospede.Senha);

            if (hospede != null && validaHospede)
            {
                var usuarioLogado = new ClaimsIdentity("cookies");
                usuarioLogado.AddClaim(new Claim(ClaimTypes.NameIdentifier, hospede.Id.ToString()));
                usuarioLogado.AddClaim(new Claim(ClaimTypes.Name, hospede.Nome));

                await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(usuarioLogado));
                return RedirectToAction(nameof(Success));
            }
            return NotFound();
        }

        public async Task<IActionResult> RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RedefinirSenha(ResetaSenhaViewModel newPassword)
        {
            var hospede = await _hospedeService.BuscaPorEmail(newPassword.Email);
            if (hospede != null)
            {
                await _hospedeService.RedefinirSenha(hospede, newPassword.Senha);
                return RedirectToAction(nameof(Success));
            }
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
