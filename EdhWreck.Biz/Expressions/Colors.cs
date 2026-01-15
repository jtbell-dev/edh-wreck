namespace EdhWreck.Biz.Expressions
{
    [System.Flags]
    public enum Colors
    {
        // Colorless
        None = 0,

        // Mono colors
        White = 1 << 0,
        Blue = 1 << 1,
        Black = 1 << 2,
        Red = 1 << 3,
        Green = 1 << 4,

        // Ravnica Allied Guilds
        Azorius = White | Blue,
        Dimir = Blue | Black,
        Rakdos = Black | Red,
        Gruul = Red | Green,
        Selesnya = White | Green,

        // Ravnica Enemy Guilds
        Boros = White | Red,
        Golgari = Black | Green,
        Izzet = Blue | Red,
        Orzhov = White | Black,
        Simic = Blue | Green,

        // Tarkir Clan Wedges
        Abzan = White | Black | Green,
        Jeskai = Blue | Red | White,
        Sultai = Black | Green | Blue,
        Mardu = Red | White | Black,
        Temur = Green | Blue | Red,

        // Alara Shards
        Bant = White | Blue | Green,
        Esper = White | Blue | Black,
        Grixis = Blue | Black | Red,
        Jund = Black | Red | Green,
        Naya = Red | White | Green,

        // Four-color nicknames
        Chaos = Green | Blue | Red | Black,
        Aggression = White | Black | Red | Green,
        Altruism = White | Blue | Red | Green,
        Growth = White | Black | Blue | Green,
        Artifice = White | Black | Blue | Red,

        // Rainbow
        All = White | Blue | Black | Red | Green
    }
}