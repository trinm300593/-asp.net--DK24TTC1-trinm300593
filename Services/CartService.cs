using BubbleTeaCafe.Models;
using BubbleTeaCafe.Models.ViewModels;
using Newtonsoft.Json;

namespace BubbleTeaCafe.Services;

public interface ICartService
{
    CartViewModel GetCart(ISession session);
    void AddToCart(ISession session, CartItem item);
    void RemoveFromCart(ISession session, int cartItemId);
    void UpdateQuantity(ISession session, int cartItemId, int quantity);
    void ClearCart(ISession session);
}

public class CartService : ICartService
{
    private const string CartSessionKey = "Cart";

    public CartViewModel GetCart(ISession session)
    {
        var cartJson = session.GetString(CartSessionKey);
        if (string.IsNullOrEmpty(cartJson))
        {
            return new CartViewModel();
        }

        var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();
        return new CartViewModel { Items = cartItems };
    }

    public void AddToCart(ISession session, CartItem item)
    {
        var cart = GetCart(session);
        
        // Check if item with same specifications already exists
        var existingItem = cart.Items.FirstOrDefault(i => 
            i.ProductId == item.ProductId &&
            i.Size == item.Size &&
            i.Temperature == item.Temperature &&
            i.Sweetness == item.Sweetness);

        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            item.CartItemId = cart.Items.Count > 0 ? cart.Items.Max(i => i.CartItemId) + 1 : 1;
            cart.Items.Add(item);
        }

        SaveCart(session, cart);
    }

    public void RemoveFromCart(ISession session, int cartItemId)
    {
        var cart = GetCart(session);
        var item = cart.Items.FirstOrDefault(i => i.CartItemId == cartItemId);
        
        if (item != null)
        {
            cart.Items.Remove(item);
            SaveCart(session, cart);
        }
    }

    public void UpdateQuantity(ISession session, int cartItemId, int quantity)
    {
        var cart = GetCart(session);
        var item = cart.Items.FirstOrDefault(i => i.CartItemId == cartItemId);
        
        if (item != null)
        {
            if (quantity <= 0)
            {
                cart.Items.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
            SaveCart(session, cart);
        }
    }

    public void ClearCart(ISession session)
    {
        session.Remove(CartSessionKey);
    }

    private void SaveCart(ISession session, CartViewModel cart)
    {
        var cartJson = JsonConvert.SerializeObject(cart.Items);
        session.SetString(CartSessionKey, cartJson);
    }
}
