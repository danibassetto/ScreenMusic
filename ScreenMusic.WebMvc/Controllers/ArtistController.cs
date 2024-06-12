using Microsoft.AspNetCore.Mvc;
using ScreenMusic.Arguments;
using ScreenMusic.WebMvc.Services;

namespace ScreenMusic.WebMvc.Controllers;

public class ArtistController(ArtistServiceClient artistServiceClient) : Controller
{
    private readonly ArtistServiceClient _artistServiceClient = artistServiceClient;

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 8)
    {
        var allArtists = await _artistServiceClient.GetAll();
        allArtists ??= new List<OutputArtist>();

        var totalItem = allArtists.Count;
        var totalPage = (int)Math.Ceiling(totalItem / (double)pageSize);

        var artistsByPage = allArtists.OrderByDescending(a => a.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = pageNumber;

        return View(artistsByPage);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(InputCreateArtist model, IFormFile profilePhotoFile)
    {
        if (!ModelState.IsValid)
            return View(model);

        string? profilePhoto = null;
        if (profilePhotoFile != null && profilePhotoFile.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            await profilePhotoFile.CopyToAsync(memoryStream);
            profilePhoto = Convert.ToBase64String(memoryStream.ToArray());
        }

        var request = new InputCreateArtist(model.Name, profilePhoto, model.Biography);

        bool success = await _artistServiceClient.Create(request);

        if (success)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Erro ao criar artista. Verifique se o artista já existe.");
        return View(model);
    }
}