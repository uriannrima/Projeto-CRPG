using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : BaseSingleton<GameManager>
{

    public void Start()
    {
        CameraInput cameraInput = null;
        if (TryFindObjectOfType<CameraInput>(out cameraInput))
        {
            cameraInput.InjectInput(InputManager.Instance);
        }

        SelectionManager.Instance.InjectInput(InputManager.Instance);
    }
}
