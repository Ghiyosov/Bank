using Infratructure.Responses;

namespace Infratructure.Services;

public interface IGenericService<T>
{
     Task<ApiResponse<List<T>>> GetAll();
    Task<ApiResponse<T>> GetById(int id);
    Task<ApiResponse<bool>> Add(T data);
    Task<ApiResponse<bool>> Update(T data);
    Task<ApiResponse<bool>> Delete(int id);
}