
using System.Drawing;
using System.Drawing.Imaging;
namespace Cms.Infrastructure.Auth;

public static class CaptchaService
{
    private static readonly Dictionary<string, string> _store = new();

    public static (string code, byte[] Image) GenerateCaptcha()
    {
        var code = Guid.NewGuid().ToString("N")[..6].ToUpper();

        using var bmp = new Bitmap(140, 50);
        using var g = Graphics.FromImage(bmp);
        g.Clear(Color.White);

        var font = new Font("Arial", 24, FontStyle.Bold);
        var brush = new SolidBrush(Color.Black);
        g.DrawString(code, font, brush, 10, 5);

        var rand = new Random();
        for (int i = 0; i < 30; i++)
        {
            g.DrawEllipse(Pens.Gray, rand.Next(140), rand.Next(50), 2, 2);
        }

        using var ms = new MemoryStream();
        bmp.Save(ms, ImageFormat.Png);

        _store[code] = code;

        return (code, ms.ToArray());
    }


    public static bool Validate(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return false;
        }

        var exist = _store.ContainsKey(code);
        if (exist)
        {
            _store.Remove(code);
        }

        return exist;
    }
}
