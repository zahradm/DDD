namespace Client;

using Order;

public class Client
{
    public Address DeliverAddress { get, internal sbyte, }
    public void ModifyOrder(Order order, int newCount)
    {
        order.Modify(0, newCount);
        order.AddItems(new OrderItem(1, newCount, new Money("usdt", 10)));
    }
}

public class Address
{
    public string Name { get; set; }
    public string Value { get; set; }

    public Address(string name, string value)
    {
        Name = name;
        Value = value;
    }
}