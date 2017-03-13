using System.Drawing;

namespace FPSTetris.GameObjects
{
    public class TetrominoDrawing
    {
        #region Constructor

        public TetrominoDrawing()
        {
        }

        #endregion

        #region Attributes and Properties

        public Color BorderColor { get; set; }
        public Color FillColor { get; set; }

        #endregion

        #region Constants

        public const int SQUARE_SIZE = 20;
        public const int SQUARE_BORDER = 2;

        #endregion

        #region Static Methods

        public static TetrominoDrawing GetTetrominoColors(TetrominoType tetrominoType)
        {
            TetrominoDrawing tetrominoDrawing = new TetrominoDrawing();

            switch (tetrominoType)
            {
                case TetrominoType.I:
                    tetrominoDrawing.FillColor = Color.Cyan;
                    tetrominoDrawing.BorderColor = Color.LightCyan;
                    return tetrominoDrawing;
                case TetrominoType.J:
                    tetrominoDrawing.FillColor = Color.Blue;
                    tetrominoDrawing.BorderColor = Color.LightBlue;
                    return tetrominoDrawing;
                case TetrominoType.L:
                    tetrominoDrawing.FillColor = Color.OrangeRed;
                    tetrominoDrawing.BorderColor = Color.Orange;
                    return tetrominoDrawing;
                case TetrominoType.O:
                    tetrominoDrawing.FillColor = Color.Yellow;
                    tetrominoDrawing.BorderColor = Color.White;
                    return tetrominoDrawing;
                case TetrominoType.S:
                    tetrominoDrawing.FillColor = Color.Green;
                    tetrominoDrawing.BorderColor = Color.LightGreen;
                    return tetrominoDrawing;
                case TetrominoType.T:
                    tetrominoDrawing.FillColor = Color.Purple;
                    tetrominoDrawing.BorderColor = Color.Orchid;
                    return tetrominoDrawing;
                case TetrominoType.Z:
                    tetrominoDrawing.FillColor = Color.Red;
                    tetrominoDrawing.BorderColor = Color.LightCoral;
                    return tetrominoDrawing;
                case TetrominoType.Filled:
                    tetrominoDrawing.FillColor = Color.LightGray;
                    tetrominoDrawing.BorderColor = Color.White;
                    return tetrominoDrawing;
                default:
                case TetrominoType.Empty:
                    tetrominoDrawing.FillColor = Color.Black;
                    tetrominoDrawing.BorderColor = Color.FromArgb(12, 29, 43);
                    return tetrominoDrawing;
            }
        }

        #endregion
    }
}
