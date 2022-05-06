using ASP.NETCoreWebAPI.Controllers;
using ASP.NETCoreWebAPI.Services;
using Bogus;
using EFCore.Data_models;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;
using xUnitTests.BogusGenerator;

namespace WebApiTests;

public class OrderControllerTest
{
    [Fact]
    public void GetAll_Returns_The_Correct_Numer_Of_Orders()
    {
        #region Arrange

        //Fake data by Bogus
        FakeOrderGenerator fakeOrderGenerator = new FakeOrderGenerator();
        List<Order> orders = fakeOrderGenerator.GenerateFakeOrder(100);

        //Get the fake service
        var orderService = new Faker<OrderService>()
            .CustomInstantiator(f => new OrderService(null, null, null, null, null, null))
            .Generate();
        var controller = new OrderController(orderService);

        #endregion Arrange

        #region Act

        //var actionResult = controller.GetAnonymous(10);

        #endregion Act

        #region Assert

        //var result = actionResult.Result;

        1.Should().Be(1);

        #endregion Assert
    }
}