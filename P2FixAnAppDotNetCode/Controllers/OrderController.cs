﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Services;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICart _cart;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService; // Ajout du service de produit
        private readonly IStringLocalizer<OrderController> _localizer;

        public OrderController(ICart pCart, IOrderService service, IProductService productService, IStringLocalizer<OrderController> localizer)
        {
            _cart = pCart;
            _orderService = service;
            _productService = productService; // Initialisation du service de produit
            _localizer = localizer;
        }

        public ViewResult Index() => View(new Order());

        [HttpPost]
        public IActionResult Index(Order order)
        {
            if (!((Cart)_cart).Lines.Any())
            {
                ModelState.AddModelError("", _localizer["CartEmpty"]);
            }

            if (ModelState.IsValid)
            {
                order.Lines = (_cart as Cart)?.Lines.ToArray();

                // Pour chaque produit dans la commande
                foreach (var line in order.Lines)
                {
                    var product = _productService.GetProductById(line.Product.Id);

                    if (product != null && product.Stock >= line.Quantity)
                    {
                        // Déduire la quantité commandée de la quantité en stock du produit
                       // product.Stock -= line.Quantity; 

                        // Mettre à jour le produit dans la base de données
                        _productService.UpdateProduct(product);
                    }
                    else
                    {
                        // Gérer le cas où la quantité en stock n'est pas suffisante
                        ModelState.AddModelError("", _localizer["NotEnoughStock", product.Name]);
                        return View(order);
                    }
                }

                _orderService.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                // Traduire les erreurs de validation du modèle
                foreach (var key in ModelState.Keys)
                {
                    var modelStateEntry = ModelState[key];
                    var translatedErrors = new List<ModelError>();

                    foreach (var error in modelStateEntry.Errors)
                    {
                        if (!string.IsNullOrEmpty(error.ErrorMessage))
                        {
                            var translatedErrorMessage = _localizer[error.ErrorMessage];
                            var translatedError = new ModelError(translatedErrorMessage);
                            translatedErrors.Add(translatedError);
                        }
                    }

                    // Supprimer les erreurs existantes
                    modelStateEntry.Errors.Clear();

                    // Ajouter les nouvelles erreurs traduites
                    foreach (var translatedError in translatedErrors)
                    {
                        modelStateEntry.Errors.Add(translatedError);
                    }
                }
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}
