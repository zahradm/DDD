namespace Client;

using Order;

public class Client
{
    public void ModifyOrder(Order order, int newCount)
    {
        order.Modify(0, newCount);
        order.AddItems(new OrderItem(1, newCount, new Money("usdt", 10)));
    }
}
