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
        var response = await _musicServiceClient.GetAll();

        if (!response.Success)
        {
            TempData["ErrorMessage"] = response.ErrorMessage ?? "Erro ao listar músicas.";
            return View(new List<OutputMusic>());
        }

        var listMusic = response.Data ?? [];

        foreach (var music in listMusic)
        {
            music.YoutubeLink = ConvertToEmbedYoutubeUrl(music.YoutubeLink!);
        }

        var totalItem = listMusic.Count;
        var totalPage = (int)Math.Ceiling(totalItem / (double)pageSize);

        var musicByPage = listMusic.OrderBy(a => a.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = pageNumber;

        return View(musicByPage);
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

        var response = await _musicServiceClient.Create(inputCreate);

        if (response.Success)
            return RedirectToAction("Index");

        await LoadComboBoxList();
        ModelState.AddModelError(string.Empty, response.ErrorMessage ?? "Erro ao criar música.");
        return View(inputCreate);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var response = await _musicServiceClient.GetById(id);

        if (!response.Success || response.Data == null)
            return NotFound();

        var music = response.Data;
        var model = new InputUpdateMusic(music.Name!, music.ReleaseYear, music.ArtistId, music.MusicGenreId, music.YoutubeLink!);

        ViewBag.Id = id;
        await LoadComboBoxList();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, InputUpdateMusic model)
    {
        if (!ModelState.IsValid)
        {
            await LoadComboBoxList();
            return View(model);
        }

        var response = await _musicServiceClient.Update(id, model);

        if (response.Success)
            return RedirectToAction("Index");

        await LoadComboBoxList();
        TempData["ErrorMessage"] = response.ErrorMessage ?? "Erro ao atualizar a música.";
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _musicServiceClient.Delete(id);

        if (response.Success)
            return RedirectToAction("Index");

        TempData["ErrorMessage"] = response.ErrorMessage ?? "Erro ao excluir a música.";
        return RedirectToAction("Index");
    }

    private async Task LoadComboBoxList()
    {
        var listArtist = await _artistServiceClient.GetAll();
        var listMusicGenre = await _musicGenreServiceClient.GetAll();

        if (listArtist.Success && listMusicGenre.Success)
        {
            ViewBag.Artists = listArtist.Data;
            ViewBag.Genres = listMusicGenre.Data;
        }
        else
        {
            ViewBag.Artists = new List<OutputArtist>();
            ViewBag.Genres = new List<OutputMusicGenre>();
            if (!listArtist.Success)
                TempData["ErrorMessage"] = listArtist.ErrorMessage ?? "Erro ao carregar combo box de artistas.";
            if (!listMusicGenre.Success)
                TempData["ErrorMessage"] = listArtist.ErrorMessage ?? "Erro ao carregar combo box de gênero musical.";
        }
    }

    private static string ConvertToEmbedYoutubeUrl(string youtubeLink)
    {
        var uri = new Uri(youtubeLink);
        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
        var videoId = query["v"];
        return $"https://www.youtube.com/embed/{videoId}";
    }
}