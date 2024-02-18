using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode.Models;
using System;

namespace P2FixAnAppDotNetCode.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly ICart _cart;

        public CartSummaryViewComponent(ICart cart)
        {
            _cart = cart ?? throw new ArgumentNullException(nameof(cart));
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}