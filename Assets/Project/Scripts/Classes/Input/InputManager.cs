using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class InputManager : BaseSingleton<InputManager>, IInputProxy
{
    public float GetAxis(string axisName)
    {
        return CrossPlatformInputManager.GetAxis(axisName);
    }

    public float GetAxisRaw(string axisName)
    {
        return CrossPlatformInputManager.GetAxisRaw(axisName);
    }

    public bool GetButton(string buttonName)
    {
        return CrossPlatformInputManager.GetButton(buttonName);
    }

    public bool GetButtonDown(string buttonName)
    {
        return CrossPlatformInputManager.GetButtonDown(buttonName);
    }

    public bool GetButtonUp(string buttonName)
    {
        return CrossPlatformInputManager.GetButtonUp(buttonName);
    }

    public Vector3 MousePosition
    {
        get
        {
            return CrossPlatformInputManager.mousePosition;
        }
    }
}
