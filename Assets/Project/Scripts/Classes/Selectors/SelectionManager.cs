using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SelectedEventArgs : EventArgs
{
    public SelectedEventArgs(GameObject selectedObject, string buttonName)
    {
        this.GameObject = selectedObject;
        this.ButtonName = buttonName;
    }
    public SelectedEventArgs(Vector3 selectedPosition, string buttonName)
    {
        this.Position = selectedPosition;
        this.ButtonName = buttonName;
    }

    public GameObject GameObject
    {
        get;
        private set;
    }

    public Vector3 Position
    {
        get;
        private set;
    }

    public String ButtonName
    {
        get;
        private set;
    }
}

public delegate void SelectedEventHandler(object sender, SelectedEventArgs e);

public class SelectionManager : BaseSingleton<SelectionManager>, IInputInjected
{
    public List<String> SelectableTags = new List<string>();

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
            // Check if the object were selectable.
            bool isSelectable = false;

            // Check each tag to see if it is selectable.
            foreach (string tag in SelectableTags)
            {
                if (Hit.transform.gameObject.CompareTag(tag))
                {
                    isSelectable = true;
                    break;
                }
            }

            // If the object is selectable
            if (isSelectable)
            {
                // Tell everyone that some selectable was selected.
                OnSelected(new SelectedEventArgs(Hit.transform.gameObject, "Fire1"));
            }
            else
            {
                // If not, just tell the click point.
                OnSelected(new SelectedEventArgs(Hit.point, "Fire1"));
            }
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

    public event SelectedEventHandler Selected;

    public void OnSelected(SelectedEventArgs e)
    {
        if (Selected != null)
        {
            Selected(this, e);
        }
    }
}
