using System;
using Scripts.Entity;
using UnityEngine;

namespace Scripts.Main
{
    public class CursorMovement : MonoBehaviour
    {
        private RaycastHit _raycastHit;

        private Ray _ray;

        private GridElement _lastGridElement;

        private RectTransform _rectTransform;

        private readonly string _gridElementTag = "GridElement";

        private void Start()
        {
            _rectTransform = transform.Find("Cursor Canvas").GetComponent<RectTransform>();

            _rectTransform.sizeDelta = Vector2.zero;
        }

        private void Update()
        {
            if (Camera.main == null) return;

            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _raycastHit))
            {
                Transform colliderTran = _raycastHit.collider.transform;
                //
                if (!colliderTran.CompareTag(_gridElementTag)) return;

                if (!colliderTran.TryGetComponent(out GridElement gridElement)) return;

                if (Input.GetMouseButtonDown(1))
                {
                    SetCursorButtonByInt(0);
                }

                if (_lastGridElement == gridElement) return;

                _lastGridElement = gridElement;

                float elementHeight = _lastGridElement.GetElementHeight();

                _rectTransform.sizeDelta = new Vector2(1, elementHeight);

                transform.position = colliderTran.position;
            }
        }

        public void SetCursorButton(GridEventEnum gridEventEnum)
        {
            if (_lastGridElement == null)
            {
                Debug.LogError($"Last Grid Null {gridEventEnum}");
                return;
            }

            Coord currentCoord = _lastGridElement.GetCoord();

            GridElement targetGrid = LevelGenerator.Instance.GetProcessGrid(currentCoord, gridEventEnum);

            if (targetGrid != null)
            {
                switch (gridEventEnum)
                {
                    case GridEventEnum.Remove:
                        targetGrid.SetDisable();
                        break;
                    case GridEventEnum.AddXPos:
                    case GridEventEnum.AddXNeg:
                    case GridEventEnum.AddZPos:
                    case GridEventEnum.AddZNeg:
                    case GridEventEnum.AddYPos:
                        targetGrid.SetEnable();
                        break;
                }
            }
        }

        public void SetCursorButtonByInt(int inputGridEventInt)
        {
            SetCursorButton((GridEventEnum)inputGridEventInt);
        }
    }
}