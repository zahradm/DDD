namespace Order;

public class Payment
{
}

public interface IDomainEvent
{
}

public interface IStoreConfig
{
    Money MinimumOrderAmount { get; set; }
}

public interface IOrderService
{
    IStoreConfig GetConfig(int storeId);
}

public record PaymentSettled(int PaymentId, Money Amount) : IDomainEvent;

public class Order
{
    public string State { get; set; }
    private List<OrderItem> items = new();
    public int Version { get; set; }
    public IReadOnlyList<OrderItem> Items => items;

    public void Modify(int index, int newCount)
    {
        if (newCount < 1) throw new Exception();

        Items[index].Count = newCount;
    }

    public void AddItems(OrderItem orderItem)
    {
        items.Add(orderItem);
    }

    public void RemoveItem(int index, IOrderService orderService)
    {

        GuardAgainstMinimumPrice(
            items.Where((it, idx) => idx != index),
            orderService.GetConfig(1).MinimumOrderAmount.Value
        );

        items.RemoveAt(index);
    }

    private void GuardAgainstMinimumPrice(IEnumerable<OrderItem> items, decimal minimumAmount)
    {
        if (items.Sum(it => it.Price.Value * it.Count) < minimumAmount)
            throw new Exception();
    }
}

public class OrderItem
{
    public OrderItem(int productId, int count, Money price)
    {
        ProductId = productId;
        if (count < 1) throw new Exception();
        Count = count;
        Price = price;
    }

    public int ProductId { get; internal set; }
    public int Count { get; internal set; }
    public Money Price { get; internal set; }
}

public class Money
{
    public string Currency { get; set; }
    public decimal Value { get; set; }

    public Money(string currency, decimal value)
    {
        Currency = currency;
        Value = value;
    }
}
