using Bakdelar.Classes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bakdelar.MethodClasses
{
    public static class SessionMethods
    {

        static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
        };


        public static List<ShoppingBasketItem> GetBasket(this ISession session)
        {
            var sessionBasket = session.GetString("shopping_basket");

            if (sessionBasket == null)
            {
                return null;
            }

            var shoppingbasket = JsonSerializer.Deserialize<List<ShoppingBasketItem>>(sessionBasket, Options);
            return shoppingbasket;

        }

        public static void UpdateShoppingBasket(this ISession session, List<ShoppingBasketItem> shoppingBasket)
        {
            
            session.SetString("shopping_basket", JsonSerializer.Serialize(shoppingBasket, Options));
        }
    }
}
