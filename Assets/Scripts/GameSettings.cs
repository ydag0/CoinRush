using UnityEngine;

[CreateAssetMenu(fileName ="GameSettings",menuName ="Create New Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Camera Settings")]
    [Tooltip("True: Follow Coin Smoothly")]
    public bool smoothFollow;
    [Tooltip("Smooth Follow Speed, Lower Value Faster Track")]
    public float trackSpeed;
    [Header("Coin Settings")]
    [Tooltip("Coin Move Speed ")]
    public float speed;
    [Tooltip("Coin Rotate Speed With Input")]
    public float rotateSpeed;
    [Tooltip("Coin Rotate Speed X ")]
    public float coinXRotateSpeed;
}
