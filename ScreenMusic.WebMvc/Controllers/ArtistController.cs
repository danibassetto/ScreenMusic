using Microsoft.AspNetCore.Mvc;
using ScreenMusic.Arguments;
using ScreenMusic.WebMvc.Services;

namespace ScreenMusic.WebMvc.Controllers;

public class ArtistController(ArtistServiceClient artistServiceClient) : Controller
{
    private readonly ArtistServiceClient _artistServiceClient = artistServiceClient;

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 8)
    {
        var response = await _artistServiceClient.GetAll();

        if (!response.Success)
        {
            TempData["ErrorMessage"] = response.ErrorMessage ?? "Erro ao listar artistas.";
            return View(new List<OutputArtist>());
        }

        var listArtist = response.Data ?? [];

        var totalItem = listArtist.Count;
        var totalPage = (int)Math.Ceiling(totalItem / (double)pageSize);

        var artistsByPage = listArtist.OrderByDescending(a => a.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
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

        var response = await _artistServiceClient.Create(request);

        if (response.Success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = response.ErrorMessage ?? "Erro ao criar artista. Verifique se o artista já existe.";
        return View(inputCreate);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var response = await _artistServiceClient.GetById(id);

        if (!response.Success || response.Data == null)
            return NotFound();

        var artist = response.Data;
        var model = new InputUpdateArtist(artist.ProfilePhoto!, artist.Biography!);

        ViewBag.Id = id;
        ViewBag.Name = artist.Name;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, InputUpdateArtist inputUpdate, IFormFile? newProfilePhoto)
    {
        if (!ModelState.IsValid)
            return View(inputUpdate);

        string? profilePhoto = inputUpdate.ProfilePhoto;

        if (newProfilePhoto != null && newProfilePhoto.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            await newProfilePhoto.CopyToAsync(memoryStream);
            profilePhoto = Convert.ToBase64String(memoryStream.ToArray());
        }

        var requestEdit = new InputUpdateArtist(profilePhoto!, inputUpdate.Biography!);

        var response = await _artistServiceClient.Update(id, requestEdit);

        if (response.Success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = response.ErrorMessage ?? "Erro ao atualizar o artista.";
        return View(inputUpdate);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _artistServiceClient.Delete(id);

        if (response.Success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = response.ErrorMessage ?? "Erro ao excluir o artista.";
        return RedirectToAction("Index");
    }
}