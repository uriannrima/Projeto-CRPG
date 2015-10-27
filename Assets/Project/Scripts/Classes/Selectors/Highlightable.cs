using UnityEngine;
using System.Collections;

public class Highlightable : BaseMonoBehavior
{
    private Color OriginalColor;
    public Color HighlightColor = new Color32(193, 193, 255, 26);

    void Start()
    {
        OriginalColor = GetComponent<Renderer>().material.color;
    }

    public void Highlight()
    {
        // Enable the selection mark.
        GetComponent<Renderer>().material.color = HighlightColor;
    }

    public void Dehighlight()
    {
        // Disable the selection mark.
        GetComponent<Renderer>().material.color = OriginalColor;
    }
}
