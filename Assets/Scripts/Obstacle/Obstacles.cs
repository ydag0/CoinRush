using UnityEngine;
using DG.Tweening;
public class Obstacles : MonoBehaviour
{
    protected virtual void Awake()
    {//tag control
        Collider col = GetComponentInChildren<Collider>();
        if(col != null)
        {
            if (!col.CompareTag(Tags.obstacleTag))
                col.tag = Tags.obstacleTag;
        }
    }
    protected virtual void OnDestroy()
    {
        var transforms = GetComponentsInChildren<Transform>();
        foreach(Transform t in transforms)
        {
            t?.DOKill();
        }
    }
}
