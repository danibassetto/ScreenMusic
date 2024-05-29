using ScreenMusic.Arguments;
using ScreenMusic.Domain.ApiManagement;
using ScreenMusic.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScreenMusic.Api.Controllers;

[Route("api/[controller]")]
public class AuthenticationController(IApiDataService apiDataService, IAuthenticationService service) : BaseController_1<IAuthenticationService>(apiDataService, service)
{
    /// <summary>
    /// Verifica se o usuário está cadastrado no sistema e retorna o token de acesso. 
    /// Caso não tenha um usuário, crie um novo a partir do endpoint aberto de post no User.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public async Task<ActionResult<BaseResponseApi<OutputAuthentication, string>>> Authenticate(InputAuthentication inputAuthentication)
    {
        try
        {
            return await ResponseAsync(_service!.Authenticate(inputAuthentication));
        }
        catch (BaseResponseException ex)
        {
            return await BaseResponseExceptionAsync(ex);
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    #region IgnoreApi
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<BaseResponse<object>>> GetAll()
    {
        return base.GetAll();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<BaseResponse<object>>> Get(long id)
    {
        return base.Get(id);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<BaseResponse<object>>> GetByIdentifier(object inputIdentifier)
    {
        return base.GetByIdentifier(inputIdentifier);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<BaseResponse<long>>> Create(object inputCreate)
    {
        return base.Create(inputCreate);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<BaseResponse<long>>> Update(long id, object inputUpdate)
    {
        return base.Update(id, inputUpdate);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<BaseResponse<bool>>> Delete(long id)
    {
        return base.Delete(id);
    }
    #endregion
}