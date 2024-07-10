using Microsoft.AspNetCore.Mvc;
using ScreenMusic.Arguments;
using ScreenMusic.WebMvc.Services;

namespace ScreenMusic.WebMvc.Controllers;

public class MusicGenreController(MusicGenreServiceClient musicGenreServiceClient) : Controller
{
    private readonly MusicGenreServiceClient _musicGenreServiceClient = musicGenreServiceClient;

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 8)
    {
        var response = await _musicGenreServiceClient.GetAll();

        if (!response.Success)
        {
            TempData["ErrorMessage"] = response.ErrorMessage;
            return View(new List<OutputMusicGenre>());
        }

        var listMusicGenre = response.Data ?? new List<OutputMusicGenre>();

        var totalItem = listMusicGenre.Count;
        var totalPage = (int)Math.Ceiling(totalItem / (double)pageSize);

        var musicGenresByPage = listMusicGenre.OrderBy(a => a.Name)
                                              .Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();

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

        var response = await _musicGenreServiceClient.Create(inputCreate);

        if (response.Success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = response.ErrorMessage ?? "Erro ao criar gênero musical. Verifique se ele já existe.";
        return View(inputCreate);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var response = await _musicGenreServiceClient.GetById(id);

        if (!response.Success || response.Data == null)
            return NotFound();

        var musicGenre = response.Data;
        var model = new InputUpdateMusicGenre(musicGenre.Description!);

        ViewBag.Id = id;
        ViewBag.Name = musicGenre.Name;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, InputUpdateMusicGenre inputUpdate)
    {
        if (!ModelState.IsValid)
            return View(inputUpdate);

        var response = await _musicGenreServiceClient.Update(id, inputUpdate);

        if (response.Success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = response.ErrorMessage ?? "Erro ao atualizar o gênero musical.";
        return View(inputUpdate);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _musicGenreServiceClient.Delete(id);

        if (response.Success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = response.ErrorMessage ?? "Erro ao excluir o gênero musical.";
        return RedirectToAction("Index");
    }
}