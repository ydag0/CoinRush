using UnityEngine;
using DG.Tweening;

public class Animations : Singleton<Animations>
{
    [Header("Bounce Effect")]
    [SerializeField] float duration;
    [SerializeField] float yHeight;
    [SerializeField] int vibration;
    float ymines => yHeight * 0.1f;

    //[Header("Game End Animation")]
    float rotateDuration = 1.0f;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position">World Position X to go </param>
    public void GameEndAnim(Vector3 pos)//float xPosition)
    {
        Transform baseCoinT = CollectManager.Instance.baseCoin.transform;
        baseCoinT.DORotate(new Vector3(90, 0, 0), rotateDuration).SetEase(Ease.Linear).OnComplete(AddForceGameEnd);
        baseCoinT.DOMove(new Vector3(pos.x,baseCoinT.transform.position.y,pos.z), rotateDuration).SetEase(Ease.Linear);
        StartCoroutine(baseCoinT.GetComponent<Coin>().AddSideForceGameWin(2.1f));
    }
    void AddForceGameEnd()
    {
        int coinCount = CollectManager.Instance.collectedCoins.Count;
        float zPos = CollectManager.Instance.collectedCoins[0].position.z ;
        for (int i=1; i <= coinCount; i++)
        {
            var tempT = CollectManager.Instance.collectedCoins[i - 1];
            tempT.SetParent(null);//set transform parent to null
            tempT.DOMoveZ(zPos + i * 0.5f, rotateDuration).SetEase(Ease.Linear);
                
            int leftRight;
            if (i % 2 == 0)
                leftRight = 1;
            else
                leftRight = -1;

            StartCoroutine(tempT.GetComponent<CollectableCoin>().AddVelocityWin(1.05f,leftRight));
        }
    }

    public void BounceEffect()
    {

        Transform baseCoin = CollectManager.Instance.baseCoin.transform.GetChild(0);
        if (baseCoin.localPosition.y != 0)
            KillandSet(baseCoin);
        baseCoin.DOPunchPosition(new Vector3(0, yHeight, 0), duration, vibration, 0.0f);
        BounceOthers();
    }
    void BounceOthers()
    {
        for (int i=1; i < CollectManager.Instance.collectedCoins.Count + 1; i++)
        {
            float height = yHeight - i * ymines;
            height = height > 0.1f ? height : 0.1f;
            var tempTransform = CollectManager.Instance.collectedCoins[i - 1];
            if (tempTransform.localPosition.y != 0)
            {
                KillandSet(tempTransform);
            }
            tempTransform.DOPunchPosition(new Vector3(0, height, 0), duration, vibration, 0.0f)
                .SetDelay(i * duration * .5f);
        }
    }
    void KillandSet(Transform t)
    {
        t?.DOKill();
        t.SetLocal(t.localPosition.x,0, t.localPosition.z);
    }
    public void KillMe(Transform t)
    {
        t?.DOKill();
    }
    public void MoveMeThere(Transform t ,Vector3 position, float time)
    {
        t.DOMove(position, time);
    }


}
