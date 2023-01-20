using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] RectTransform handTransform;
    [SerializeField] float xPosition;
    [SerializeField] float speed;

    [SerializeField] RectTransform gameEndCanvas;

    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text gameEndText;
    #region localvariables
    bool started;
    bool killed;
    float coinWorth = 0.25f;
    float _mouseSensivity = .2f;
    float delay = 2.5f;
#endregion
    #region observer 
    private void OnEnable()
    {
        Settings.gameEnd += GameWinText;
        Settings.gameOver += GameFailText;

    }
    private void OnDisable()
    {
        Settings.gameEnd -= GameWinText;
        Settings.gameOver -= GameFailText;
    }
    #endregion
    void Start() => SwipeAnim();
    
    void Update()
    {
        if (started && !killed)
        {
            KillSwipe();
            StartCoin();
        }
        if (!started)
            CheckForStarted();
    }
    void CheckForStarted()
    {
        if (Mathf.Abs(InputManager.Instance.mouseX) > _mouseSensivity)
            started = true;
    }
    void SwipeAnim()
    {
        handTransform.DOAnchorPosX(-xPosition, speed).SetEase(Ease.Linear).OnComplete(() =>
             handTransform.DOAnchorPosX(xPosition, speed).SetEase(Ease.Linear).OnComplete(SwipeAnim));
    }
    void KillSwipe()
    {
        killed = true;
        handTransform?.DOKill();
        handTransform.parent.gameObject.SetActive(false);
    }
    public void UpdateCoinText()
    {
        float number = CollectManager.Instance.collectedCoins.Count * coinWorth;
        coinText.text = $"${number}";
    }

    void OpenGameEndUI()=> gameEndCanvas.gameObject.SetActive(true);
    void StartCoin()=> CollectManager.Instance.baseCoin.GetComponent<Coin>().StartCoin();
     void GameWinText()=> StartCoroutine(GameWin());
     void GameFailText() => StartCoroutine(GameFail());
     IEnumerator GameWin()
     {

        yield return new WaitForSeconds(delay);
        float coins = CollectManager.Instance.collectedCoins.Count * coinWorth;
        gameEndText.text = $"Coins:{coins}";
        OpenGameEndUI();
     }
    IEnumerator GameFail()
    {
        yield return new WaitForSeconds(delay);
        gameEndText.text = "Failed";
        OpenGameEndUI();
    }
    public void NewGame()
    {
        
        LevelManager.Instance.NewGame();
        CollectManager.Instance.ResetCollectedCoins();
        CollectManager.Instance.baseCoin.GetComponent<Coin>().NewGame();
        UpdateCoinText();
    }


}
