using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CollectManager : Singleton<CollectManager>
{
    [SerializeField] Transform coinsParent;
    public GameObject baseCoin;
    public List<Transform> collectedCoins = new List<Transform>();

    float localDistance = .24f;

    void Start()
    {
        if (coinsParent == null)
        {
            "Restart and Select Coins Transform Parent".PrintBlue();
            coinsParent = baseCoin.transform;
        }
    }
    public void CollectCoin(Transform coin)
    {
        collectedCoins.Add(coin);
        coin.transform.DOMove(baseCoin.transform.position, .1f).OnComplete(SetCoin);
        UIManager.Instance.UpdateCoinText();
    }
    public void DropCoins()
    {
        if (collectedCoins.Count == 0)
            return;
 
        collectedCoins.ForEach(x => DropEveryCoin(x));
    }
    void DropEveryCoin(Transform coin)
    {
        coin.transform.SetParent(null);
        coin.GetComponent<CollectableCoin>().AddVelocityFail(Vector3.zero);

    }
    void SetCoin()
    {
        var coin = collectedCoins.GetLastItem();
        coin.transform.SetParent(coinsParent);
        coin.GetComponent<CollectableCoin>().collected = true;
        coin.transform.localEulerAngles = Vector3.zero;
        Vector3 localPos = Vector3.back * (localDistance * (collectedCoins.Count - 1) );
        coin.transform.localPosition = localPos;
        coin.transform.GetChild(1).gameObject.SetActive(true);
        Animations.Instance.BounceEffect();
    }
    public void ResetCollectedCoins() => collectedCoins = new List<Transform>();
}
