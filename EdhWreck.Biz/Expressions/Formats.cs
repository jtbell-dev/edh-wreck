namespace EdhWreck.Biz.Expressions
{
    [System.Flags]
    public enum Formats : ulong
    {
        // Formats
        Standard = 1UL << 0,
        Future = 1UL << 1,            // "future" (Future Standard)
        Historic = 1UL << 2,
        Timeless = 1UL << 3,
        Gladiator = 1UL << 4,
        Pioneer = 1UL << 5,
        Modern = 1UL << 6,
        Legacy = 1UL << 7,
        Pauper = 1UL << 8,
        Vintage = 1UL << 9,
        Penny = 1UL << 10,            // Penny Dreadful
        Commander = 1UL << 11,
        Oathbreaker = 1UL << 12,
        StandardBrawl = 1UL << 13,
        Brawl = 1UL << 14,
        Alchemy = 1UL << 15,
        PauperCommander = 1UL << 16,
        Duel = 1UL << 17,             // Duel Commander
        Oldschool = 1UL << 18,        // Old School 93/94
        Premodern = 1UL << 19,
        Predh = 1UL << 20,

        // Convenience groupings
        All = Standard | Future | Historic | Timeless | Gladiator | Pioneer | Modern | Legacy | Pauper | Vintage | Penny | Commander | Oathbreaker | StandardBrawl | Brawl | Alchemy | PauperCommander | Duel | Oldschool | Premodern | Predh
    }
}