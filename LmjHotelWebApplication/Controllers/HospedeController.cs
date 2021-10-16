﻿using LmjHotelWebApplication.Models;
using LmjHotelWebApplication.Models.ViewModels;
using LmjHotelWebApplication.Services.Contratos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
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

        public async Task<IActionResult> Editar(long? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var hospede = await _hospedeService.BuscaPorId(id.Value);
            if (hospede == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Hóspede não encontrado" });
            }

            return View(hospede);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Hospede hospede)
        {
            if (id != hospede.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id do hóspede diferente do Id cadastrado" });
            }

            try
            {
                await _hospedeService.AtualizarCadastro(hospede);
                return RedirectToAction(nameof(Success));
            }

            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginHospedeViewModel loginHospede)
        {
            var hospede = await _hospedeService.BuscaPorEmail(loginHospede.Email);
            if (hospede == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Hóspede não encontrado, verifique se você " +
                              " digitou o seu email corretamente"
                });
            }

            var validaHospede = await _hospedeService.ValidarAcesso(hospede.Id, loginHospede.Email, loginHospede.Senha);
            if (!validaHospede)
            {
                ModelState.AddModelError("", "Usuário ou senha inválida!");
                return View();
            }

            var usuarioLogado = new ClaimsIdentity("cookies");
            usuarioLogado.AddClaim(new Claim(ClaimTypes.NameIdentifier, hospede.Id.ToString()));
            usuarioLogado.AddClaim(new Claim(ClaimTypes.Name, hospede.Nome));

            await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(usuarioLogado));
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
            if (hospede == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Email informado para redefinir sua senha não encontra-se cadastrado" });
            }

            await _hospedeService.RedefinirSenha(hospede, newPassword.Senha);
            return RedirectToAction(nameof(Success));
        }

        public async Task<IActionResult> Detalhes()
        {
            long codigoHospede;
            foreach (var claim in User.Claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                {
                    codigoHospede = long.Parse(claim.Value);
                    var hospede = await _hospedeService.BuscaPorId(codigoHospede);

                    if (hospede != null)
                    {
                        return View(hospede);
                    }
                }
            }
            return RedirectToAction(nameof(Error), new
            {
                message = "Falha ao carregar os seus dados, tente novamente mais tarde," +
                " caso o erro persista, favor entrar em contato com a administração da LMJ Hotel"
            });
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
