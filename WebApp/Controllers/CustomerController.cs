using Domain.Entities;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController(IGenericService<Customer> customerService) : ControllerBase
{
    [HttpGet]
    public ApiResponse<List<Customer>> GetAll()
    {
        return customerService.GetAll();
    }
    [HttpGet("{id:int}")]
    public ApiResponse<Customer> GetById(int id)
    {
        return customerService.GetById(id);
    }
    [HttpPost]
    public ApiResponse<bool> Add(Customer customer)
    {
        return customerService.Add(customer);
    }
    [HttpPut]
    public ApiResponse<bool> Update(Customer customer)
    {
        return customerService.Update(customer);
    }
    [HttpDelete]
    public ApiResponse<bool> Delete(int id)
    {
        return customerService.Delete(id);
    }
}