using Domain.Common;

namespace Application.Interfaces.Services;

public interface IConvertFileService<T> where T : FileData
{
    Task<T> ConvertAsync(T file);
}
