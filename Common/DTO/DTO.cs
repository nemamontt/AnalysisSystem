using Common.Enums.ForSolution;

namespace Common.DTO
{
    public class DTO<T>
    {
        public FileType? Type { get; set; }
        public T? Value { get; set; }
    }
}