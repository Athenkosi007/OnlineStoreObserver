using System;
using System.Collections.Generic;

// Subject (Publisher)
class OnlineStore
{
    private List<IObserver> observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void PurchaseProduct(string productName, int quantity)
    {
        Console.WriteLine($"\nProduct purchased: {productName} (Qty: {quantity})");
        NotifyObservers(productName, quantity);
    }

    private void NotifyObservers(string productName, int quantity)
    {
        foreach (var observer in observers)
            observer.Update(productName, quantity);
    }
}

// Observer Interface
interface IObserver
{
    void Update(string productName, int quantity);
}

// Concrete Observers
class EmailNotifier : IObserver
{
    public void Update(string productName, int quantity)
    {
        Console.WriteLine($"[Email] Confirmation sent for {quantity}x {productName}");
    }
}

class InventorySystem : IObserver
{
    public void Update(string productName, int quantity)
    {
        Console.WriteLine($"[Inventory] Deducted {quantity}x {productName} from stock");
    }
}

class Logger : IObserver
{
    public void Update(string productName, int quantity)
    {
        Console.WriteLine($"[Log] Purchase logged: {quantity}x {productName}");
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        // Create subject
        OnlineStore store = new OnlineStore();

        // Create observers
        EmailNotifier emailNotifier = new EmailNotifier();
        InventorySystem inventorySystem = new InventorySystem();
        Logger logger = new Logger();

        // Subscribe observers
        store.Attach(emailNotifier);
        store.Attach(inventorySystem);
        store.Attach(logger);

        // Simulate purchases
        store.PurchaseProduct("Laptop", 1);
        store.PurchaseProduct("Headphones", 2);

        // Unsubscribe email notifications
        store.Detach(emailNotifier);

        // Another purchase
        store.PurchaseProduct("Mouse", 3);
    }
}
