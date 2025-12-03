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

        private readonly string _gridElementTag = "GridElement";

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

            Debug.Log($"Current Coord: {currentCoord} , GridEvent: {gridEventEnum} Bool: {targetGrid != null}");

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

                Debug.Log($"Target: {targetGrid.GetCoord()}");
            }
        }

        public void SetCursorButtonByInt(int inputGridEventInt)
        {
            SetCursorButton((GridEventEnum)inputGridEventInt);
        }
    }
}