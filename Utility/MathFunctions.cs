using System;
using System.Drawing;

namespace Tetris.Utility
{
    public static class MathFunctions
    {
        public static float Distance(Point pointOne, Point pointTwo)
        {
            float YDistance = (pointTwo.Y - pointOne.Y);
            float XDistance = (pointTwo.X - pointOne.X);

            float test = (float)Math.Sqrt((YDistance * YDistance) + (XDistance * XDistance));
            return (float)Math.Sqrt((YDistance * YDistance) + (XDistance * XDistance));
        }
    }
}
