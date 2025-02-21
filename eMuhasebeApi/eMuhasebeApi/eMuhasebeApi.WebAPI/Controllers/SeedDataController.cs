using Bogus;
using eMuhasebeApi.Application.Features.Customers.GetAllCustomers;
using eMuhasebeApi.Application.Features.Invoices.CreateInvoice;
using eMuhasebeApi.Application.Features.Products.GetAllProducts;
using eMuhasebeApi.Domain.Dtos;
using eMuhasebeApi.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TS.Result;

namespace eMuhasebeApi.WebAPI.Controllers;

public class SeedDataController : ApiController
{
    public SeedDataController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
      

        Random rand = new();
        // #region Customers
        //
        // for (int i = 0; i < 1000; i++)
        // {
        //     Faker faker = new();
        //     int customerTypeValue = rand.Next(1, 5);
        //     CreateCustomerCommand customerCommand = new(
        //         faker.Company.CompanyName(),
        //         customerTypeValue,
        //         faker.Address.City(),
        //         faker.Address.State(),
        //         faker.Address.FullAddress(),
        //         faker.Company.Random.String2(new Random().Next(10, 15)),
        //         faker.Company.Nipc()
        //     );
        //     await _mediator.Send(customerCommand);
        // }
        //
        // #endregion
        //
        // #region Products
        //
        // for (int i = 0; i < 10000; i++)
        // {
        //     Faker faker = new();
        //     CreateProductCommand createProductCommand = new(
        //         faker.Commerce.Product()
        //     );
        //     await _mediator.Send(createProductCommand);
        // }
        //
        // #endregion

        #region Invoices
        var customersResult = await _mediator.Send(new GetAllCustomersQuery());
        var customers = customersResult.Data;
        var productsResult = await _mediator.Send(new GetAllProductsQuery());
        var products = productsResult.Data;
        for (int i = 0; i < 1000; i++)
        {
            Console.WriteLine("On Iteration "+i);
            Faker faker = new();
            if (products is null)
            {
                continue;
            }

            if (customers is null)
            {
                continue;
            }

            List<InvoiceDetailDto> invoiceDetails = new();
            for (int j = 0; j <= rand.Next(1, 11); j++)
            {
                Console.WriteLine(products[j].Id);
                InvoiceDetailDto dto = new()
                {
                    ProductId = products[rand.Next(0, products.Count)].Id,
                    Price = rand.Next(1, 1000),
                    Quantity = rand.Next(1, 100),
                };
                invoiceDetails.Add(dto);
            }

            CreateInvoiceCommand invoice = new(
                rand.Next(1, 3),
                new DateOnly(2024, rand.Next(1, 13), rand.Next(1, 29)),
                faker.Random.String2(16),
                customers[rand.Next(0, customers.Count)].Id,
                invoiceDetails
            );
            await _mediator.Send(invoice);
        }

        #endregion

        return Ok(Result<string>.Succeed("Seed data başarıyla oluşturuldu"));
    }
}