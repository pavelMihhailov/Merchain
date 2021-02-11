namespace Merchain.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Merchain.Common;
    using Merchain.Common.Extensions;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Web.ViewModels.ShoppingCart;
    using Microsoft.AspNetCore.Http;

    public class CartService : ICartService
    {
        private readonly IProductsService productsService;

        public CartService(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<Task> AddToCart(ISession session, int id)
        {
            var product = await this.productsService.GetByIdAsync(id);

            if (product != null)
            {
                IEnumerable<CartItem> cartItems = SessionExtension.Get<List<CartItem>>(session, SessionConstants.Cart) ?? new List<CartItem>();

                var productInCart = cartItems.FirstOrDefault(x => x.Product.Id == id);

                if (productInCart != null)
                {
                    productInCart.Quantity++;
                }
                else
                {
                    var cartItem = new List<CartItem>() { new CartItem { Product = product, Quantity = 1 } };
                    cartItems = cartItems.Concat(cartItem);
                }

                SessionExtension.Set(session, SessionConstants.Cart, cartItems);
            }

            return Task.CompletedTask;
        }

        public void RemoveFromCart(ISession session, int id)
        {
            var cart = SessionExtension.Get<List<CartItem>>(session, SessionConstants.Cart);

            if (cart != null)
            {
                int productListId = this.GetProductListId(session, id);

                cart.RemoveAt(productListId);

                SessionExtension.Set(session, SessionConstants.Cart, cart);
            }
        }

        public HttpStatusCode DecreaseQuantity(ISession session, int id)
        {
            var cartItems = SessionExtension.Get<List<CartItem>>(session, SessionConstants.Cart) ?? new List<CartItem>();

            var productInCart = cartItems.FirstOrDefault(x => x.Product.Id == id);

            if (productInCart != null)
            {
                if (productInCart.Quantity == 1)
                {
                    int productListId = cartItems.IndexOf(productInCart);
                    cartItems.RemoveAt(productListId);
                }
                else
                {
                    productInCart.Quantity--;
                }
            }

            SessionExtension.Set(session, SessionConstants.Cart, cartItems);

            return HttpStatusCode.OK;
        }

        public void EmptyCart(ISession session)
        {
            SessionExtension.Set(session, SessionConstants.Cart, new List<CartItem>());
        }

        public int GetCartItemsCount(ISession session)
        {
            var cart = SessionExtension.Get<List<CartItem>>(session, SessionConstants.Cart);

            return cart != null ? cart.Count : 0;
        }

        private int GetProductListId(ISession session, int id)
        {
            var cart = SessionExtension.Get<List<CartItem>>(session, SessionConstants.Cart);

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
