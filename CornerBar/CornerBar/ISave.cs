using System.IO;
using System.Threading.Tasks;

namespace CornerBar
{
    public interface ISave
    {
        Task SaveTextAsync(string filename, string contentType, MemoryStream stream);
    }
}
