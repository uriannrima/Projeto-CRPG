using UnityEngine;
using System;
using System.Collections.Generic;

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
    public SelectedEventArgs(GameObject selectedObject, string buttonName, List<string> buttonsPressed = null)
    {
        this.GameObject = selectedObject;
        this.SelectionButton = buttonName;
        ButtonsPressed = buttonsPressed;
    }

    public SelectedEventArgs(Vector3 selectedPosition, string buttonName, List<string> buttonsPressed = null)
    {
        this.Position = selectedPosition;
        this.SelectionButton = buttonName;
        ButtonsPressed = buttonsPressed;
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

    public string SelectionButton
    {
        get;
        private set;
    }

    public List<string> ButtonsPressed
    {
        get;
        private set;
    }

    public bool IsCombined
    {
        get
        {
            return (ButtonsPressed != null && ButtonsPressed.Count > 0);
        }
    }
}
