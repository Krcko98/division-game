using System.Collections.Generic;
using UnityEngine;

namespace Grid.Static.Helper
{
    public static class GridItemHelper
    {
        public static readonly Dictionary<int, string> colorNumberPairs = new Dictionary<int, string>()
        {
            { 2, "#00DDB1" },
            { 3, "#00D9AE" },
            { 4, "#00D5AA" },
            { 5, "#00D1A6" },
            { 6, "#00CEA3" },
            { 7, "#00CA9F" },
            { 8, "#00C69B" },
            { 9, "#00C297" },
            { 10, "#00BE94" },
            { 11, "#00BA90" },
            { 12, "#00B78C" },
            { 13, "#00B389" },
            { 14, "#00AF85" },
            { 15, "#00AB81" },
            { 16, "#00A77E" },
            { 17, "#00A37A" },
            { 18, "#009F76" },
            { 19, "#009C72" },
            { 20, "#00986F" },
            { 21, "#00946B" },
            { 22, "#009067" },
            { 23, "#008D64" },
            { 24, "#008960" }
        };

        public static Color FetchColor(int number)
        {
            Color col;
            ColorUtility.TryParseHtmlString(colorNumberPairs[number], out col);
            
            return col;
        }
    }
}