using PragmaticAnalyzer.DTO;

namespace PragmaticAnalyzer.Abstractions
{
    public interface IFileService
    {
        Task<T?> LoadDTOAsync<T>(string? path, DataType type);
        Task<T?> LoadFileAsync<T>(string? path);
        Task<bool> SaveDTOAsync<T>(T value, DataType type, string path);
        Task<bool> SaveFileAsync(object value, string path);
    }
}
