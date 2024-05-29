using ScreenMusic.Arguments;
using ScreenMusic.Domain.ApiManagement;
using ScreenMusic.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace ScreenMusic.Api.Controllers;

[Route("api/[controller]")]
public class MusicController(IApiDataService apiDataService, IMusicService service) : BaseController<IMusicService, InputCreateMusic, InputUpdateMusic, OutputMusic, InputIdentifierMusic>(apiDataService, service) { }