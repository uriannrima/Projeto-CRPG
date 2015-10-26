using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : BaseSingleton<GameManager>
{
    public List<GameObject> SelectedObjects = new List<GameObject>();

    public void Start()
    {
        CameraInput cameraInput = null;
        if (TryFindObjectOfType<CameraInput>(out cameraInput))
        {
            cameraInput.InjectInput(InputManager.Instance);
        }

        SelectionManager.Instance.InjectInput(InputManager.Instance);
        SelectionManager.Instance.Selected += new SelectedEventHandler(Instance_Selected);
    }

    void Instance_Selected(object sender, SelectedEventArgs e)
    {
        // Some gameObject was selected
        // And it isn't selected yet.
        if (e.GameObject != null && !SelectedObjects.Contains(e.GameObject))
        {
            // Add it to the selected gameObjects.
            SelectedObjects.Add(e.GameObject);

            // And select it.
            var selectionController = e.GameObject.GetComponent<SelectionController>();
            if (selectionController)
            {
                selectionController.Select();
            }
        }
        // No gameObject was selected, so a position was selected.
        else if (e.GameObject == null)
        {
            // Deselect everyone.
            foreach (GameObject gameObject in SelectedObjects)
            {
                var selectionController = gameObject.GetComponent<SelectionController>();
                if (selectionController)
                {
                    selectionController.Deselect();
                }
            }

            // Clear the selected gameObject list.
            SelectedObjects.Clear();
        }
    }
}
