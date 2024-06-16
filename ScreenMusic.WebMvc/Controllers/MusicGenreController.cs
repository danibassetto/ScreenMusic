using Microsoft.AspNetCore.Mvc;
using ScreenMusic.Arguments;
using ScreenMusic.WebMvc.Services;

namespace ScreenMusicGenre.WebMvc.Controllers;

public class MusicGenreController(MusicGenreServiceClient musicGenreServiceClient) : Controller
{
    private readonly MusicGenreServiceClient _musicGenreServiceClient = musicGenreServiceClient;

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 8)
    {
        var allMusicGenres = await _musicGenreServiceClient.GetAll();
        allMusicGenres ??= new List<OutputMusicGenre>();

        var totalItem = allMusicGenres.Count;
        var totalPage = (int)Math.Ceiling(totalItem / (double)pageSize);

        var musicGenresByPage = allMusicGenres.OrderByDescending(a => a.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = pageNumber;

        return View(musicGenresByPage);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(InputCreateMusicGenre inputCreate)
    {
        if (!ModelState.IsValid)
            return View(inputCreate);

        bool success = await _musicGenreServiceClient.Create(inputCreate);

        if (success)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Erro ao criar gênero musical.");
        return View(inputCreate);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var musicGenre = await _musicGenreServiceClient.GetById(id);

        if (musicGenre == null)
            return NotFound();

        var model = new InputUpdateMusicGenre(musicGenre.Description!);

        ViewBag.Id = id;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, InputUpdateMusicGenre model)
    {
        if (!ModelState.IsValid)
            return View(model);

        bool success = await _musicGenreServiceClient.Update(id, model);

        if (success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = "Erro ao atualizar o gênero musical.";
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await _musicGenreServiceClient.Delete(id);

        if (success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = "Erro ao excluir o gênero musical.";
        return RedirectToAction("Index");
    }
}