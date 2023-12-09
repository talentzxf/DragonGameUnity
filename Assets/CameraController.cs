using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 3.0f;

    private Transform _transform;
    private Transform _parentTransform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _parentTransform = _transform.parent;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 parentPosition = _parentTransform.position;
        
        Vector3 offset = _transform.position - parentPosition;
        float horizontalInput = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float verticalInput = -Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // horizontalInput = Math.Clamp(horizontalInput, -90, 90);
        // verticalInput = Math.Clamp(verticalInput, -90, 90);
        
        Quaternion rotation = Quaternion.Euler(verticalInput, horizontalInput, 0);
        Debug.Log("");
        Vector3 newPosition = parentPosition + rotation * offset;

        _transform.position = newPosition;
        _transform.LookAt(parentPosition);
    }
}