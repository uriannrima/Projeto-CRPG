using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Implementation of what happens when something is selected.
/// </summary>
public class SelectionController : BaseMonoBehavior
{
    public List<GameObject> SelectedObjects = new List<GameObject>();

    void Start()
    {
        SelectionManager.Instance.Selected += Instance_Selected;
    }

    private void Instance_Selected(object sender, SelectedEventArgs e)
    {
        // Some gameObject was selected
        if (e.GameObject != null)
        {
            HandleObjectSelection(e);
        }
        // No gameObject was selected, so a position was selected.
        else
        {
            HandlePositionSelection(e);
        }
    }

    private void HandlePositionSelection(SelectedEventArgs e)
    {
        // If someone is selected
        if (SelectedObjects.Count != 0)
        {
            Debug.Log("Tell selected units to move to point " + e.Position);
        }
    }

    private void HandleObjectSelection(SelectedEventArgs e)
    {
        // If someone is selected.
        if (SelectedObjects.Count != 0)
        {
            // If the actual selected game object is a enemy
            // And was assigned to attack (Fire2)
            if (e.GameObject.CompareTag("Enemy") && e.ButtonName == "Fire2")
            {
                Debug.Log("Units will attack " + e.GameObject);
            }
        }
        else
        {
            // Add it to the selected gameObjects.
            SelectedObjects.Add(e.GameObject);

            // And select it.
            var Selectable = e.GameObject.GetComponent<Selectable>();
            if (Selectable)
            {
                Selectable.Select();
            }
        }
    }
}
