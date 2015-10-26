using UnityEngine;
using System.Collections;

/// <summary>
/// Basic singleton behavior, should be used to single objets through out the game,
/// which reference could be used in another script.
/// </summary>
/// <typeparam name="T">Type of Istance</typeparam>
public class BaseSingleton<T> : BaseMonoBehavior where T : BaseSingleton<T>
{
    /// <summary>
    /// Private static reference to the instance.
    /// </summary>
    private static T _instance;

    /// <summary>
    /// Public reference to the instance.
    /// If an reference doesn't exist, we try to find object of type T in the game.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null) _instance = GameObject.FindObjectOfType<T>();
            return _instance;
        }
    }
}
