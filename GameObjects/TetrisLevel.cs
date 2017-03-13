using System;
using System.Collections.Generic;
using System.Drawing;

namespace FPSTetris.GameObjects
{
    public class TetrisLevel
    {
        #region Constructor

        public TetrisLevel(Size levelSize)
        {
            LevelSize = levelSize;
            _randomizer = new Random();
        }

        #endregion

        #region Attributes and Properties

        public Tetromino NextTetromino { get; private set; }
        public Tetromino ActiveTetromino { get; private set; }
        public TetrisStatistics TetrisStatictics { get; private set; }
        public Size LevelSize { get; private set; }
        public List<TetrominoType[]> LevelMatrix { get; private set; }
        public bool Paused { get; set; }
        
        #endregion

        #region Constants

        public const int XMARGIN = 10;
        public const int YMARGIN = 10;

        #endregion

        #region Private Fields

        private float _accumulatedTime = 0;
        private readonly Random _randomizer;

        private bool _pauseBlink;
        private float _pauseBlinkAccumulatedTime = 0;

        private bool _gameOverBlink;
        private float _gameOverBlinkAccumulatedTime = 0;
        
        #endregion

        #region Private Methods

        private void CheckForTetris()
        {
            List<TetrominoType[]> linesToRemove = new List<TetrominoType[]>();

            foreach(TetrominoType[] line in LevelMatrix)
            {
                bool remove = true;
                for (int x = 0; x < LevelSize.Width; x++)
                {
                    if (line[x] == TetrominoType.Empty)
                    {
                        remove = false;
                        break;
                    }
                }
                if (remove) linesToRemove.Add(line);
            }

            foreach (TetrominoType[] line in linesToRemove)
            {
                TetrisStatictics.RemovedLines++;
                LevelMatrix.Remove(line);
                LevelMatrix.Insert(0, new TetrominoType[LevelSize.Width]);
            }

            UpdateScore(linesToRemove.Count);
            UpdateLevel();
        }

        private void RenderFPS(Graphics graphics)
        {
            if (!TetrisStatictics.FirstPersonMode) return;

            Font font = new Font(FontFamily.GenericSansSerif, 20);
            graphics.DrawString("FPS",
                                font,
                                new SolidBrush(Color.Red),
                                new Point((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN) + 9,
                                           YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 26)));
        }

        private void RenderGameOver(Graphics graphics)
        {
            if (!TetrisStatictics.GameOver) return;
            if (_gameOverBlink) return;

            Font font = new Font(FontFamily.GenericSansSerif, 50, FontStyle.Bold);
            Font fontSmall = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold);

            SizeF screenHalfSize = new SizeF((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) / 2f, (LevelSize.Height * TetrominoDrawing.SQUARE_SIZE) / 2f);
            PointF location = new PointF(XMARGIN, YMARGIN);
            if (TetrisStatictics.FirstPersonMode)
            {
                screenHalfSize = new SizeF(graphics.ClipBounds.Width / 2f, graphics.ClipBounds.Height / 2f);
                location = new PointF();
            }

            SizeF gameSize = graphics.MeasureString("GAME", font);
            SizeF gameHalfSize = new SizeF(gameSize.Width / 2f, gameSize.Height / 2f);
            SizeF gameTranslationSize = screenHalfSize - gameHalfSize;

            SizeF overSize = graphics.MeasureString("OVER", font);
            SizeF overHalfSize = new SizeF(overSize.Width / 2f, overSize.Height / 2f);
            SizeF overTranslationSize = screenHalfSize - overHalfSize;

            SizeF messageSize = graphics.MeasureString("Press Enter to continue...", fontSmall);
            SizeF messageHalfSize = new SizeF(messageSize.Width / 2f, messageSize.Height / 2f);
            SizeF messageTranslationSize = screenHalfSize - messageHalfSize;

            PointF gameLocation = location + gameTranslationSize;
            PointF overLocation = location + overTranslationSize;
            PointF messageLocation = location + messageTranslationSize;

