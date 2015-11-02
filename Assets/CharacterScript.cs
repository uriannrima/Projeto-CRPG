using UnityEngine;
using System.Collections;



public class CharacterScript : MonoBehaviour
{
    private string[] Names = new string[3] { "Hunar", "Uriann", "Khnemu" };
    public string Name = "";

    // Use this for initialization
    void Start()
    {
        Name = Names[Random.Range(0, 2)];
    }
}
