using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Tooltip("Level Objects Transform Parent")]
    [SerializeField] Transform levelParent;
    public uint levelSize;
    [Tooltip("How many coins in the levels X for minimum Y for maksimum. It will be random")]
    public Vector2 coins;

    [SerializeField] LevelParts levelStart;
    [SerializeField] LevelParts levelEnd;
    [SerializeField] GameObject coin;
    public List<LevelParts> levelParts = new List<LevelParts>();

    List<GameObject> createdObjects;

    float startPosZ = -9.5f;

    protected override void Awake()
    {
        base.Awake();
        CreateLevel();
    }
    public void CreateLevel()
    {
        createdObjects = new List<GameObject>();
        int coinCount = Random.Range((int)coins.x, (int)coins.y);
        int coinCountForEachPart = (int)((float)coinCount / (float)(levelSize - 2));
        
        Vector3 pos = Vector3.zero;
        pos.z = startPosZ ;

        GameObject temp;
        temp=Instantiate(levelStart.level, pos , Quaternion.identity, levelParent);
        createdObjects.Add(temp);
        pos.z += levelStart.size;
        
        for(int i=0; i < levelSize - 2; i++)
        {
            LevelParts part = levelParts.GetRandomItem();
            Vector3 coinPos = part.level.GetComponentInChildren<BoxCollider>().GetRandomPointInBox();
            temp = Instantiate(part.level, pos, Quaternion.identity, levelParent);
            createdObjects.Add(temp);
            for (int j = 0; j < coinCountForEachPart; j++)
            {
                    coinPos = temp.GetComponentInChildren<BoxCollider>().GetRandomPointInBox();
                    coinPos.y = 0.2f;
                    createdObjects.Add(Instantiate(coin, coinPos, coin.transform.rotation, levelParent));
            }
            pos.z += part.size;

        }

        createdObjects.Add(Instantiate(levelEnd.level, pos, Quaternion.identity, levelParent));
    }

    public void NewGame()
    {
        createdObjects.ForEach(x => Destroy(x));
        CreateLevel();
    }
}
