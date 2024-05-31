using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScreenMusic.Arguments;
using ScreenMusic.Domain.ApiManagement;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Api.Controllers;

[Route("api/[controller]")]
public class UserController(IApiDataService apiDataService, IUserService service) : BaseController<IUserService, InputCreateUser, InputUpdateUser, OutputUser, InputIdentifierUser>(apiDataService, service)
{
    [AllowAnonymous]
    public override Task<ActionResult<BaseResponseApi<long>>> Create(InputCreateUser inputCreate)
    {
        return base.Create(inputCreate);
    }

    #region IgnoreApi
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<BaseResponseApi<OutputUser>>> GetAll()
    {
        return base.GetAll();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<BaseResponseApi<OutputUser>>> GetByIdentifier(InputIdentifierUser inputIdentifier)
    {
        return base.GetByIdentifier(inputIdentifier);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<BaseResponseApi<OutputUser>>> Get(long id)
    {
        return base.Get(id);
    }
    #endregion
}