using UnityEngine;
using System.Collections;
using System;

public class MouseManager : BaseMonoBehavior, IInputInjected
{
    public IInputProxy InputProxy
    {
        get;
        private set;
    }

    public void ClearInput()
    {
        InputProxy = null;
    }

    public void InjectInput(IInputProxy input)
    {
        InputProxy = input;
    }

    // Update is called once per frame
    void Update()
    {
        HandleClick();
    }

    void HandleClick()
    {

    }
}
