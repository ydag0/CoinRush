using UnityEngine;
public enum parts
{
    Start,
    End,
    Part
}
[System.Serializable]
public struct LevelParts
{
    public GameObject level;
    public parts LevelPart;
    [Tooltip("Z position size of the level")]
    public float size;
}

