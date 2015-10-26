using UnityEngine;
using System.Collections;

public class RotateScript : BaseMonoBehavior
{
    [Range(2f, 10f)]
    public float RotateSpeed = 5f;
    public Vector3 Axis = Vector3.up;

    void FixedUpdate()
    {
        this.transform.Rotate(this.transform.up, RotateSpeed * Time.deltaTime * 10f);
    }
}
