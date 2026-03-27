using System;

foreach (Gem.GemColor color in Enum.GetValues(typeof(Gem.GemColor)))
{
    Gem gem = new Gem(1, 2, color);
}