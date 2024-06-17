using Microsoft.AspNetCore.Mvc;
using ScreenMusic.Arguments;
using ScreenMusic.WebMvc.Services;

namespace ScreenMusic.WebMvc.Controllers;

public class MusicController(MusicServiceClient musicServiceClient, ArtistServiceClient artistServiceClient, MusicGenreServiceClient musicGenreServiceClient) : Controller
{
    private readonly MusicServiceClient _musicServiceClient = musicServiceClient;
    private readonly ArtistServiceClient _artistServiceClient = artistServiceClient;
    private readonly MusicGenreServiceClient _musicGenreServiceClient = musicGenreServiceClient;

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 8)
    {
        var listMusic = await _musicServiceClient.GetAll();
        listMusic ??= new List<OutputMusic>();

        var totalItem = listMusic.Count;
        var totalPage = (int)Math.Ceiling(totalItem / (double)pageSize);

        var artistsByPage = listMusic.OrderByDescending(a => a.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = pageNumber;

        return View(artistsByPage);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadComboBoxList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(InputCreateMusic inputCreate)
    {
        if (!ModelState.IsValid)
        {
            await LoadComboBoxList();
            return View(inputCreate);
        }

        bool success = await _musicServiceClient.Create(inputCreate);

        if (success)
            return RedirectToAction("Index");

        await LoadComboBoxList();
        ModelState.AddModelError(string.Empty, "Erro ao criar musica.");
        return View(inputCreate);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var music = await _musicServiceClient.GetById(id);

        if (music == null)
            return NotFound();

        var model = new InputUpdateMusic(music.Name, music.ReleaseYear, music.ArtistId, music.MusicGenreId);

        ViewBag.Id = id;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, InputUpdateMusic model)
    {
        if (!ModelState.IsValid)
            return View(model);

        bool success = await _musicServiceClient.Update(id, model);

        if (success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = "Erro ao atualizar o musica.";
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await _musicServiceClient.Delete(id);

        if (success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = "Erro ao excluir o musica.";
        return RedirectToAction("Index");
    }

    private async Task LoadComboBoxList()
    {
        ViewBag.Artists = await _artistServiceClient.GetAll();
        ViewBag.Genres = await _musicGenreServiceClient.GetAll();
    }
}