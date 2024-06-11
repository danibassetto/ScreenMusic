using Microsoft.AspNetCore.Mvc;
using ScreenMusic.Arguments;
using ScreenMusic.WebMvc.Services;

namespace ScreenMusic.WebMvc.Controllers;

public class ArtistController(ArtistServiceClient artistServiceClient) : Controller
{
    private readonly ArtistServiceClient _artistServiceClient = artistServiceClient;

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 8)
    {
        var artists = await _artistServiceClient.GetAll();
        if (artists == null)
            return View(new List<OutputArtist>());

        int totalItem = artists.Count;
        int totalPage = (int)Math.Ceiling(totalItem / (double)pageSize);
        var pagedArtists = artists.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = pageNumber;

        return View(pagedArtists);
    }

    public async Task<IActionResult> Details(long id)
    {
        var artist = await _artistServiceClient.GetById(id);
        if (artist == null)
            return NotFound();

        return View(artist);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(InputCreateArtist inputCreateArtist)
    {
        if (!ModelState.IsValid)
            return View(inputCreateArtist);

        var result = await _artistServiceClient.Create(inputCreateArtist);
        if (result)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "Erro ao criar artista");
        return View(inputCreateArtist);
    }

    [HttpGet]
    public async Task<IActionResult> Update(long id)
    {
        var artist = await _artistServiceClient.GetById(id);
        if (artist == null)
            return NotFound();

        var inputUpdateArtist = new InputUpdateArtist(artist.ProfilePhoto!, artist.Biography!);

        return View(inputUpdateArtist);
    }

    [HttpPost]
    public async Task<IActionResult> Update(long id, InputUpdateArtist inputUpdateArtist)
    {
        if (!ModelState.IsValid)
            return View(inputUpdateArtist);

        var result = await _artistServiceClient.Update(id, inputUpdateArtist);
        if (result)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "Erro ao atualizar artista");
        return View(inputUpdateArtist);
    }

    public async Task<IActionResult> Delete(long id)
    {
        var result = await _artistServiceClient.Delete(id);
        if (result)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "Erro ao deletar artista");
        return RedirectToAction(nameof(Index));
    }
}