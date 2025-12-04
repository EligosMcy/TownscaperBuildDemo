using System;
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

        private float _elementHeight;

        [SerializeField]
        private CornerElement[] _corners = new CornerElement[8];

        public void Initialize(int setX, int setY, int setZ, float elementHeight)
        {
            _collider = transform.GetComponent<Collider>();

            _renderer = transform.GetComponent<Renderer>();

            _coord = new Coord(setX, setY, setZ);

            _elementHeight = elementHeight;

            name = $"GE_{_coord.X}_{_coord.Y}_{_coord.Z}";

            transform.localScale = new Vector3(1, _elementHeight, 1);

            Physics.SyncTransforms();

            _corners = LevelGenerator.Instance.GetGridForCornerElements(_coord);

            LevelGenerator.Instance.ProcessCornerElementsPosition(_collider, _corners);
        }

        public Coord GetCoord() { return _coord; }

        public void SetEnable()
        {
            _isEnable = true;

            _collider.enabled = true;

            // _renderer.enabled = true;

            setCornerBitMaskValue();
        }

        public void SetDisable()
        {
            _isEnable = false;

            _collider.enabled = false;

            // _renderer.enabled = false;

            setCornerBitMaskValue();
        }


        public bool GetEnable()
        {
            return _isEnable;
        }

        public float GetElementHeight()
        {
            return _elementHeight;
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