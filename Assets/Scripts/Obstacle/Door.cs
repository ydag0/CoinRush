using UnityEngine;
using DG.Tweening;
public class Door : Obstacles
{
    float turningDuration=4;
    float angle = 120;
    [SerializeField] float rangeToStartOpening = 10;//5
    bool opened;

    void Update()
    {
        if (!opened)
        {            
            Vector3 dir = CollectManager.Instance.baseCoin.transform.position - transform.position;
            if(dir.magnitude < rangeToStartOpening)
            {
                opened = true;
                OpenDoors();
            }
        }
    }
    void OpenDoors()
    { 
        var rightDoor = transform.GetChild(0);
        var leftDoor = transform.GetChild(1);
        rightDoor.DORotate(new Vector3(rightDoor.localEulerAngles.x, 0, -angle), turningDuration).SetEase(Ease.Linear);
        leftDoor.DORotate(new Vector3(rightDoor.localEulerAngles.x, 0, angle), turningDuration).SetEase(Ease.Linear);
    }

}
