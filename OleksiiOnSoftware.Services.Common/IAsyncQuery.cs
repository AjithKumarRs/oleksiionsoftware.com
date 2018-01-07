namespace OleksiiOnSoftware.Services.Common
{
    using System.Threading.Tasks;

    public interface IAsyncQuery<T>
    {
        Task<T> ExecuteAsync();
    }
}
