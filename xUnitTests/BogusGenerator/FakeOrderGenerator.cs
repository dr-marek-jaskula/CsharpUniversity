using Bogus;
using EFCore.Data_models;
using System;
using System.Collections.Generic;

namespace xUnitTests.BogusGenerator;

public class FakeOrderGenerator
{
    private readonly Faker<Payment> _paymentFaker;
    private readonly Faker<Order> _orderFaker;
    private readonly Faker<Product> _productFaker;
    private readonly string[] _prodcutNames = new[] { "Piano", "Laptop", "Computer", "Table", "Chair", "Doll", "Ring", "Necklace", "Sword", "Notebook", "Clock", "Painting", "Door", "Balloon", "Shoes" };

    public FakeOrderGenerator()
    {
        _paymentFaker = new Faker<Payment>()
            .RuleFor(p => p.Status, f => f.PickRandom<Status>())
            .RuleFor(p => p.Discount, f => Math.Round(f.Random.Decimal(), 2))
            .RuleFor(p => p.Deadline, f => f.Date.Soon(60, DateTime.Now));

        _productFaker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.PickRandom(_prodcutNames))
                .RuleFor(p => p.Price, f => Math.Round(f.Random.Decimal(50, 1000), 2));

        _orderFaker = new Faker<Order>()
            .RuleFor(o => o.Status, f => f.PickRandom<Status>())
            .RuleFor(o => o.Amount, f => f.Random.Int(1, 300))
            .RuleFor(o => o.Deadline, f => f.Date.Soon(60, DateTime.Now))
            .RuleFor(o => o.Payment, f => _paymentFaker.Generate())
            .RuleFor(o => o.Product, f => _productFaker.Generate());
    }

    public List<Order> GenerateFakeOrder(int numberOfFakeOrder)
    {
        return _orderFaker.Generate(numberOfFakeOrder);
    }
}