using UnityEngine;
using System;

/// <summary>
/// Selected Event Handler.
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void SelectedEventHandler(object sender, SelectedEventArgs e);

/// <summary>
/// Arguments sent to Selected Event.
/// </summary>
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

    public string ButtonName
    {
        get;
        private set;
    }
}
