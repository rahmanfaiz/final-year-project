using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RTSCamera : MonoBehaviour
{
    [SerializeField] Camera cam;
    public Camera mainCamera { get { return cam; } }

    [Header("Panning with Mouse and Keyboard")]
    [SerializeField] float panSpeed = 20f;
    [SerializeField] float panBorderThickness = 15f;
    [SerializeField] Vector2 panLimit;

    [Header("Zoom with Scroll")]
    [SerializeField] float maxZoom = 5f;
    [SerializeField] float minZoom = 20f;
    [SerializeField] float sensitivity = 1f;
    [SerializeField] float speed = 20f;
    float targetZoom;

    private void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector2 panBorder = new Vector2 ();

        panBorder.x = Screen.width - panBorderThickness;
        panBorder.y = Screen.height - panBorderThickness;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= panBorder.y)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= panBorder.x)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        targetZoom -= Input.mouseScrollDelta.y * sensitivity;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
        float newSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, speed * Time.deltaTime);
        cam.orthographicSize = newSize;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}
