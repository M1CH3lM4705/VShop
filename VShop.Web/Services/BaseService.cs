using System.Text;
using System.Text.Json;
using VShop.Core.Communication;

namespace VShop.Web.Services;

public abstract class BaseService
{
    private const string mediaType = "application/json";

    protected StringContent ObterConteudo(object dado)
    {
        return new StringContent(
            JsonSerializer.Serialize(dado),
            Encoding.UTF8,
            mediaType
        );
    }

    protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
    {
        var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), option);
    }

    protected bool TratarErrosResponse(HttpResponseMessage response)
    {
        switch((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new HttpRequestException("", null, response.StatusCode);
            case 400:
                return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }

    public ResponseResult ReturnOK()
    {
        return new ResponseResult();
    }
}
