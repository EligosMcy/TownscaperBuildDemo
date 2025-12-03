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

        private CalculateProcessor _calculateProcessor;

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

            _calculateProcessor = new CalculateProcessor();

            spawnCornerElements();

            spawnGridElement();
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

                        _gridElements[x + _width * (z + _width * y)] = gridElement;
                    }
                }
            }
        }

        public GridElement GetProcessGrid(Coord currentCoord, GridEventEnum gridEventEnum)
        {
            return _calculateProcessor.GetProcessGrid(_width, _height, _gridElements, currentCoord, gridEventEnum);
        }

        public CornerElement[] GetGridCornerElements(Coord currentCoord)
        {
            return _calculateProcessor.GetGridCornerElements(_widthPlusOne, _heightPlusOne, _cornerElements, currentCoord);
        }

        public void ProcessCornerElementsPosition(Bounds bounds, CornerElement[] targetCornerElements)
        {
            _calculateProcessor.ProcessCornerElementsPosition(bounds, targetCornerElements);
        }
    }
}