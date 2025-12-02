using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int _width = 5;

    private int _height = 10;

    [SerializeField]
    private Transform _gridElementGroup;

    [Space(20)]
    [SerializeField]
    private GridElement _gridElement;

    [SerializeField]
    private GridElement[] _gridElements;

    void Start()
    {
        spawnGridElement();
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
                    gridElement.name = $"GridElement_{x}_{y}_{z}";

                    _gridElements[x + _width * (z + _width * y)] = gridElement;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}