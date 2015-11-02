using UnityEngine;
using System.Collections;

public class CameraManager : BaseSingleton<CameraManager>
{
    public GameObject CameraReference;

    public void Focus(Vector3 worldPosition)
    {
        if (CameraReference)
        {
            CameraReference.transform.position = worldPosition;
        }
    }
}
