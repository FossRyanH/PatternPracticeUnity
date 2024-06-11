using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePointDetector : MonoBehaviour
{
    [SerializeField] private LayerMask clickMask;
    private Camera _camera;

    public event Action<Vector3> OnPointDetected;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit RaycastHit, clickMask))
            {
                OnPointDetected?.Invoke(RaycastHit.point);
            }
        }
    }
}
