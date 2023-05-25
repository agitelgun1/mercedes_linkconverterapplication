using System.Threading.Tasks;

namespace LinkConverterApplication.Domain.Abstractions
{
    public interface IConvertLinkService
    {
        Task<string> ConvertWebUrlToDeepLinkUrl(string webUrl);
        Task<string> Redirection(string deeplinkUrl);
        Task<string> CustomUrl(string webUrl, string deeplinkUrl);
    }
}