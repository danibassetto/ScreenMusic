using Microsoft.AspNetCore.Mvc;
using ScreenMusic.Arguments;
using ScreenMusic.Domain.ApiManagement;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Api.Controllers;

[Route("api/[controller]")]
public class MusicGenreController(IApiDataService apiDataService, IMusicGenreService service) : BaseController<IMusicGenreService, InputCreateMusicGenre, InputUpdateMusicGenre, OutputMusicGenre, InputIdentifierMusicGenre>(apiDataService, service) { }