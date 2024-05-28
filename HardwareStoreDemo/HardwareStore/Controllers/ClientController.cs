using FluentValidation;
using FluentValidation.Results;
using HardwareStore.Models;
using HardwareStore.Repositories.Clients;
using HardwareStore.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidator<Client> _validator;

        public ClientController(IClientRepository clientRepository, IValidator<Client> validator)
        {
            _clientRepository = clientRepository;
            _validator = validator;
        }

        // GET: ClientController
        public async Task<ActionResult> Index()
        {
            var client = await _clientRepository.GetAllAsync();

            return View(client);
        }

        // GET:  ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST:  ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Client client)
        {
            try
            {
                ValidationResult validationResult =
                    await _validator.ValidateAsync(client);

                if (!validationResult.IsValid)
                {
                    validationResult.AddToModelState(this.ModelState);

                    return View(client);
                }

                await _clientRepository.AddAsync(client);

                TempData["addClient"] = "Cliente Agregado con exito";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(client);
            }
        }

        // GET: Clientontroller/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
                return NotFound();

            return View(client);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Client client)
        {
            try
            {
                await _clientRepository.EditAsync(client);

                TempData["editClient"] = "Cliente Editado con exito";

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(client);
            }
        }

        // GET: EmployeeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
                return NotFound();

            return View(client);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Client client)
        {
            try
            {
                await _clientRepository.DeleteAsync(client.Id);

                TempData["deleteClient"] = "Cliente Eliminado con exito";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "No se pudo Eliminar el cliente";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
