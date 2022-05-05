using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<Contact> contacts = await _contactRepository.GetAllAsync();

            return View(contacts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
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

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Contact contact = await _contactRepository.GetByIdAsync(id);

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
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

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Contact contact = await _contactRepository.GetByIdAsync(id);

            return PartialView("_Delete", contact);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Contact contact)
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

            return RedirectToAction(nameof(Index));
        }
    }
}
