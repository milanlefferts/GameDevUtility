using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace General.Colors
{
    /// <summary>
    /// Provides the correct colors for use in the game.
    /// </summary>
    public class Colors
    {
        public static Color32 red = new Color32(150, 0, 0, 255);
        public static Color32 green = new Color32(20, 60, 66, 255);

        public static Color32 yellowPlayer = new Color32(215, 154, 41, 255);
        public static Color32 greenPlayer = new Color32(170, 172, 55, 255);
        public static Color32 purplePlayer = new Color32(165, 91, 150, 255);
        public static Color32 bluePlayer = new Color32(44, 176, 200, 255);

        public static Color32[] playerColors = new Color32[] { bluePlayer, purplePlayer, yellowPlayer, greenPlayer };
    }
}