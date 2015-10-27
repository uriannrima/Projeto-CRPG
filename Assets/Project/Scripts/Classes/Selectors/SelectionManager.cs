using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Class responsible for doing selection logic: Get mouse input, check colision and sent event.
/// Everytime something or some location is selected, a SelectedEvent is raised.
/// </summary>
public class SelectionManager : BaseSingleton<SelectionManager>, IInputInjected
{
    #region IInputInjected Implementation

    public IInputProxy InputProxy
    {
        get;
        private set;
    }

    public void InjectInput(IInputProxy input)
    {
        InputProxy = input;
    }

    public void ClearInput()
    {
        InputProxy = null;
    }

    #endregion

    public List<string> SelectableTags = new List<string>();

    public List<string> Buttons = new List<string>();

    public GameObject HighlightedObject;

    /// <summary>
    /// Raycast used to store the ray information.
    /// </summary>
    RaycastHit Hit;

    public void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // We hit something...
        if (CastRay(InputProxy.MousePosition))
        {
            // Save object.
            GameObject gameObjectHit = Hit.transform.gameObject;

            // Handle Highlight.
            HandleHighlight(gameObjectHit);

            HandleSelection(gameObjectHit, Hit.point);
        }
    }

    private void HandleSelection(GameObject gameObjectHit, Vector3 positionHit)
    {
        // For each valid selection button
        foreach (string button in Buttons)
        {
            // Check if it was pressed.
            if (InputProxy.GetButtonUp(button))
            {
                // Flag to check if something is selectable.
                bool isSelectable = false;

                // Check if the object has some of the selectable tags.
                foreach (string tag in SelectableTags)
                {
                    if (gameObjectHit.CompareTag(tag))
                    {
                        // It does, so set selectable to true and break.
                        isSelectable = true;
                        break;
                    }
                }

                // If is selectable, raise event
                if (isSelectable)
                {
                    OnSelected(new SelectedEventArgs(gameObjectHit, button));
                }
                // Just tell that some point was clicked.
                else
                {
                    OnSelected(new SelectedEventArgs(positionHit, button));
                }
            }
        }
    }

    void HandleHighlight(GameObject gameObjectHit)
    {
        // If something was previously highlighted by the mouse
        // And the previous highlighted object isn't this one, clear the previous one.
        if (HighlightedObject != null && gameObjectHit != HighlightedObject)
        {
            HighlightedObject.GetComponent<Highlightable>().Dehighlight();
            HighlightedObject = null;
        }

        // If the new object is highlightable, highlight it.
        var highlightable = gameObjectHit.GetComponent<Highlightable>();
        if (highlightable)
        {
            HighlightedObject = gameObjectHit;
            highlightable.Highlight();
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

    /// <summary>
    /// Selected Event Handler.
    /// </summary>
    public event SelectedEventHandler Selected;

    /// <summary>
    /// Method called when something is selected.
    /// </summary>
    /// <param name="e"></param>
    public void OnSelected(SelectedEventArgs e)
    {
        if (Selected != null)
        {
            Selected(this, e);
        }
    }
}
