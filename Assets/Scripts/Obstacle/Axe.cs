using UnityEngine;
using DG.Tweening;

public class Axe : Obstacles
{
    Transform model;
    float duration=2;
    float angle = 360;

    void Start()
    {
        model = transform.GetChild(0);
        RotateAndMove();
    }
    void RotateAndMove()
    {
        if (transform.localPosition.x < 0)
        {
            angle = -360;
            model.localEulerAngles = new Vector3(0,180,0) ;// reverse the model 
        }
        else
        {
            angle = 360;
            model.localEulerAngles = Vector3.zero;
        }
        model.DORotate(Vector3.forward * angle, duration, RotateMode.WorldAxisAdd).SetEase(Ease.Linear);
        transform.DOLocalMoveX(-transform.localPosition.x, duration).SetEase(Ease.Linear).OnComplete(RotateAndMove);
    }


}