            gameLocation.Y -= overSize.Height;
            messageLocation.Y += overSize.Height;

            graphics.DrawString("GAME",
                                font,
                                new SolidBrush(Color.Red),
                                gameLocation);

            graphics.DrawString("OVER",
                                font,
                                new SolidBrush(Color.Red),
                                overLocation);

            graphics.DrawString("Prees Enter to Continue...",
                                fontSmall,
                                new SolidBrush(Color.Red),
                                messageLocation);
        }

        private void RenderLevel(Graphics graphics)
        {
            graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                   new Rectangle((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                                 YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 9),
                                                 TetrominoDrawing.SQUARE_SIZE * 4,
                                                 TetrominoDrawing.SQUARE_SIZE));

            graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                   new Rectangle((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                                 YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 10),
                                                 TetrominoDrawing.SQUARE_SIZE * 4,
                                                 TetrominoDrawing.SQUARE_SIZE));

            Font font = new Font(FontFamily.GenericSansSerif, 12);
            graphics.DrawString("LEVEL:",
                                font,
                                new SolidBrush(Color.White),
                                new Point((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                           YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 9)));

            graphics.DrawString((TetrisStatictics.Level + 1) + "",
                                font,
                                new SolidBrush(Color.White),
                                new Point((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                           YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 10)));
        }

        private void RenderLines(Graphics graphics)
        {
            graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                   new Rectangle((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                                 YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 12),
                                                 TetrominoDrawing.SQUARE_SIZE * 4,
                                                 TetrominoDrawing.SQUARE_SIZE));

            graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                   new Rectangle((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                                 YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 13),
                                                 TetrominoDrawing.SQUARE_SIZE * 4,
                                                 TetrominoDrawing.SQUARE_SIZE));

            Font font = new Font(FontFamily.GenericSansSerif, 12);
            graphics.DrawString("LINES:",
                                font,
                                new SolidBrush(Color.White),
                                new Point((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                           YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 12)));

            graphics.DrawString(TetrisStatictics.RemovedLines + "",
                                font,
                                new SolidBrush(Color.White),
                                new Point((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                           YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 13)));
        }

        private void RenderNextTetromino(Graphics graphics)
        {
            graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                   new Rectangle((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                                 YMARGIN, 
                                                 TetrominoDrawing.SQUARE_SIZE * 4,
                                                 TetrominoDrawing.SQUARE_SIZE));

            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                {
                    int xPosition = x;
                    int yPosition = y;

                    xPosition *= TetrominoDrawing.SQUARE_SIZE;
                    yPosition *= TetrominoDrawing.SQUARE_SIZE;

                    xPosition += (LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN);
                    yPosition += YMARGIN + TetrominoDrawing.SQUARE_SIZE;

                    TetrominoDrawing tetrominoDrawing = TetrominoDrawing.GetTetrominoColors(TetrominoType.Empty);
                    graphics.FillRectangle(new SolidBrush(tetrominoDrawing.FillColor),
                                           new Rectangle(xPosition, yPosition, TetrominoDrawing.SQUARE_SIZE, TetrominoDrawing.SQUARE_SIZE));
                    graphics.DrawRectangle(new Pen(tetrominoDrawing.BorderColor, TetrominoDrawing.SQUARE_BORDER),
                                           new Rectangle(xPosition, yPosition, TetrominoDrawing.SQUARE_SIZE, TetrominoDrawing.SQUARE_SIZE));
                }

            graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                   new Rectangle((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                                 YMARGIN + TetrominoDrawing.SQUARE_SIZE,
                                                 TetrominoDrawing.SQUARE_SIZE * 4,
                                                 TetrominoDrawing.SQUARE_SIZE * 4));

            NextTetromino.RenderNext((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                          YMARGIN + TetrominoDrawing.SQUARE_SIZE, 
                                          graphics);

            Font font = new Font(FontFamily.GenericSansSerif, 12);
            graphics.DrawString("NEXT:", 
                                font, 
                                new SolidBrush(Color.White), 
                                new Point((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN), YMARGIN));
        }

        private void RenderPaused(Graphics graphics)
        {
            if (!Paused) return;
            if (_pauseBlink) return;

            Font font = new Font(FontFamily.GenericSansSerif, 50, FontStyle.Bold);

            SizeF screenHalfSize = new SizeF((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) / 2f, (LevelSize.Height * TetrominoDrawing.SQUARE_SIZE) / 2f);
            PointF location = new PointF(XMARGIN, YMARGIN);
            if (TetrisStatictics.FirstPersonMode) 
            { 
                screenHalfSize = new SizeF(graphics.ClipBounds.Width / 2f, graphics.ClipBounds.Height / 2f);
                location = new PointF();
            }                  

            SizeF stringSize = graphics.MeasureString("PAUSED", font);
            SizeF stringHalfSize = new SizeF(stringSize.Width / 2f, stringSize.Height / 2f);

            SizeF translationSize = screenHalfSize - stringHalfSize;
            location += translationSize;

            graphics.DrawString("PAUSED",
                                font,
                                new SolidBrush(Color.Yellow),
                                location);
        }

        private void RenderScore(Graphics graphics)
        {
            graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                   new Rectangle((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                                 YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 6),
                                                 TetrominoDrawing.SQUARE_SIZE * 4,
                                                 TetrominoDrawing.SQUARE_SIZE));

            graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                   new Rectangle((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                                 YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 7),
                                                 TetrominoDrawing.SQUARE_SIZE * 4,
                                                 TetrominoDrawing.SQUARE_SIZE));

            Font font = new Font(FontFamily.GenericSansSerif, 12);
            graphics.DrawString("SCORE:",
                                font,
                                new SolidBrush(Color.White),
                                new Point((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                           YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 6)));

            graphics.DrawString(TetrisStatictics.Score + "",
                                font,
                                new SolidBrush(Color.White),
                                new Point((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                           YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 7)));
        }

        private void RenderStatistics(Graphics graphics)
        {
            graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                   new Rectangle((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                                 YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 15),
                                                 TetrominoDrawing.SQUARE_SIZE * 4,
                                                 TetrominoDrawing.SQUARE_SIZE));

            Font font = new Font(FontFamily.GenericSansSerif, 12);
            graphics.DrawString("STATS:",
                                font,
                                new SolidBrush(Color.White),
                                new Point((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                           YMARGIN + (TetrominoDrawing.SQUARE_SIZE * 15)));

            for (int i = 1; i < 8; i++)
            {
                graphics.DrawString(((TetrominoType)i).ToString(),
                                    font,
                                    new SolidBrush(Color.White),
                                    new Point((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                               YMARGIN + (TetrominoDrawing.SQUARE_SIZE * (15 + i))));

                graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                       new Rectangle((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN),
                                                     YMARGIN + (TetrominoDrawing.SQUARE_SIZE * (15 + i)),
                                                     TetrominoDrawing.SQUARE_SIZE,
                                                     TetrominoDrawing.SQUARE_SIZE));
            }

            for (int i = 1; i < 8; i++)
            {
                graphics.DrawString(TetrisStatictics.GetTetrominoCount((TetrominoType)i) + "",
                                    font,
                                    new SolidBrush(Color.White),
                                    new Point((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN) + TetrominoDrawing.SQUARE_SIZE,
                                               YMARGIN + (TetrominoDrawing.SQUARE_SIZE * (15 + i))));

                graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                       new Rectangle((LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) + (2 * XMARGIN) + TetrominoDrawing.SQUARE_SIZE,
                                                     YMARGIN + (TetrominoDrawing.SQUARE_SIZE * (15 + i)),
                                                     TetrominoDrawing.SQUARE_SIZE * 3,
                                                     TetrominoDrawing.SQUARE_SIZE));
            }
        }

        private void RenderTetrisCanvas(Graphics graphics)
        {
            for (int x = 0; x < LevelSize.Width; x++)
                for (int y = 0; y < LevelSize.Height; y++)
                {
                    int xPosition = (x * TetrominoDrawing.SQUARE_SIZE) + TetrisLevel.XMARGIN ;
                    int yPosition = (y * TetrominoDrawing.SQUARE_SIZE) + TetrisLevel.YMARGIN;

                    TetrominoDrawing tetrominoDrawing = TetrominoDrawing.GetTetrominoColors(LevelMatrix[y][x]);
                    graphics.FillRectangle(new SolidBrush(tetrominoDrawing.FillColor),
                                           new Rectangle(xPosition, yPosition, TetrominoDrawing.SQUARE_SIZE, TetrominoDrawing.SQUARE_SIZE));
                    graphics.DrawRectangle(new Pen(tetrominoDrawing.BorderColor, TetrominoDrawing.SQUARE_BORDER),
                                           new Rectangle(xPosition, yPosition, TetrominoDrawing.SQUARE_SIZE, TetrominoDrawing.SQUARE_SIZE));
                }

            for (int x = 0; x < LevelSize.Width; x++)
                for (int y = 0; y < LevelSize.Height; y++)
                {
                    if (LevelMatrix[y][x] == TetrominoType.Empty) continue;

                    int xPosition = (x * TetrominoDrawing.SQUARE_SIZE) + TetrisLevel.XMARGIN;
                    int yPosition = (y * TetrominoDrawing.SQUARE_SIZE) + TetrisLevel.YMARGIN;

                    TetrominoDrawing tetrominoDrawing = TetrominoDrawing.GetTetrominoColors(TetrominoType.Filled);
                    graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                           new Rectangle(xPosition, yPosition, TetrominoDrawing.SQUARE_SIZE, TetrominoDrawing.SQUARE_SIZE));
                    graphics.DrawRectangle(new Pen(Color.White, TetrominoDrawing.SQUARE_BORDER),
                                           new Rectangle(xPosition, yPosition, TetrominoDrawing.SQUARE_SIZE, TetrominoDrawing.SQUARE_SIZE));
                }

            ActiveTetromino.Render(graphics);
        }

        private void RotateLevel(Graphics graphics)
        {
            if (!TetrisStatictics.FirstPersonMode) return;

            Point centerScreen = new Point(GameWindow.Instance.Width / 2, GameWindow.Instance.Height / 2);
            Point tetrominoLocation = ActiveTetromino.TetrominoCenter;
            graphics.ResetTransform();

            graphics.TranslateTransform(centerScreen.X - tetrominoLocation.X, centerScreen.Y - tetrominoLocation.Y);

            graphics.TranslateTransform(tetrominoLocation.X, tetrominoLocation.Y);
            graphics.RotateTransform(TetrisStatictics.Angle);
            
            graphics.TranslateTransform(-tetrominoLocation.X , -tetrominoLocation.Y);
        }

        private void UpdateScore(int removedLines)
        {
            TetrisStatictics.RemovedLines += removedLines;

            switch (removedLines)
            {
                case 1:
                    TetrisStatictics.Score += 40 * (TetrisStatictics.Level + 1);
                    break;
                case 2:
                    TetrisStatictics.Score += 100 * (TetrisStatictics.Level + 1);
                    break;
                case 3:
                    TetrisStatictics.Score += 300 * (TetrisStatictics.Level + 1);
                    break;
                case 4:
                    TetrisStatictics.Score += 1200 * (TetrisStatictics.Level + 1);
                    break;
            }
        }

        private void UpdateLevel()
        {
            TetrisStatictics.Level = TetrisStatictics.Score / 1000;
        }

        #endregion

        #region Public Methods

        public void AddNextTetromino()
        {
            if (NextTetromino != null)
                ActiveTetromino = NextTetromino;
            else
            {
                TetrominoType activeTetrominoType = TetrominoType.Empty;
                while (activeTetrominoType == TetrominoType.Empty)
                    activeTetrominoType = (TetrominoType)_randomizer.Next(2);

                ActiveTetromino = new Tetromino(this, activeTetrominoType);
            }

            TetrominoType tetrominoType = TetrominoType.Empty;
            while (tetrominoType == TetrominoType.Empty)
                tetrominoType = (TetrominoType)_randomizer.Next(8);

            Tetromino tetromino = new Tetromino(this, tetrominoType);

            NextTetromino = tetromino;

            switch (ActiveTetromino.TetrominoType)
            {
                case TetrominoType.I:
                    TetrisStatictics.ITetrominoCount++;
                    break;
                case TetrominoType.J:
                    TetrisStatictics.JTetrominoCount++;
                    break;
                case TetrominoType.L:
                    TetrisStatictics.LTetrominoCount++;
                    break;
                case TetrominoType.O:
                    TetrisStatictics.OTetrominoCount++;
                    break;
                case TetrominoType.S:
                    TetrisStatictics.STetrominoCount++;
                    break;
                case TetrominoType.T:
                    TetrisStatictics.TTetrominoCount++;
                    break;
                case TetrominoType.Z:
                    TetrisStatictics.ZTetrominoCount++;
                    break;
            }
        }

        public void FinishGame()
        {
            TetrisStatictics.GameOver = true;
        }

        public void InitializeLevel()
        {
            TetrisStatictics = new TetrisStatistics();
            LevelMatrix = new List<TetrominoType[]>();
            for (int y = 0; y < LevelSize.Height; y++)
                LevelMatrix.Add(new TetrominoType[LevelSize.Width]);

            AddNextTetromino();
        }

        public void Render(Graphics graphics)
        {
            RotateLevel(graphics);

            RenderLevel(graphics);
            RenderLines(graphics);
            RenderNextTetromino(graphics);
            RenderTetrisCanvas(graphics);
            RenderScore(graphics);
            RenderStatistics(graphics);
            RenderFPS(graphics);
            if (TetrisStatictics.FirstPersonMode)
            {
                SizeF levelSize = new SizeF((TetrisLevel.XMARGIN + (LevelSize.Width * TetrominoDrawing.SQUARE_SIZE) / 2f),
                                            (TetrisLevel.YMARGIN + (LevelSize.Height * TetrominoDrawing.SQUARE_SIZE) / 2f));

                switch (TetrisStatictics.Angle)
                {
                    case 0:
                    case 180:
                        graphics.TranslateTransform(levelSize.Height, levelSize.Width);
                        break;
                    case 90:
                    case 270:
                        graphics.TranslateTransform(levelSize.Height, levelSize.Width);
                        break;
                }
                
            }

            graphics.ResetTransform();
            RenderGameOver(graphics);
            RenderPaused(graphics);
        }

        public void Update(float deltaTime)
        {
            if (_pauseBlinkAccumulatedTime < 500f)
                _pauseBlinkAccumulatedTime += deltaTime;
            else
            {
                _pauseBlinkAccumulatedTime = 0;
                _pauseBlink = !_pauseBlink;
            }

            if (_gameOverBlinkAccumulatedTime < 500f)
                _gameOverBlinkAccumulatedTime += deltaTime;
            else
            {
                _gameOverBlinkAccumulatedTime = 0;
                _gameOverBlink = !_gameOverBlink;
            }

            if (Paused) return;
            if (TetrisStatictics.GameOver) return;

            if (!ActiveTetromino.Active)
            {
                CheckForTetris();
                AddNextTetromino();
                return;
            }

            int level = 5 * TetrisStatictics.Level;
            if (level < 999)
                level = 999;
            
            if (_accumulatedTime < level)
            {
                _accumulatedTime += deltaTime;
                return;
            }

            ActiveTetromino.Down();
            _accumulatedTime = 0;
        }

        #endregion
    }
}
