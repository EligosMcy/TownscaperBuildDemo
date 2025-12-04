using Scripts.Entity;
using UnityEngine;

namespace Scripts.Main
{
    public class GridElement : MonoBehaviour
    {
        private Coord _coord;

        private Collider _collider;

        private Renderer _renderer;

        private bool _isEnable = false;

        [SerializeField]
        private CornerElement[] _corners = new CornerElement[8];

        public void Initialize(int setX, int setY, int setZ)
        {
            _coord = new Coord(setX, setY, setZ);

            _collider = transform.GetComponent<Collider>();

            _renderer = transform.GetComponent<Renderer>();

            name = $"GE_{_coord.X}_{_coord.Y}_{_coord.Z}";

            _corners = LevelGenerator.Instance.GetGridForCornerElements(_coord);

            Bounds bounds = _collider.bounds;

            LevelGenerator.Instance.ProcessCornerElementsPosition(bounds, _corners);
        }

        public Coord GetCoord() { return _coord; }

        public void SetEnable()
        {
            _isEnable = true;

            _collider.enabled = true;

            _renderer.enabled = true;

            setCornerBitMaskValue();
        }

        public void SetDisable()
        {
            _isEnable = false;

            _collider.enabled = false;

            _renderer.enabled = false;

            setCornerBitMaskValue();
        }


        public bool GetEnable()
        {
            return _isEnable;
        }

        private void setCornerBitMaskValue()
        {
            foreach (CornerElement cornerElement in _corners)
            {
                cornerElement.SetCornerBitMaskValue();
            }
        }
    }
}