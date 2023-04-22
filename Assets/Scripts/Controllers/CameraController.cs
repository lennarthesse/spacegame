using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public GameObject focusedObject;

    [SerializeField] private float maxZoom = 5;
    [SerializeField] private float minZoom = 200;
    private float sensitivity = 1;
    private float targetZoom;

    private Rigidbody2D rigidbody2d;
    private Rigidbody2D targetRigidbody2d;

    void Start() {
        targetZoom = cam.orthographicSize;
        rigidbody2d = GetComponent<Rigidbody2D>();
        targetRigidbody2d = focusedObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        ZoomCamera();

        Vector3 targetPosition = new Vector3(
            focusedObject.transform.position.x,
            focusedObject.transform.position.y,
            this.transform.position.z
        );

        Vector2 distance = targetRigidbody2d.position - rigidbody2d.position;

        rigidbody2d.velocity = Vector2.Lerp(rigidbody2d.velocity, targetRigidbody2d.velocity + distance, 0.01f);
    }

    void ZoomCamera() {
        targetZoom -= Input.mouseScrollDelta.y * sensitivity;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
        cam.orthographicSize = targetZoom;
        sensitivity = targetZoom / maxZoom;
    }
}
