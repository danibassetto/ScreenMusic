using Microsoft.AspNetCore.Mvc;
using ScreenMusic.Arguments;
using ScreenMusic.Domain.ApiManagement;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Api.Controllers;

[Route("api/[controller]")]
public class MusicController(IApiDataService apiDataService, IMusicService service) : BaseController<IMusicService, InputCreateMusic, InputUpdateMusic, OutputMusic, InputIdentifierMusic>(apiDataService, service)
{
    [HttpGet("GetListByArtistId/{artistId}")]
    public virtual async Task<ActionResult<BaseResponseApi<List<OutputMusic>>>> GetListByArtistId(long artistId)
    {
        try
        {
            return await ResponseAsync(_service?.GetListByArtistId(artistId));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpGet("GetListByMusicGenreId/{musicGenreId}")]
    public virtual async Task<ActionResult<BaseResponseApi<List<OutputMusic>>>> GetListByMusicGenreId(long musicGenreId)
    {
        try
        {
            return await ResponseAsync(_service?.GetListByMusicGenreId(musicGenreId));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }
}