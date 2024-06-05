namespace CMS.Services
{
    public interface IBaseService<TResponse, TCreateRequest>
    {
        Task<IEnumerable<TResponse>> GetAllAsync();
        Task<TResponse> GetByIdAsync(int id);
        Task<TResponse> CreateAsync(TCreateRequest request);
        Task<TResponse> UpdateAsync(int id, TCreateRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
