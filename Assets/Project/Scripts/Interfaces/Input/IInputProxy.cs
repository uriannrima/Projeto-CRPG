using UnityEngine;
using System.Collections;

public interface IInputProxy
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="axisName"></param>
    /// <returns></returns>
    float GetAxis(string axisName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="axisName"></param>
    /// <returns></returns>
    float GetAxisRaw(string axisName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buttonName"></param>
    /// <returns></returns>
    bool GetButton(string buttonName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buttonName"></param>
    /// <returns></returns>
    bool GetButtonDown(string buttonName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buttonName"></param>
    /// <returns></returns>
    bool GetButtonUp(string buttonName);

    /// <summary>
    /// 
    /// </summary>
    Vector3 MousePosition
    {
        get;
    }
}

public interface IInputInjected
{
    /// <summary>
    /// Proxy to the inputObject.
    /// </summary>
    IInputProxy InputProxy
    {
        get;
    }

    /// <summary>
    /// Define gameObject inputObject.
    /// </summary>
    /// <param name="input"></param>
    void InjectInput(IInputProxy input);

    /// <summary>
    /// Clear gameObject inputObject.
    /// </summary>
    void ClearInput();
}
