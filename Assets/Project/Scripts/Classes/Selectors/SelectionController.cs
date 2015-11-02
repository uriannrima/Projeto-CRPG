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
        else
        {
            if (e.SelectionButton == "Fire2")
            {
                // Show context menu
                UIManager.Instance.ShowMenu(e.Position);
            }
        }
    }

    private void HandleObjectSelection(SelectedEventArgs e)
    {
        if (e.SelectionButton == "Fire2") // Right Button Pressed
        {
            // There are selected objects
            if (SelectedObjects.Count != 0)
            {
                // If the actual selected game object is a enemy
                // And was assigned to attack (Fire2)
                if (e.GameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Units will attack " + e.GameObject);
                }
            }
        }
        else if (e.SelectionButton == "Fire1") // Left button Pressed
        {
            // A character was selected
            if (e.GameObject.CompareTag("Character"))
            {
                // If shift (fire3) is pressed, add to the selected list
                if (e.IsCombined && e.ButtonsPressed.Contains("Fire3"))
                {
                    // Already at list
                    if (SelectedObjects.Contains(e.GameObject))
                    {
                        // Remove it from selected
                        SelectedObjects.Remove(e.GameObject);
                        e.GameObject.GetComponent<Selectable>().Deselect();
                    }
                    // Not at list
                    else
                    {
                        // Add the object to the selected list
                        SelectedObjects.Add(e.GameObject);

                        // And select it.
                        var Selectable = e.GameObject.GetComponent<Selectable>();
                        if (Selectable)
                        {
                            Selectable.Select();
                        }
                    }                    
                }
                // Character was simple selected, so we must change the selection to him
                else
                {
                    // Deselect all selected objects
                    foreach (GameObject gameObject in SelectedObjects)
                    {
                        gameObject.GetComponent<Selectable>().Deselect();
                    }

                    // Clear selected list
                    SelectedObjects.Clear();

                    // Add the object to the selected list
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
    }
}
