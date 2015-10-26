using UnityEngine;
using System.Collections;
using System;


public class ObjectSelectedEventArgs : EventArgs
{


    public GameObject Objects
    {
        get;
        set;
    }
}

public delegate void ObjectSelectedEventHandler(object sender, EventArgs e);

public class SelectionManager : BaseSingleton<SelectionManager>, IInputInjected
{
    /// <summary>
    /// Raycast used to store the ray information.
    /// </summary>
    RaycastHit Hit;

    public IInputProxy InputProxy
    {
        get;
        private set;
    }

    public void Update()
    {
        if (InputProxy.GetButtonUp("Fire1") && CastRay(InputProxy.MousePosition))
        {
            Debug.Log(Hit.transform.gameObject);

        }
    }

    /// <summary>
    /// Uses Physics.Raycast to find out if hitted something using the main camera.
    /// </summary>
    /// <param name="selectionPosition">Position to be used from Screen Point to Ray.</param>
    /// <returns></returns>
    private bool CastRay(Vector3 selectionPosition, float distance = 100f)
    {
        return Physics.Raycast(Camera.main.ScreenPointToRay(selectionPosition), out Hit, distance);
    }

    public void InjectInput(IInputProxy input)
    {
        InputProxy = input;
    }

    public void ClearInput()
    {
        InputProxy = null;
    }

    public ObjectSelectedEventHandler ObjectSelected;

    public void OnObjectSelected(EventArgs e)
    {
        if (ObjectSelected != null)
        {
            ObjectSelected(this, e);
        }
    }
}
