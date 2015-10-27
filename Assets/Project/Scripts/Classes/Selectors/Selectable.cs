using UnityEngine;
using System.Collections;
using System;

public class Selectable : BaseMonoBehavior
{
    public GameObject SelectionMarkPrefab;
    private GameObject SelectionMark;

    public void Start()
    {
        CreateSelectionMark();
    }

    private void CreateSelectionMark()
    {
        // Instanciate
        SelectionMark = Instantiate(SelectionMarkPrefab);

        // At character and then bellow it.
        SelectionMark.transform.Translate(this.transform.position);
        SelectionMark.transform.Translate(0, -this.transform.localScale.y + 0.1f, 0);

        // Set Character as parent.
        SelectionMark.transform.parent = this.transform;

        // Disable it.
        SelectionMark.SetActive(false);
    }

    public void Select()
    {
        // Enable the selection mark.
        SelectionMark.SetActive(true);
    }

    public void Deselect()
    {
        // Disable the selection mark.
        SelectionMark.SetActive(false);
    }

    public event EventHandler Selected;

    public event EventHandler Deselected;

    private void OnSelected(EventArgs e)
    {
        if (Selected != null)
        {
            Selected(this, e);
        }
    }

    private void OnDeselected(EventArgs e)
    {
        if (Deselected != null)
        {
            Deselected(this, e);
        }
    }
}
