using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkConverterApplication.Configuration;
using LinkConverterApplication.Domain.Abstractions;
using LinkConverterApplication.Entities;
using LinkConverterApplication.Repositories;
using Microsoft.Extensions.Options;

namespace LinkConverterApplication.Domain.Services
{
    public class ConvertLinkService : IConvertLinkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static readonly Random Random = new();
        private readonly AppSettings _appSettings;
        public ConvertLinkService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }
        public async Task<string> ConvertWebUrlToDeepLinkUrl(string webUrl)
        {
            var checkUrlIsValid = CheckValidWebUrl(webUrl);
            
            if (!checkUrlIsValid)
            {
                return string.Empty;
            }

            var linkId = RandomString(6);
            var defaultDeepLinkUrl = new StringBuilder(_appSettings.ShortenBaseUrl);
            defaultDeepLinkUrl.Append(linkId);
            
            var webUrlModel = new WebUrl
            {
                Request = webUrl,
                Response = defaultDeepLinkUrl.ToString(),
                IsActive = true,
                LinkId = linkId
            };
            
            await _unitOfWork.WebUrl.DisableDataAsync(webUrlModel);
            await _unitOfWork.WebUrl.InsertAsync(webUrlModel);
            
            return defaultDeepLinkUrl.ToString();
        }

        public async Task<string> Redirection(string deeplinkUrl)
        {
            var checkUrlIsValid = deeplinkUrl.Contains(_appSettings.ShortenBaseUrl);
            
            if (!checkUrlIsValid)
            {
                return string.Empty;
            }
            
            var webUrlModel = new WebUrl
            {
                Response = deeplinkUrl
            };
            
            var response = await _unitOfWork.WebUrl.GetAsync(webUrlModel);
            return response.Request;
        }

        public async Task<string> CustomUrl(string webUrl, string deeplinkUrl)
        {
            var checkUrlIsValid = CheckValidWebUrl(webUrl);
            
            if (!checkUrlIsValid)
            {
                return string.Empty;
            }
            
            var checkDeepUrlIsValid = deeplinkUrl.Contains(_appSettings.ShortenBaseUrl);
            
            if (!checkDeepUrlIsValid)
            {
                return string.Empty;
            }

            var webUrlModel = new WebUrl
            {
                Request = webUrl,
                Response = deeplinkUrl,
                IsActive = true,
                LinkId = string.Empty
            };
            
            await _unitOfWork.WebUrl.DisableDataAsync(webUrlModel);
            await _unitOfWork.WebUrl.InsertAsync(webUrlModel);

            return deeplinkUrl;
        }

        private bool CheckValidWebUrl(string url)
        {
            var result = WebUrlIValidation(url);

            if (!result)
            {
                return false;
            }
            
            var uri = new Uri(url);
            var path = uri.GetLeftPart(UriPartial.Path);

            return path.Contains(_appSettings.WebUrlPrefix);
        }

        private static bool WebUrlIValidation(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
                   && (uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeHttp);
        }

        private string RandomString(int length)
        {
            var chars = _appSettings.LinkIdChars;
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}