using Microsoft.AspNetCore.Mvc;
using ScreenMusic.Arguments;
using ScreenMusic.Domain.ApiManagement;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Api.Controllers;

[Route("api/[controller]")]
public class ArtistController(IApiDataService apiDataService, IArtistService service) : BaseController<IArtistService, InputCreateArtist, InputUpdateArtist, OutputArtist, InputIdentifierArtist>(apiDataService, service)
{
    [HttpPost("Review/{id}")]
    public virtual async Task<ActionResult<BaseResponseApi<bool>>> Review(long id, InputReviewArtist inputReviewArtist)
    {
        try
        {
            return await ResponseAsync(_service!.Review(id, inputReviewArtist));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpGet("GetReview/{id}")]
    public virtual async Task<ActionResult<BaseResponseApi<OutputArtistReview?>>> GetReview(long id)
    {
        try
        {
            return await ResponseAsync(_service!.GetReview(id));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }
}