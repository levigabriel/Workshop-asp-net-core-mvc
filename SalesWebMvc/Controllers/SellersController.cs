using System;
using System.Collections.Generic;
using System.Linq;
using SalesWebMvc.Services;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellersService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellersService, DepartmentService departmentService)
        {
            _sellersService = sellersService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellersService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var ViewModel = new SellerFormViewModel { Departments = departments };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellersService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delet (int? id)
        {
            if (id == null)
            {
                // NotFound: Instancia uma resposta básica
                return NotFound();
            }

            // Como o id é opcional, é necessário colocar o Value
            var obj = _sellersService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
    }
}