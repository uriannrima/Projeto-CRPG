using System;
using UnityEngine;

public class CameraInput : BaseMonoBehavior, IInputInjected
{
    #region IInputInjected Implementation
    public IInputProxy InputProxy
    {
        get;
        private set;
    }

    public void InjectInput(IInputProxy inputHandler)
    {
        this.InputProxy = inputHandler;
        this.enabled = true;
    }

    public void ClearInput()
    {
        InputProxy = null;
        this.enabled = false;
    }
    #endregion

    ICameraController CameraController;

    public bool EnableSmoothMovement = false;
    public bool EnableEdge = false;
    public int edgeDistance = 50;

    public void Start()
    {
        CameraController = GetInterfaceComponent<ICameraController>();
    }

    public void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
        HandleEdge();
    }

    void HandleMovement()
    {
        float horizontal = (EnableSmoothMovement) ? InputProxy.GetAxis("Horizontal") : InputProxy.GetAxisRaw("Horizontal");
        if (horizontal > 0)
        {
            CameraController.Right(horizontal);
        }
        else if (horizontal < 0)
        {
            CameraController.Left(-horizontal);
        }

        float vertical = (EnableSmoothMovement) ? InputProxy.GetAxis("Vertical") : InputProxy.GetAxisRaw("Vertical");
        if (vertical > 0)
        {
            CameraController.Forward(vertical);
        }
        else if (vertical < 0)
        {
            CameraController.Backward(-vertical);
        }
    }

    private void HandleRotation()
    {
        float rotation = (EnableSmoothMovement) ? InputProxy.GetAxis("Rotation") : InputProxy.GetAxisRaw("Rotation");
        if (rotation > 0)
        {
            CameraController.RotateLeft(rotation);
        }
        else if (rotation < 0)
        {
            CameraController.RotateRight(-rotation);
        }
    }

    private void HandleZoom()
    {
        float scroll = (EnableSmoothMovement) ? InputProxy.GetAxis("Mouse ScrollWheel") : InputProxy.GetAxisRaw("Mouse ScrollWheel");
        if (scroll > 0)
        {
            CameraController.ZoomIn(scroll);
        }
        else if (scroll < 0)
        {
            CameraController.ZoomOut(-scroll);
        }
    }

    private void HandleEdge()
    {
        if (!EnableEdge) return;

        if (InputProxy.MousePosition.x <= edgeDistance)
        {
            CameraController.Left();
        }
        else if (InputProxy.MousePosition.x >= Screen.width - edgeDistance)
        {
            CameraController.Right();
        }

        if (InputProxy.MousePosition.y <= edgeDistance)
        {
            CameraController.Backward();
        }
        else if (InputProxy.MousePosition.y >= Screen.height - edgeDistance)
        {
            CameraController.Forward();
        }
    }
}
