using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IErrorLogRepository _errorLogRepository;

        public ContactController(IContactRepository contactRepository, IErrorLogRepository errorLogRepository)
        {
            _contactRepository = contactRepository;
            _errorLogRepository = errorLogRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<Contact> contacts = await _contactRepository.GetAllAsync();

                return View(contacts);
            }
            catch (Exception e)
            {
                await _errorLogRepository.SaveExceptionAsync("ContactController", "Index", e);
                TempData["Danger"] = "Erro ao tentar acessar a página, entre em contato com o administrador";
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            try
            {
                bool phoneNumberValidation = contact.IsValidPhoneNumber();

                if (phoneNumberValidation == false)
                {
                    ModelState.AddModelError("PhoneNumber", "Formato de telefone inválido, tente outro formato, para fixo: (dd)0000-0000 ou para celular (dd)90000-0000");
                }

                if (!ModelState.IsValid)
                {
                    return View(contact);
                }

                bool created = await _contactRepository.CreateAsync(contact);

                if (created)
                {
                    TempData["Success"] = "Contato cadastrado com sucesso";
                }
                else
                {
                    TempData["Danger"] = "Não foi possível cadastrar o contato, verifique se o contato já não existe, buscando por nome e telefone";
                }
            }
            catch (Exception e)
            {
                await _errorLogRepository.SaveExceptionAsync("ContactController", "Create", e);
                TempData["Danger"] = "Erro ao tentar acessar a página, entre em contato com o administrador";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                Contact contact = await _contactRepository.GetByIdAsync(id);

                return View(contact);
            }
            catch (Exception e)
            {
                await _errorLogRepository.SaveExceptionAsync("ContactController", "Edit", e);
                TempData["Danger"] = "Erro ao tentar acessar a página, entre em contato com o administrador";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            try
            {
                bool phoneNumberValidation = contact.IsValidPhoneNumber();

                if (phoneNumberValidation == false)
                {
                    ModelState.AddModelError("PhoneNumber", "Formato de telefone inválido, tente outro formato, para fixo: (dd)0000-0000 ou para celular (dd)90000-0000");
                }

                if (!ModelState.IsValid)
                {
                    return View(contact);
                }

                bool updated = await _contactRepository.UpdateAsync(contact);

                if (updated)
                {
                    TempData["Info"] = "Contato atualizado com sucesso";
                }
                else
                {
                    TempData["Danger"] = "Não foi possível atualizar o contato, verifique se ele realmente existe";
                }
            }
            catch (Exception e)
            {
                await _errorLogRepository.SaveExceptionAsync("ContactController", "Edit", e);
                TempData["Danger"] = "Erro ao tentar acessar a página, entre em contato com o administrador";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Contact contact = await _contactRepository.GetByIdAsync(id);

                return PartialView("_Delete", contact);
            }
            catch (Exception e)
            {
                await _errorLogRepository.SaveExceptionAsync("ContactController", "Delete", e);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Contact contact)
        {
            try
            {
                bool deleted = await _contactRepository.DeleteAsync(contact.Id);

                if (deleted)
                {
                    TempData["Warning"] = "Contato deletado sucesso";
                }
                else
                {
                    TempData["Danger"] = "Não foi possível deletar o contato, verifique se ele realmente existe";
                }
            }
            catch (Exception e)
            {
                await _errorLogRepository.SaveExceptionAsync("ContactController", "Delete", e);
                TempData["Danger"] = "Erro ao tentar acessar a página, entre em contato com o administrador";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
