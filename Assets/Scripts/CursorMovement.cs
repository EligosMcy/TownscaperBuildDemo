using System;
using UnityEngine;

namespace Scripts
{
    public class CursorMovement : MonoBehaviour
    {
        private RaycastHit _raycastHit;

        private Ray _ray;

        private readonly string _gridElementTag = "GridElement";

        private void Update()
        {
            if (Camera.main == null) return;

            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _raycastHit))
            {
                if (!_raycastHit.transform.CompareTag(_gridElementTag)) return;

                if (!_raycastHit.transform.TryGetComponent(out GridElement gridElement)) return;

                transform.position = _raycastHit.collider.transform.position;
            }
        }
    }
}