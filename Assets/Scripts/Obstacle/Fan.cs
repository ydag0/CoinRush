using UnityEngine;
using DG.Tweening;

public class Fan : Obstacles
{
    float rotateDuration=5;
    float angle = 360;
    void Start()
    {
        Rotate();
    }
    void Rotate()
    {
        transform.DORotate(Vector3.up * angle, rotateDuration, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).OnComplete(Rotate);
    }

}
