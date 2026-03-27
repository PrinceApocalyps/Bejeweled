using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

public class Gem
{
    private GemColor gem_color;
    private int x_pos;
    private int y_pos;

    public enum GemColor
    {
        Red, Blue, Green, Yellow, Purple
    }
    public Gem(int x, int y, GemColor color)
    {
        gem_color = color;
        SetPos(x, y);
    }

    public GemColor GetColor()
    {
        return gem_color;
    }

    public int[] get_pos() => new[] {x_pos, y_pos};

    public void SetPos(int x, int y)
    {
        x_pos = x;
        y_pos = y;
    }
}