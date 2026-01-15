using EdhWreck.Biz.Expressions;

namespace EdhWreck.Biz.Extensions
{
    public static class ColorsExtensions
    {
        extension(Colors colors)
        {
            /// <summary>
            /// Convert the flagged Colors value to a string containing the unique color abbreviations
            /// in alphabetical order. Mappings: Blue='b', Black='u', White='w', Green='g', Red='r'.
            /// Example: Colors.White | Colors.Red => "rw"
            /// </summary>
            public string ToQueryString()
            {
                if (colors == Colors.None)
                {
                    return string.Empty;
                }

                var chars = new System.Collections.Generic.List<char>(5);

                if ((colors & Colors.Black) == Colors.Black)
                {
                    chars.Add('b');
                }

                if ((colors & Colors.Green) == Colors.Green)
                {
                    chars.Add('g');
                }

                if ((colors & Colors.Red) == Colors.Red)
                {
                    chars.Add('r');
                }

                if ((colors & Colors.Blue) == Colors.Blue)
                {
                    chars.Add('u');
                }

                if ((colors & Colors.White) == Colors.White)
                {
                    chars.Add('w');
                }

                return new string([.. chars]);
            }
        }
    }
}