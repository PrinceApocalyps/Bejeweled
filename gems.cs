using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Represents a gem on the game board with a color and position.
/// </summary>
public class Gem
{
    private GemColor gem_color;
    private int x_pos;
    private int y_pos;

    /// <summary>
    /// The available colors a gem can be.
    /// </summary>
    public enum GemColor
    {
        Red, Blue, Green, Yellow, Purple
    }

    /// <summary>
    /// Initializes a new gem with a position and color.
    /// </summary>
    /// <param name="x">The x-coordinate of the gem on the board.</param>
    /// <param name="y">The y-coordinate of the gem on the board.</param>
    /// <param name="color">The color of the gem.</param>
    public Gem(int x, int y, GemColor color)
    {
        gem_color = color;
        SetPos(x, y);
    }

    /// <summary>
    /// Returns the color of this gem.
    /// </summary>
    public GemColor GetColor()
    {
        return gem_color;
    }

    /// <summary>
    /// Returns the gem's current position as a two-element array [x, y].
    /// </summary>
    public int[] get_pos() => new[] {x_pos, y_pos};

    /// <summary>
    /// Updates the gem's position on the board.
    /// </summary>
    /// <param name="x">The new x-coordinate.</param>
    /// <param name="y">The new y-coordinate.</param>
    public void SetPos(int x, int y)
    {
        x_pos = x;
        y_pos = y;
    }
}