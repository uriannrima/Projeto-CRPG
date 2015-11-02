using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestScript : MonoBehaviour
{
    List<GameObject> Characters;
    public int ActorIndex;
    public GameObject Actor;
    public bool ShowActionMenu = false;
    public bool ShowMoveLabel = false;
    public GameObject RangeIndicator;

    // Use this for initialization
    void Start()
    {
        // Create Range indicator
        RangeIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(RangeIndicator.GetComponent<SphereCollider>());
        RangeIndicator.SetActive(false);

        // Get all characters.
        Characters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Character"));
        Debug.Log(Characters.Count + " character(s) found");

        // Order Characters
        OrderCharactersByLuck();

        // Set first character to act.
        ActorIndex = 0;

        // Define Actor.
        DefineActor();
    }

    public void DefineActor()
    {
        Actor = Characters[ActorIndex];
        CameraController.Instance.Focus(Actor.transform.position);
        ShowActionMenu = true;
        Debug.Log(Actor.ToString() + "is acting...");
    }

    public void OrderCharactersByLuck()
    {
        List<GameObject> Reorder = new List<GameObject>();

        while (Characters.Count > 0)
        {
            int index = Random.Range(0, Characters.Count - 1);
            Reorder.Add(Characters[index]);
            Characters.RemoveAt(index);
        }

        Characters = Reorder;
    }

    void OnGUI()
    {
        if (ShowActionMenu) DrawActionMenu();
        if (ShowMoveLabel) DrawMoveLabel();
    }

    public void DrawActionMenu()
    {
        if (GUI.Button(new Rect(0, 0, 50, 25), "Move"))
        {
            Debug.Log(Actor.ToString() + " is moving...");
            ShowMoveLabel = true;
            ShowActionMenu = false;
            RangeIndicator.SetActive(true);
        }
    }

    public void DrawMoveLabel()
    {
        GUI.Label(new Rect(0, 0, 200, 25), "Select move target");
    }

    void Update()
    {
        // No one is now acting
        if (Actor == null)
        {
            // Call next actor.
            ActorIndex++;

            // If the index above the limit
            if (ActorIndex > Characters.Count - 1)
            {
                // Return to the first one
                ActorIndex = 0;
            }

            // Define Actor.
            DefineActor();

            // Focus the actor.
            CameraController.Instance.Focus(Actor.transform.position);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            OrderCharactersByLuck();
        }

        if (ShowMoveLabel)
        {
            // Show range.
            RaycastHit hit;

            if (CastRay(Input.mousePosition, out hit))
            {
                RangeIndicator.transform.position = hit.point;

                if ((RangeIndicator.transform.position - Actor.transform.position).sqrMagnitude < 10 * 10)
                {
                    RangeIndicator.GetComponent<Renderer>().material.color = Color.green;

                    // If click and is selecting some target
                    if (Input.GetMouseButtonUp(0))
                    {
                        Debug.Log("Move " + Actor.ToString() + " to " + hit.point);
                        Actor.transform.position = hit.point + Vector3.up;
                        RangeIndicator.SetActive(false);
                        Debug.Log(Actor.ToString() + " round finished... calling next one.");
                        Actor = null;
                        ShowMoveLabel = false;
                    }
                }
                else
                {
                    RangeIndicator.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }


    }

    private bool CastRay(Vector3 selectionPosition, out RaycastHit hit, float distance = 100f)
    {
        return Physics.Raycast(Camera.main.ScreenPointToRay(selectionPosition), out hit, distance);
    }
}
