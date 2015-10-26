using UnityEngine;
using System.Collections;

public class GameManager : BaseSingleton<GameManager>
{
    public void Start()
    {
        CameraInput cameraInput = null;
        if (TryFindObjectOfType<CameraInput>(out cameraInput))
        {
            cameraInput.InjectInput(InputManager.Instance);
        }

        SelectionManager singleSelector = null;
        if (TryFindObjectOfType<SelectionManager>(out singleSelector))
        {
            singleSelector.InjectInput(InputManager.Instance);
        }
    }
}
