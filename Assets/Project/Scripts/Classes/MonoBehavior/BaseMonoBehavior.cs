using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BaseMonoBehavior : MonoBehaviour
{
    /// <summary>
    /// Components cache.
    /// </summary>
    Dictionary<Type, Component> Cache = new Dictionary<Type, Component>();

    /// <summary>
    /// Components cache from children.
    /// </summary>
    Dictionary<Type, Component> ChildrenCache = new Dictionary<Type, Component>();

    /// <summary>
    /// Components cache from parent.
    /// </summary>
    Dictionary<Type, Component> ParentCache = new Dictionary<Type, Component>();

    /// <summary>
    /// Get and create a cache from the Component present in the game object.
    /// </summary>
    /// <typeparam name="T">Type of component.</typeparam>
    /// <returns>Returns the component if exists in the dictionary, or in the game object.</returns>
    protected T GetCached<T>() where T : Component
    {
        // Get type of T
        var type = typeof(T);

        // Cache contains some component type of T, and is T.
        if (Cache.ContainsKey(type) && Cache[type] is T)
        {
            // Return component as T.
            return (T)Cache[type];
        }
        else
        {
            // If it doesn't have, check if the Game Object contains this component.
            var component = GetComponent<T>();

            // If it do
            if (component)
            {
                // Save the component to the Cache.
                Cache.Add(type, component);

                // Return it.
                return component;
            }
        }

        // If nothing was found, return default of T, maybe is a null.
        return default(T);
    }

    protected T GetCachedInChildren<T>() where T : Component
    {
        // Get type of T
        var type = typeof(T);

        // Cache contains some component type of T, and is T.
        if (ChildrenCache.ContainsKey(type) && ChildrenCache[type] is T)
        {
            // Return component as T.
            return (T)ChildrenCache[type];
        }
        else
        {
            // If it doesn't have, check if the Game Object contains this component.
            var component = GetComponentInChildren<T>();

            // If it do
            if (component)
            {
                // Save the component to the Cache.
                ChildrenCache.Add(type, component);

                // Return it.
                return component;
            }
        }

        // If nothing was found, return default of T, maybe is a null.
        return default(T);
    }

    protected T GetCachedInParent<T>() where T : Component
    {
        // Get type of T
        var type = typeof(T);

        // Cache contains some component type of T, and is T.
        if (ParentCache.ContainsKey(type) && ParentCache[type] is T)
        {
            // Return component as T.
            return (T)ParentCache[type];
        }
        else
        {
            // If it doesn't have, check if the Game Object contains this component.
            var component = GetComponentInParent<T>();

            // If it do
            if (component)
            {
                // Save the component to the Cache.
                ParentCache.Add(type, component);

                // Return it.
                return component;
            }
        }

        // If nothing was found, return default of T, maybe is a null.
        return default(T);
    }

    /// <summary>
    /// Find all children inside this Game Object.
    /// </summary>
    /// <returns>List with wall children of the Game Object.</returns>
    public List<GameObject> GetChildren()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }

    /// <summary>
    /// Find all children inside this Game Object using Child Name as filter.
    /// </summary>
    /// <param name="childName">Name to be used as filter.</param>
    /// <returns>All children which has childName as name.</returns>
    public List<GameObject> GetChildren(string childName)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.name == childName) children.Add(child.gameObject);
        }
        return children;
    }

    /// <summary>
    /// Return first appearance of Child Game Object with Child Name.
    /// </summary>
    /// <param name="childName">Name to be used as filter.</param>
    /// <returns>First child that has childName as name.</returns>
    public GameObject GetChild(string childName)
    {
        foreach (Transform child in transform)
        {
            if (child.name == childName) return child.gameObject;
        }

        return null;
    }

    /// <summary>
    /// Find Component which implements interface I.
    /// </summary>
    /// <typeparam name="I">Type of Interface.</typeparam>
    /// <returns>Component as I.</returns>
    public I GetInterfaceComponent<I>() where I : class
    {
        return GetComponent(typeof(I)) as I;
    }

    /// <summary>
    /// Try to find gameObject of type T.
    /// </summary>
    /// <typeparam name="T">Type of Game Object.</typeparam>
    /// <param name="result">Variable assigned with gameObject found.</param>
    /// <returns>True if found. False if didn't found.</returns>
    public bool TryFindObjectOfType<T>(out T result) where T : UnityEngine.Object
    {
        result = FindObjectOfType<T>();

        if (result != null)
        {
            return true;
        }

        return false;
    }
}
/// <summary>
/// Extension class to the Game Object Class.
/// </summary>
public static class GameObjectExt
{
    /// <summary>
    /// Return first appearance of Child Game Object with Child Name.
    /// </summary>
    /// <param name="childName">Name to be used as filter.</param>
    /// <returns>First child that has childName as name.</returns>
    public static GameObject GetChild(this GameObject gameObject, string childName)
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.name == childName) return child.gameObject;
        }

        return null;
    }

    /// <summary>
    /// Find all children inside this Game Object using Child Name as filter.
    /// </summary>
    /// <param name="childName">Name to be used as filter.</param>
    /// <returns>All children which has childName as name.</returns>
    public static List<GameObject> GetChildren(this GameObject gameObject, string childName)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in gameObject.transform)
        {
            if (child.name == childName) children.Add(child.gameObject);
        }
        return children;
    }

    /// <summary>
    /// Find all children inside this Game Object.
    /// </summary>
    /// <returns>List with wall children of the Game Object.</returns>
    public static List<GameObject> GetChildren(this GameObject gameObject)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in gameObject.transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }

    /// <summary>
    /// Find Component which implements interface I.
    /// </summary>
    /// <typeparam name="I">Type of Interface.</typeparam>
    /// <returns>Component as I.</returns>
    public static I GetInterfaceComponent<I>(this GameObject gameObject) where I : class
    {
        return gameObject.GetComponent(typeof(I)) as I;
    }

    /// <summary>
    /// Find Component which implements interface I on this gameObject parent.
    /// </summary>
    /// <typeparam name="I">Type of Interface</typeparam>
    /// <returns>Component as I.</returns>
    public static I GetInterfaceComponentInParent<I>(this GameObject gameObject) where I : class
    {
        return gameObject.GetComponentInParent(typeof(I)) as I;
    }

    /// <summary>
    /// Find Component which implements interface I on this gameObject children.
    /// </summary>
    /// <typeparam name="I">Type of Interface</typeparam>
    /// <returns>Component as I.</returns>
    public static I GetInterfaceComponentInChildren<I>(this GameObject gameObject) where I : class
    {
        return gameObject.GetComponentInChildren(typeof(I)) as I;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="tags"></param>
    /// <param name="exclusive"></param>
    /// <returns></returns>
    public static bool CompareTags(this GameObject gameObject, string[] tags, bool exclusive = false)
    {
        bool result = false;
        foreach (var tag in tags)
        {
            if (exclusive)
            {
                result &= gameObject.CompareTag(tag);
            }
            else
            {
                result |= gameObject.CompareTag(tag);
            }            
        }

        return result;
    }
}

/// <summary>
/// Extension class to the NavMesh Path Class.
/// </summary>
public static class NavMeshPathExt
{
    /// <summary>
    /// Get length of the NavMesh Path.
    /// </summary>
    /// <param name="NavMeshPath"></param>
    /// <returns></returns>
    public static float PathLength(this NavMeshPath NavMeshPath)
    {
        float length = 0;

        Vector3 previousCorner = NavMeshPath.corners[0];

        for (int i = 1; i < NavMeshPath.corners.Length; i++)
        {
            length += Vector3.Distance(previousCorner, NavMeshPath.corners[i]);
            previousCorner = NavMeshPath.corners[i];
        }

        return length;
    }
}

/// <summary>
/// Extension class to the NavMesh Agent Class.
/// </summary>
public static class NavMeshAgentExt
{
    /// <summary>
    /// Check if the NavMesh Agent is near it's destination.
    /// </summary>
    /// <param name="NavMeshAgent"></param>
    /// <returns></returns>
    public static bool IsNearDestination(this NavMeshAgent NavMeshAgent)
    {
        return NavMeshAgent.remainingDistance < NavMeshAgent.stoppingDistance;
    }
}