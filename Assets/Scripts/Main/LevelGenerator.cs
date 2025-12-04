using Scripts.Entity;
using UnityEngine;

namespace Scripts.Main
{
    public class LevelGenerator : MonoBehaviour
    {
        public static LevelGenerator Instance;

        //
        [Range(1, 15)]
        [SerializeField]
        private int _width = 5;

        [Range(1, 15)]
        [SerializeField]
        private int _height = 10;

        private int _widthPlusOne => _width + 1;

        private int _heightPlusOne => _height + 1;

        [Space(20)]
        [SerializeField]
        private Transform _gridElementGroup;

        [SerializeField]
        private GridElement _gridElement;

        [SerializeField]
        private GridElement[] _gridElements;

        [Space(20)]
        [SerializeField]
        private Transform _cornerElementGroup;

        [SerializeField]
        private CornerElement _cornerElement;

        [SerializeField]
        private CornerElement[] _cornerElements;

        void Start()
        {
            Instance = this;

            spawnCornerElements();

            spawnGridElement();

            getNearGrid();

            getCornerBitMaskValue();
        }

        private void getNearGrid()
        {
            foreach (CornerElement cornerElement in _cornerElements)
            {
                Coord currentCoord = cornerElement.GetCoord();

                GridElement[] nearGridElements = GetCornerNearGridElements(currentCoord);

                cornerElement.SetNearGridElements(nearGridElements);
            }
        }

        private void getCornerBitMaskValue()
        {
            foreach (CornerElement cornerElement in _cornerElements)
            {
                cornerElement.SetCornerBitMaskValue();
            }
        }

        private void spawnCornerElements()
        {
            _cornerElements = new CornerElement[(_heightPlusOne) * (_widthPlusOne) * (_widthPlusOne)];

            for (int y = 0; y < _heightPlusOne; y++)
            {
                for (int x = 0; x < _widthPlusOne; x++)
                {
                    for (int z = 0; z < _widthPlusOne; z++)
                    {
                        CornerElement cornerElement = Instantiate(_cornerElement, Vector3.zero, Quaternion.identity, _cornerElementGroup);

                        cornerElement.Initialize(x, y, z);

                        _cornerElements[x + _widthPlusOne * (z + _widthPlusOne * y)] = cornerElement;
                    }
                }
            }
        }

        private void spawnGridElement()
        {
            _gridElements = new GridElement[_height * _width * _width];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    for (int z = 0; z < _width; z++)
                    {
                        GridElement gridElement = Instantiate(_gridElement, new Vector3(x, y, z), Quaternion.identity, _gridElementGroup);

                        gridElement.Initialize(x, y, z);

                        gridElement.SetEnable();

                        _gridElements[x + _width * (z + _width * y)] = gridElement;
                    }
                }
            }
        }

        public GridElement GetProcessGrid(Coord currentCoord, GridEventEnum gridEventEnum)
        {
            return CalculateProcessor.GetProcessGrid(_width, _height, _gridElements, currentCoord, gridEventEnum);
        }

        public CornerElement[] GetGridForCornerElements(Coord currentCoord)
        {
            return CalculateProcessor.GetGridForCornerElements(_widthPlusOne, _heightPlusOne, _cornerElements, currentCoord);
        }

        public GridElement[] GetCornerNearGridElements(Coord currentCoord)
        {
            return CalculateProcessor.GetCornerNearGridElements(_width, _height, _gridElements, currentCoord);
        }

        public void ProcessCornerElementsPosition(Bounds bounds, CornerElement[] targetCornerElements)
        {
            CalculateProcessor.ProcessCornerElementsPosition(bounds, targetCornerElements);
        }
    }
}