namespace FPSTetris.GameObjects
{
    public class TetrisStatistics
    {
        #region Constructor

        public TetrisStatistics()
        {
        }

        #endregion

        #region Attributes and Properties

        public int ITetrominoCount { get; set; }
        public int JTetrominoCount { get; set; }
        public int LTetrominoCount { get; set; }
        public int OTetrominoCount { get; set; }
        public int STetrominoCount { get; set; }
        public int TTetrominoCount { get; set; }
        public int ZTetrominoCount { get; set; }

        private int _angle;
        public int Angle
        {
            get { return _angle; }
            set 
            {
                _angle = value;
                if (_angle == -90)
                    _angle = 270;
                if (_angle == 360)
                    _angle = 0;
            } 
        }

        public bool FirstPersonMode { get; set; }
        public bool GameOver { get; set; }
        public int Level { get; set; }
        public int RemovedLines { get; set; }
        public int Score { get; set; }

        #endregion

        #region Public Methods

        public int GetTetrominoCount(TetrominoType tetrominoType)
        {
            switch (tetrominoType)
            {
                case TetrominoType.I:
                    return ITetrominoCount;
                case TetrominoType.J:
                    return JTetrominoCount;
                case TetrominoType.L:
                    return LTetrominoCount;
                case TetrominoType.O:
                    return OTetrominoCount;
                case TetrominoType.S:
                    return STetrominoCount;
                case TetrominoType.T:
                    return TTetrominoCount;
                case TetrominoType.Z:
                    return ZTetrominoCount;
            }

            return -1;
        }

        #endregion
    }
}
