using UnityEngine;
using System.Collections;

public class UIManager : BaseSingleton<UIManager>
{
    public GameObject OptionMenu;

    public void ShowMenu(Vector3 worldPosition)
    {
        OptionMenu.SetActive(true);
        OptionMenu.GetComponent<RectTransform>().anchoredPosition = TransformWorldToScreen(worldPosition);
    }

    public Vector2 TransformWorldToScreen(Vector3 worldPosition)
    {
        return Camera.main.WorldToScreenPoint(worldPosition);
    }

    public bool IsMenuActive
    {
        get
        {
            return OptionMenu.activeSelf;
        }
    }
}
