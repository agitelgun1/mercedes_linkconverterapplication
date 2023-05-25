using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LinkConverterApplication.Domain.Abstractions;
using LinkConverterApplication.DTO.Response;

namespace LinkConverterApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConvertLinkController : ControllerBase
    {
        private readonly IConvertLinkService _convertLinkService;

        public ConvertLinkController(IConvertLinkService convertLinkService)
        {
            _convertLinkService = convertLinkService;
        }


        [HttpGet("convertweburltodeeplink")]
        public async Task<BaseResponse<string>> ConvertWebUrlToDeepLink(string webUrl)
        {
            var response = new BaseResponse<string>
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Result = "Url is not valid! Please check your request."
            };

            var deepLinkUrl = await _convertLinkService.ConvertWebUrlToDeepLinkUrl(webUrl);

            if (string.IsNullOrEmpty(deepLinkUrl))
            {
                return response;
            }
            
            response.IsSuccess = true;
            response.StatusCode = StatusCodes.Status200OK;
            response.Result = deepLinkUrl;

            return response;
        }
        
        [HttpGet("redirection")]
        public async Task<BaseResponse<string>> Redirection(string deeplinkUrl)
        {
            var response = new BaseResponse<string>
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Result = "Url is not valid! Please check your request."
            };

            var deepLinkUrl = await _convertLinkService.Redirection(deeplinkUrl);

            if (string.IsNullOrEmpty(deepLinkUrl))
            {
                return response;
            }
            
            response.IsSuccess = true;
            response.StatusCode = StatusCodes.Status200OK;
            response.Result = deepLinkUrl;

            return response;
        }
        
        [HttpGet("customurl")]
        public async Task<BaseResponse<string>> CustomUrl(string webUrl, string deeplinkUrl)
        {
            var response = new BaseResponse<string>
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Result = "Url is not valid! Please check your request."
            };
        
            var customUrl = await _convertLinkService.CustomUrl(webUrl, deeplinkUrl);
        
            if (string.IsNullOrEmpty(customUrl))
            {
                return response;
            }
            
            response.IsSuccess = true;
            response.StatusCode = StatusCodes.Status200OK;
            response.Result = customUrl;
        
            return response;
        }
    }
}