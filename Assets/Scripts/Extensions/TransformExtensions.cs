using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void ClearChildren(this Transform transform)
    {
        int count = transform.childCount;
        while (count > 0)
        {
            GameObject.DestroyImmediate(transform.GetChild(count - 1).gameObject);
            count--;
        }
    }

    public static List<T> GetChildren<T>(this Transform transform)
    {
        List<T> children = new List<T>();
        foreach (Transform child in transform)
        {
            children.Add(child.GetComponent<T>());
        }
        return children;
    }
}