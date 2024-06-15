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
    public async Task<IActionResult> Create(InputCreateArtist inputCreate, IFormFile profilePhotoFile)
    {
        if (!ModelState.IsValid)
            return View(inputCreate);

        string? profilePhoto = null;
        if (profilePhotoFile != null && profilePhotoFile.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            await profilePhotoFile.CopyToAsync(memoryStream);
            profilePhoto = Convert.ToBase64String(memoryStream.ToArray());
        }

        var request = new InputCreateArtist(inputCreate.Name!, profilePhoto!, inputCreate.Biography!);

        bool success = await _artistServiceClient.Create(request);

        if (success)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Erro ao criar artista. Verifique se o artista já existe.");
        return View(inputCreate);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var artist = await _artistServiceClient.GetById(id);

        if (artist == null)
            return NotFound();

        var model = new InputUpdateArtist(artist.ProfilePhoto!, artist.Biography!);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, InputUpdateArtist model, IFormFile newProfilePhotoFile)
    {
        if (!ModelState.IsValid)
            return View(model);

        string? profilePhoto = model.ProfilePhoto;

        if (newProfilePhotoFile != null && newProfilePhotoFile.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            await newProfilePhotoFile.CopyToAsync(memoryStream);
            profilePhoto = Convert.ToBase64String(memoryStream.ToArray());
        }

        var requestEdit = new InputUpdateArtist(profilePhoto!, model.Biography!);

        bool success = await _artistServiceClient.Update(id, requestEdit);

        if (success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = "Erro ao atualizar o artista. Verifique se o artista ainda existe.";
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await _artistServiceClient.Delete(id);

        if (success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = "Erro ao excluir o artista. Verifique se o artista ainda existe.";
        return RedirectToAction("Index");
    }
}