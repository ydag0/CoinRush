using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    Transform cameraTransform;
    Vector3 offset;
    Vector3 targetPos;
    bool smoothTrack;//false
    [Tooltip("Higher Value Make Slower Follow")]
    float smoothTime;//0
    Vector3 velocity;
    void Start()
    {
        smoothTrack = Settings.Instance.setting.smoothFollow;
        smoothTime = Settings.Instance.setting.trackSpeed;
        cameraTransform = Camera.main.transform;
        if (targetTransform == null)
            targetTransform = CollectManager.Instance.baseCoin.transform;
        offset = cameraTransform.position - targetTransform.position;
        offset.y = cameraTransform.position.y - targetTransform.position.y;
        targetPos = targetTransform.position + offset;
        
    }

    void LateUpdate()
    {
        if (targetTransform.position.y < 0)
            return;
        targetPos = targetTransform.position + offset;
        if (smoothTrack)
            cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPos, ref velocity, smoothTime * Time.deltaTime);
        else
            cameraTransform.position = targetPos;
    }
}
