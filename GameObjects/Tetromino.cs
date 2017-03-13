using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FPSTetris.GameObjects
{
    public class Tetromino
    {
        #region Constructor

        public Tetromino(TetrisLevel tetrisLevel, TetrominoType type)
        {
            TetrominoType = type;
            TetrisLevel = tetrisLevel;
            Positions = new List<Point>();

            CreateTetromino();

            Active = true;
        }

        #endregion

        #region Attributes and Properties

        public bool Active { get; private set; }
        public List<Point> Positions { get; private set; }
        public Size MatrixSize { get; private set; }
        public TetrisLevel TetrisLevel { get; private set; }
        public TetrominoType TetrominoType { get; private set; }

        public Point TetrominoCenter
        {
            get { return CalculateTetrominoCenter(); }
        }

        #endregion

        #region Private Fields

        private bool[,] _tetrominoMatrix;
        private int _xIncrease;
        private int _yIncrease;

        #endregion

        #region Private Methods

        private Point CalculateTetrominoCenter()
        {
            int xPosition = (_xIncrease * TetrominoDrawing.SQUARE_SIZE) + TetrisLevel.XMARGIN;
            int yPosition = (_yIncrease * TetrominoDrawing.SQUARE_SIZE) + TetrisLevel.YMARGIN;

            switch (TetrominoType)
            {
                case TetrominoType.I:
                    if (_tetrominoMatrix[1,0])
                        return new Point(TetrominoDrawing.SQUARE_SIZE + (TetrominoDrawing.SQUARE_SIZE / 2) + xPosition,
                                          (TetrominoDrawing.SQUARE_SIZE * 2) + yPosition);
                    else if (_tetrominoMatrix[2, 0])
                        return new Point((TetrominoDrawing.SQUARE_SIZE * 2) + (TetrominoDrawing.SQUARE_SIZE / 2) + xPosition,
                                          (TetrominoDrawing.SQUARE_SIZE * 2) + yPosition);
                    else if (_tetrominoMatrix[3, 1])
                        return new Point((TetrominoDrawing.SQUARE_SIZE * 2) + xPosition,
                                          TetrominoDrawing.SQUARE_SIZE + (TetrominoDrawing.SQUARE_SIZE / 2) + yPosition);
                    else 
                        return new Point((TetrominoDrawing.SQUARE_SIZE * 2) + xPosition,
                                          (TetrominoDrawing.SQUARE_SIZE * 2) + (TetrominoDrawing.SQUARE_SIZE / 2) + yPosition);
                case TetrominoType.O:
                    return new Point(TetrominoDrawing.SQUARE_SIZE + xPosition,
                                      TetrominoDrawing.SQUARE_SIZE + yPosition);
                case TetrominoType.J:
                case TetrominoType.L:
                case TetrominoType.S:
                case TetrominoType.T:
                case TetrominoType.Z:
                    return new Point(TetrominoDrawing.SQUARE_SIZE + (TetrominoDrawing.SQUARE_SIZE / 2) + xPosition,
                                      TetrominoDrawing.SQUARE_SIZE + (TetrominoDrawing.SQUARE_SIZE / 2) + yPosition);
            }

            return new Point();
        }

        private void CreateTetromino()
        {
            switch (TetrominoType)
            {
                case TetrominoType.O:
                    CreateOTetromino();
                    break;
                case TetrominoType.I:
                    CreateITetromino();
                    break;
                case TetrominoType.T:
                    CreateTTetromino();
                    break;
                case TetrominoType.L:
                    CreateLTetromino();
                    break;
                case TetrominoType.J:
                    CreateJTetromino();
                    break;
                case TetrominoType.S:
                    CreateSTetromino();
                    break;
                case TetrominoType.Z:
                    CreateZTetromino();
                    break;
            }
        }

        private void CreateOTetromino()
        {
            MatrixSize = new Size(2, 2);
            _tetrominoMatrix = new bool[MatrixSize.Width, MatrixSize.Height];
            
            _tetrominoMatrix[0, 0] = true;
            _tetrominoMatrix[1, 0] = true;
            _tetrominoMatrix[0, 1] = true;
            _tetrominoMatrix[1, 1] = true;

            _xIncrease = 5;
            _yIncrease = -2;
        }

        private void CreateITetromino()
        {
            MatrixSize = new Size(4, 4);
            _tetrominoMatrix = new bool[MatrixSize.Width, MatrixSize.Height];

            _tetrominoMatrix[1, 0] = true;
            _tetrominoMatrix[1, 1] = true;
            _tetrominoMatrix[1, 2] = true;
            _tetrominoMatrix[1, 3] = true;

            _xIncrease = 3;
            _yIncrease = -4;
        }

        private void CreateTTetromino()
        {
            MatrixSize = new Size(3, 3);
            _tetrominoMatrix = new bool[MatrixSize.Width, MatrixSize.Height];

            _tetrominoMatrix[1, 0] = true;
            _tetrominoMatrix[0, 1] = true;
            _tetrominoMatrix[1, 1] = true;
            _tetrominoMatrix[2, 1] = true;

            _xIncrease = 3;
            _yIncrease = -4;
        }

        private void CreateLTetromino()
        {
            MatrixSize = new Size(3, 3);
            _tetrominoMatrix = new bool[MatrixSize.Width, MatrixSize.Height];

            _tetrominoMatrix[1, 0] = true;
            _tetrominoMatrix[1, 1] = true;
            _tetrominoMatrix[1, 2] = true;
            _tetrominoMatrix[2, 2] = true;

            _xIncrease = 3;
            _yIncrease = -4;
        }

        private void CreateJTetromino()
        {
            MatrixSize = new Size(3, 3);
            _tetrominoMatrix = new bool[MatrixSize.Width, MatrixSize.Height];

            _tetrominoMatrix[1, 0] = true;
            _tetrominoMatrix[1, 1] = true;
            _tetrominoMatrix[1, 2] = true;
            _tetrominoMatrix[0, 2] = true;

            _xIncrease = 3;
            _yIncrease = -4;
        }

        private void CreateSTetromino()
        {
            MatrixSize = new Size(3, 3);
            _tetrominoMatrix = new bool[MatrixSize.Width, MatrixSize.Height];

            _tetrominoMatrix[0, 1] = true;
            _tetrominoMatrix[1, 1] = true;
            _tetrominoMatrix[1, 2] = true;
            _tetrominoMatrix[2, 2] = true;

            _xIncrease = 3;
            _yIncrease = -4;
        }

        private void CreateZTetromino()
        {
            MatrixSize = new Size(3, 3);
            _tetrominoMatrix = new bool[MatrixSize.Width, MatrixSize.Height];

            _tetrominoMatrix[2, 1] = true;
            _tetrominoMatrix[1, 1] = true;
            _tetrominoMatrix[1, 2] = true;
            _tetrominoMatrix[0, 2] = true;

            _xIncrease = 3;
            _yIncrease = -4;
        }

        private HitType Hit(int xIncrease, int yIncrease)
        {
            int xMoveIncrease = _xIncrease + xIncrease;
            int yMoveIncrease = _yIncrease + yIncrease;

            for (int x = 0; x < MatrixSize.Width; x++)
                for (int y = 0; y < MatrixSize.Height; y++)
                {
                    if (_tetrominoMatrix[x, y])
                    {
                        int matrixX = x + xMoveIncrease;
                        int matrixY = y + yMoveIncrease;

                        if (xIncrease != 0)
                        {
                            if ((matrixX < 0) || (matrixX >= TetrisLevel.LevelSize.Width))
                                return HitType.Side;
                            if ((matrixX >= 0) && (matrixX < TetrisLevel.LevelSize.Width) && (matrixY >= 0))
                                if (TetrisLevel.LevelMatrix[matrixY][matrixX] != TetrominoType.Empty)
                                    return HitType.Side;
                        }

                        if (yIncrease != 0)
                        {
                            if (matrixY >= TetrisLevel.LevelSize.Height)
                                return HitType.Bottom;

                            if ((matrixX >= 0) && (matrixX < TetrisLevel.LevelSize.Width))
                                if (matrixY >= 0)
                                    if (TetrisLevel.LevelMatrix[matrixY][matrixX] != TetrominoType.Empty)
                                        return HitType.Bottom;
                        }
                    }
                }

            return HitType.None;
        }

        private void Move(int increase, MoveDirection direction)
        {
            int xIncrease = 0;
            int yIncrease = 0;

            switch (direction)
            {
                case MoveDirection.Horizontal:
                    xIncrease = increase;
                    break;
                case MoveDirection.Vertical:
                    yIncrease = increase;
                    break;
            }

            switch (Hit(xIncrease, yIncrease))
            {
                case HitType.Side:
                    return;
                case HitType.Bottom:
                    Weld();
                    return;
            }

            _xIncrease += xIncrease;
            _yIncrease += yIncrease;
        }

        private bool[,] Rotate3x3Tetromino(Rotation rotation)
        {
            bool[,] newMatrix = new bool[3, 3];
            if (rotation == Rotation.Clockwise)
            {
                newMatrix[0, 0] = _tetrominoMatrix[0, 2];
                newMatrix[1, 0] = _tetrominoMatrix[0, 1];
                newMatrix[2, 0] = _tetrominoMatrix[0, 0];
                newMatrix[0, 1] = _tetrominoMatrix[1, 2];
                newMatrix[1, 1] = _tetrominoMatrix[1, 1];
                newMatrix[2, 1] = _tetrominoMatrix[1, 0];
                newMatrix[0, 2] = _tetrominoMatrix[2, 2];
                newMatrix[1, 2] = _tetrominoMatrix[2, 1];
                newMatrix[2, 2] = _tetrominoMatrix[2, 0];
            }
            else
            {
                newMatrix[0, 0] = _tetrominoMatrix[2, 0];
                newMatrix[1, 0] = _tetrominoMatrix[2, 1];
                newMatrix[2, 0] = _tetrominoMatrix[2, 2];
                newMatrix[0, 1] = _tetrominoMatrix[1, 0];
                newMatrix[1, 1] = _tetrominoMatrix[1, 1];
                newMatrix[2, 1] = _tetrominoMatrix[1, 2];
                newMatrix[0, 2] = _tetrominoMatrix[0, 0];
                newMatrix[1, 2] = _tetrominoMatrix[0, 1];
                newMatrix[2, 2] = _tetrominoMatrix[0, 2];
            }

            return newMatrix;
        }

        private bool[,] Rotate4x4Tetromino(Rotation rotation)
        {
            bool[,] newMatrix = new bool[4, 4];

            if (rotation == Rotation.Clockwise)
            {
                newMatrix[0, 0] = _tetrominoMatrix[0, 3];
                newMatrix[1, 0] = _tetrominoMatrix[0, 2];
                newMatrix[2, 0] = _tetrominoMatrix[0, 1];
                newMatrix[3, 0] = _tetrominoMatrix[0, 0];
                newMatrix[0, 1] = _tetrominoMatrix[1, 3];
                newMatrix[1, 1] = _tetrominoMatrix[1, 2];
                newMatrix[2, 1] = _tetrominoMatrix[1, 1];
                newMatrix[3, 1] = _tetrominoMatrix[1, 0];
                newMatrix[0, 2] = _tetrominoMatrix[2, 3];
                newMatrix[1, 2] = _tetrominoMatrix[2, 2];
                newMatrix[2, 2] = _tetrominoMatrix[2, 1];
                newMatrix[3, 2] = _tetrominoMatrix[2, 0];
                newMatrix[0, 3] = _tetrominoMatrix[3, 3];
                newMatrix[1, 3] = _tetrominoMatrix[3, 2];
                newMatrix[2, 3] = _tetrominoMatrix[3, 1];
                newMatrix[3, 3] = _tetrominoMatrix[3, 0];
            }
            else
            {
                newMatrix[0, 0] = _tetrominoMatrix[3, 0];
                newMatrix[1, 0] = _tetrominoMatrix[3, 1];
                newMatrix[2, 0] = _tetrominoMatrix[3, 2];
                newMatrix[3, 0] = _tetrominoMatrix[3, 3];
                newMatrix[0, 1] = _tetrominoMatrix[2, 0];
                newMatrix[1, 1] = _tetrominoMatrix[2, 1];
                newMatrix[2, 1] = _tetrominoMatrix[2, 2];
                newMatrix[3, 1] = _tetrominoMatrix[2, 3];
                newMatrix[0, 2] = _tetrominoMatrix[1, 0];
                newMatrix[1, 2] = _tetrominoMatrix[1, 1];
                newMatrix[2, 2] = _tetrominoMatrix[1, 2];
                newMatrix[3, 2] = _tetrominoMatrix[1, 3];
                newMatrix[0, 3] = _tetrominoMatrix[0, 0];
                newMatrix[1, 3] = _tetrominoMatrix[0, 1];
                newMatrix[2, 3] = _tetrominoMatrix[0, 2];
                newMatrix[3, 3] = _tetrominoMatrix[0, 3];
            } 
            return newMatrix;
        }

        private void Weld()
        {
            for (int x = 0; x < MatrixSize.Width; x++)
                for (int y = 0; y < MatrixSize.Height; y++)
                    if (_tetrominoMatrix[x, y])
                    {
                        int xPosition = x + _xIncrease;
                        int yPosition = y + _yIncrease;

                        if (yPosition < 0)
                        {
                            TetrisLevel.FinishGame();
                        }
                        else
                            TetrisLevel.LevelMatrix[yPosition][xPosition] = TetrominoType;
                    }

            Active = false;
        }

        #endregion
        
        #region Public Methods

        public void Down()
        {
            Move(1, MoveDirection.Vertical);
        }

        public void Left()
        {
            Move(-1, MoveDirection.Horizontal);
        }

        public void Right()
        {
            Move(1, MoveDirection.Horizontal);
        }

        public void Rotate(Rotation rotation)
        {
            if (rotation == Rotation.Clockwise)
                TetrisLevel.TetrisStatictics.Angle -= 90;
            else
                TetrisLevel.TetrisStatictics.Angle += 90;

            if (TetrominoType == GameObjects.TetrominoType.O)
                return;

            bool[,] rotatedTetrominoMatrix;
            if (TetrominoType == GameObjects.TetrominoType.I)
                rotatedTetrominoMatrix = Rotate4x4Tetromino(rotation);
            else
                rotatedTetrominoMatrix = Rotate3x3Tetromino(rotation);

            for (int x = 0; x < MatrixSize.Width; x++)
                for (int y = 0; y < MatrixSize.Height; y++)
                {
                    if (rotatedTetrominoMatrix[x, y])
                    {
                        int matrixX = x + _xIncrease;
                        int matrixY = y + _yIncrease;

                        if ((matrixX < 0) || (matrixX >= TetrisLevel.LevelSize.Width))
                            return;
                        if ((matrixX >= 0) && (matrixX < TetrisLevel.LevelSize.Width) && (matrixY >= 0))
                            if (TetrisLevel.LevelMatrix[matrixY][matrixX] != TetrominoType.Empty)
                                return;

                        if (matrixY >= TetrisLevel.LevelSize.Height)
                            return;

                        if ((matrixX >= 0) && (matrixX < TetrisLevel.LevelSize.Width))
                            if (matrixY >= 0)
                                if (TetrisLevel.LevelMatrix[matrixY][matrixX] != TetrominoType.Empty)
                                    return;
                    }
                }
            
            _tetrominoMatrix = rotatedTetrominoMatrix;
        }

        public void Render(Graphics graphics)
        {
            for (int x = 0; x < MatrixSize.Width; x++)
                for (int y = 0; y < MatrixSize.Height; y++)
                {
                    if (!_tetrominoMatrix[x, y]) continue;
                    int xPosition = x + _xIncrease;
                    int yPosition = y + _yIncrease;
                    if (yPosition < 0) continue;

                    xPosition *= TetrominoDrawing.SQUARE_SIZE;
                    yPosition *= TetrominoDrawing.SQUARE_SIZE;

                    xPosition += TetrisLevel.XMARGIN;
                    yPosition += TetrisLevel.YMARGIN;

                    TetrominoDrawing tetrominoDrawing = TetrominoDrawing.GetTetrominoColors(TetrominoType);
                    graphics.FillRectangle(new SolidBrush(tetrominoDrawing.FillColor),
                                           new Rectangle(xPosition, yPosition, TetrominoDrawing.SQUARE_SIZE, TetrominoDrawing.SQUARE_SIZE));
                    graphics.DrawRectangle(new Pen(tetrominoDrawing.BorderColor, TetrominoDrawing.SQUARE_BORDER),
                                           new Rectangle(xPosition, yPosition, TetrominoDrawing.SQUARE_SIZE, TetrominoDrawing.SQUARE_SIZE));
                }
        }

        public void RenderNext(int xMargin, int yMargin, Graphics graphics)
        {
            for (int x = 0; x < MatrixSize.Width; x++)
                for (int y = 0; y < MatrixSize.Height; y++)
                {
                    if (!_tetrominoMatrix[x, y]) continue;
                    int xPosition = x;
                    int yPosition = y;
                    if (yPosition < 0) continue;

                    xPosition *= TetrominoDrawing.SQUARE_SIZE;
                    yPosition *= TetrominoDrawing.SQUARE_SIZE;

                    xPosition += xMargin;
                    yPosition += yMargin;

                    TetrominoDrawing tetrominoDrawing = TetrominoDrawing.GetTetrominoColors(TetrominoType);
                    graphics.FillRectangle(new SolidBrush(tetrominoDrawing.FillColor),
                                           new Rectangle(xPosition, yPosition, TetrominoDrawing.SQUARE_SIZE, TetrominoDrawing.SQUARE_SIZE));
                    graphics.DrawRectangle(new Pen(tetrominoDrawing.BorderColor, TetrominoDrawing.SQUARE_BORDER),
                                           new Rectangle(xPosition, yPosition, TetrominoDrawing.SQUARE_SIZE, TetrominoDrawing.SQUARE_SIZE));
                }
        }

        #endregion
    }

    #region Enumerators

    public enum TetrominoType
    {
        Empty,        
        I,
        J,
        L,
        O,
        S,
        T,
        Z,
        Filled
    }

    public enum HitType
    {
        Bottom,
        None,
        Side
    }

    public enum MoveDirection
    {
        Horizontal,
        Vertical
    }

    public enum Rotation
    {
        Clockwise,
        CounterClockwise
    }

    #endregion
}
