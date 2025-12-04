using Scripts.Entity;
using UnityEngine;

namespace Scripts.Main
{
    public class CornerElement : MonoBehaviour
    {
        private Coord _coord;

        private MeshFilter _meshFilter;

        [SerializeField]
        private int _bitMaskValue;

        [SerializeField]
        private GridElement[] _nearGridElements = new GridElement[8];
        public void Initialize(int setX, int setY, int setZ)
        {
            _coord = new Coord(setX, setY, setZ);

            name = $"CE_{_coord.X}_{_coord.Y}_{_coord.Z}";

            _meshFilter = transform.GetComponent<MeshFilter>();
        }

        public Coord GetCoord()
        {
            return _coord;
        }

        public void SetPosition(float setX, float setY, float setZ)
        {
            transform.position = new Vector3(setX, setY, setZ);
        }

        public void SetNearGridElements(GridElement[] nearGridElements)
        {
            _nearGridElements = nearGridElements;
        }

        public void SetCornerBitMaskValue()
        {
            _bitMaskValue = CalculateProcessor.GetBitMaskValue(_nearGridElements);

            _meshFilter.mesh = CornerMeshes.Instance.GetCornerMesh(_bitMaskValue, _coord.Y);
        }
    }
}