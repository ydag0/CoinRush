using System.Collections;
using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    public float velocitySpeed;
    public bool collected;
    public bool dropped;
    float rotateSpeed;
    Vector3 vel;
    Transform modelTransform;
    Rigidbody myRb;
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        modelTransform = transform.GetChild(0);
        rotateSpeed = Settings.Instance.setting.coinXRotateSpeed * 1.3f;
    }

    void Update()
    {
        if (collected && !dropped)//If Collected and not dropped
        { modelTransform.Rotate(transform.right, rotateSpeed * Time.deltaTime, Space.World); }
        if (!collected && !dropped)
        { transform.Rotate(transform.up, rotateSpeed * Time.deltaTime, Space.World); }
    }
    public void AddVelocityFail(Vector3 velocity)
    {
        collected = false;
        dropped = true;
        vel = /*Adding Noise*/new Vector3(Random.Range(-0.5f,0.5f), Random.Range(0.35f, .8f), Random.Range(0.2f,1.1f)) ;
        AddVelocity(velocitySpeed * vel);
    }
    void AddVelocity(Vector3 velocity)
    {
        myRb.isKinematic = false;

        myRb.velocity = velocity;
        myRb.useGravity = true;

    }

    /// <param name="leftOrRight">1 for right -1 for left </param>
    public IEnumerator AddVelocityWin(float waitingTime, int leftOrRight)
    {
        collected = false;
        dropped = true;
        yield return new WaitForSeconds(waitingTime);
        //myRb.useGravity = false;
        int r = Random.Range(0, 2);
        r = r == 1 ? 1 : -1;
        AddVelocity(new Vector3(leftOrRight*.7f,0,0 ));
        
    }

}
