using UnityEngine;
using System.Collections;
using System;

public class CameraController : BaseSingleton<CameraController>, ICameraController
{
    Vector3 movementDirection;

    [Range(0.5f, 20f)]
    public float movementSpeed = 10f;

    [Range(0.5f, 3f)]
    public float rotateSpeed = 1f;

    [Range(2f, 4f)]
    public float zoomSpeed = 3f;

    public int maxZoomIn = -9;
    public int maxZoomOut = -15;
    public GameObject Camera;

    public void Start()
    {

    }

    public void Update()
    {
        movementDirection.Set(0, 0, 0);
    }

    public void FixedUpdate()
    {
        this.transform.Translate(movementSpeed * movementDirection * Time.deltaTime);
    }

    public void Backward(float intensity = 1)
    {
        movementDirection.Set(movementDirection.x, movementDirection.y, -intensity);
    }

    public void Forward(float intensity = 1)
    {
        movementDirection.Set(movementDirection.x, movementDirection.y, intensity);
    }

    public void Left(float intensity = 1)
    {
        movementDirection.Set(-intensity, movementDirection.y, movementDirection.z);
    }

    public void Right(float intensity = 1)
    {
        movementDirection.Set(intensity, movementDirection.y, movementDirection.z);
    }

    public void RotateLeft(float intensity = 1)
    {
        this.transform.Rotate(this.transform.up, -intensity * rotateSpeed);
    }

    public void RotateRight(float intensity = 1)
    {
        this.transform.Rotate(this.transform.up, intensity * rotateSpeed);
    }

    public void ZoomIn(float intensity = 1)
    {
        if (!Camera) return;
        if (Camera.transform.localPosition.z < maxZoomIn)
        {
            Camera.transform.Translate(Camera.transform.forward * zoomSpeed * intensity, Space.World);
        }
    }

    public void ZoomOut(float intensity = 1)
    {
        if (!Camera) return;
        if (Camera.transform.localPosition.z > maxZoomOut)
        {
            Camera.transform.Translate(-Camera.transform.forward * zoomSpeed * intensity, Space.World);
        }
    }

    public void Focus(Vector3 position)
    {
        StopAllCoroutines();
        StartCoroutine(MoveTo(position));
    }

    public IEnumerator MoveTo(Vector3 position)
    {
        while ((this.transform.position - position).sqrMagnitude > 0.5f)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, position, Time.deltaTime);
            yield return null;
        }
    }
}

public interface ICameraController
{
    void Forward(float intensity = 1);

    void Backward(float intensity = 1);

    void Left(float intensity = 1);

    void Right(float intensity = 1);

    void ZoomIn(float intensity = 1);

    void ZoomOut(float intensity = 1);

    void RotateLeft(float intensity = 1);

    void RotateRight(float intensity = 1);

    void Focus(Vector3 position);
}

