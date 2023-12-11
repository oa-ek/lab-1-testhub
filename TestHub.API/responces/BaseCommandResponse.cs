using System.Net;
using Application.results.common;
using Newtonsoft.Json;

namespace TestHub.API.responces
{
    public static class BaseCommandResponse<T>
    {
        public static ActionResult<T> GetBaseCommandResponseMessage(BaseCommandResult<T> result)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = result.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            response.ReasonPhrase = result.Message;

            var responseObject = new
            {
                responseObject = result.ResponseObject,
                responseObjectId = result.ResponseObjectId,
                message = response.ReasonPhrase,
                errors = result.Errors
            };

            var jsonContent = JsonConvert.SerializeObject(responseObject);
            return new ContentResult
            {
                Content = jsonContent,
                StatusCode = (int)response.StatusCode,
                ContentType = "application/json"
            };
        }
    }
}