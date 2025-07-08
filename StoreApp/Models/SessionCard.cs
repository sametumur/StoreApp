using System.Text.Json.Serialization;
using Entities.Models;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Models;

public class SessionCard : Cart
{

    [JsonIgnore]
    public ISession? Session { get; set; }

    public static SessionCard Get(IServiceProvider serviceProvider)
    {
        ISession? session = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
        SessionCard cart;
        
        if (session == null)
        {
            cart = new SessionCard();
        }
        else
        {
            cart = session.GetObjectMessage<SessionCard>("cart") ?? new SessionCard();
        }
        
        cart.Session = session;
        return cart;
    }

    public override void AddProduct(Product product, int quantity)
    {
        base.AddProduct(product, quantity);
        Session?.AddObjectMessage<SessionCard>("cart", this);
    }
    
    public override void RemoveProduct(Product product)
    {
        base.RemoveProduct(product);
        Session?.AddObjectMessage<SessionCard>("cart", this);
    }
    
    public override void Clear()
    {
        base.Clear();
        Session?.Remove("cart");
    }
}