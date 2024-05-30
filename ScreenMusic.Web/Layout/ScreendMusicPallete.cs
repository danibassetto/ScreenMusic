using MudBlazor;
using MudBlazor.Utilities;

namespace ScreenMusic.Web.Layout;

public sealed class ScreendMusicPallete : PaletteDark
{
    private ScreendMusicPallete()
    {
        Primary = new MudColor("#9966FF");
        Secondary = new MudColor("#F6AD31");
        Tertiary = new MudColor("#8AE491");
    }
    public static ScreendMusicPallete CreatePallete => new();
}