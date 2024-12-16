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
    public async Task<ApiResponse<List<Customer>>> GetAll()
    {
        return await customerService.GetAll();
    }
    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Customer>> GetById(int id)
    {
        return await customerService.GetById(id);
    }
    [HttpPost]
    public async Task<ApiResponse<bool>> Add(Customer customer)
    {
        return await customerService.Add(customer);
    }
    [HttpPut]
    public async Task<ApiResponse<bool>> Update(Customer customer)
    {
        return await customerService.Update(customer);
    }
    [HttpDelete]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await customerService.Delete(id);
    }
}