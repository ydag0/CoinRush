using System.Collections.Generic;
using UnityEngine;
public static class Extensions 
{
    public static void PrintBlue(this string s )
    {
        Debug.Log("<color=blue>" + s + "</color>");
    }
    public static Vector3 GetRandomPointInBox(this BoxCollider collider)
    {
        float x = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float y = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
        float z = Random.Range(collider.bounds.min.z, collider.bounds.max.z);
        return new Vector3(x, y, z);
    }
    public static T GetRandomItem<T>(this List<T> list)
    {
        if(list.Count == 0)
            throw new System.IndexOutOfRangeException("List is Empty");
        return list[Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Returns last item in the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T GetLastItem<T>(this List<T> list)
    {
        if (list.Count == 0)
            throw new System.IndexOutOfRangeException("List is Empty");
        return list[list.Count - 1];
    }
    /// <summary>
    /// Sets transforms local position.
    /// </summary>
    /// <param name="">Transform to reposition</param>
    /// <param name="x">Default: 0</param>
    /// <param name="y">Default: 0</param>
    /// <param name="y">Default: 0</param>
    /// <returns></returns>
    public static void SetLocal(this Transform t ,float x=0, float y=0,float z=0)
    {
        t.localPosition = new Vector3(x, y, z);
    }
}
