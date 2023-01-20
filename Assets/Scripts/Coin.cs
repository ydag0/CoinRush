using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Coin : MonoBehaviour
{
    #region variables
    Rigidbody myRb;
    Transform childModelTransform;
    float speed; //100
    float rotateSpeed;//70
     float coinModelRotateSpeed;//180
    float _speed;
    float _rotateSpeed;
    float _coinModelRotateSpeed;
    Vector3 startPos;
    Quaternion startRotation;


    float rotationBoundry = 45;
    float fallingForce=2;
    bool gameOver;
    bool checkedGrounded;
    #endregion
    private void Awake()
    {
        _speed = Settings.Instance.setting.speed;
        _rotateSpeed = Settings.Instance.setting.rotateSpeed;
        _coinModelRotateSpeed = Settings.Instance.setting.coinXRotateSpeed;
    }
    void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
        //Dont start;
        speed = 0;
        rotateSpeed = 0;
        coinModelRotateSpeed = 0;
        //
        myRb = GetComponent<Rigidbody>();
        childModelTransform = transform.GetChild(0);

    }
    #region Observer
    private void OnEnable()
    {
        Settings.gameOver += GameEnd;
    }
    private void OnDisable()
    {
        Settings.gameOver -= GameEnd;
    }
#endregion

    #region Collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Tags.obstacleTag))
        {
            Settings.gameOver?.Invoke();
            gameOver = true;
        }
        else if (collision.collider.CompareTag(Tags.gameWinTag))
        {
            gameOver = true;
            Settings.gameEnd?.Invoke();
            Animations.Instance.GameEndAnim(collision.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.collectableTag))
        {
            other.enabled = false;
            CollectManager.Instance.CollectCoin(other.transform);
        }

    }
#endregion
    private void FixedUpdate()
    {
        Rotate();
        RotateChild();

        if(!gameOver)
            myRb.velocity = transform.forward * (speed * Time.deltaTime);
        if (transform.position.y < 0 && !checkedGrounded)
            StartCoroutine(CheckForOnGround());
    }
    void Rotate()
    {
        float mouseX = InputManager.Instance.mouseX;
        if (mouseX == 0 )
            return;
        if (!CanRotate() || gameOver)
            return;
        transform.Rotate(Vector3.up * (mouseX * rotateSpeed * Time.fixedDeltaTime));
    }
    bool CanRotate()
    {
        float mouseX = InputManager.Instance.mouseX;
        if (mouseX > 0.0f)
        {
            if (transform.localEulerAngles.y >= rotationBoundry && transform.localEulerAngles.y < rotationBoundry * 2)
                return false;
        }
        else
        {
            if (transform.localEulerAngles.y <= 360-rotationBoundry && transform.localEulerAngles.y >= rotationBoundry*2)
                return false;
        }
        
        return true;
    }
    void RotateChild()
    {
        
        if (gameOver)
            return;
        childModelTransform.Rotate(childModelTransform.forward, Time.fixedDeltaTime * coinModelRotateSpeed, Space.World);

    }

    void GameEnd()
    {
        gameOver = true;
        AddSideForce();
        CollectManager.Instance.DropCoins();
    }
    void AddSideForce(int x=0)
    {
        myRb.velocity *= .3f;
        myRb.constraints = RigidbodyConstraints.None;
        if (x == 0)
        {
            x = Random.Range(0, 2);
            x = x == 0 ? 1 : -1;
        }

        Vector3 force = transform.right *(x * fallingForce); 
        force.y = Random.Range(0, .25f);
        myRb.AddForce(force, ForceMode.VelocityChange);
    }
    public IEnumerator AddSideForceGameWin(float time )
    {
        yield return new WaitForSeconds(time);
        AddSideForce(1);
    }

    public void StartCoin()
    {
        speed = _speed;
        rotateSpeed = _rotateSpeed;
        coinModelRotateSpeed = _coinModelRotateSpeed;
    }
    IEnumerator CheckForOnGround()
    {
        checkedGrounded = true;
        yield return new WaitForSeconds(1.0f);
        if (transform.position.y < 0)
        {
            Settings.gameOver?.Invoke();
            gameOver = true;
        }
        else
            checkedGrounded = false;

    }

    public void NewGame()
    {
        myRb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.rotation = startRotation;
        transform.position = startPos;
        gameOver = false;
        checkedGrounded = false;
    }
}

