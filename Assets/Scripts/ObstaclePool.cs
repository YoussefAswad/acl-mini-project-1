using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    // Start is called before the first frame update
    public static ObstaclePool SharedInstance;
    public List<GameObject> obstaclePool;
    public GameObject obstacle;
    public int amountToPool;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        obstaclePool = new List<GameObject> { };
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(obstacle);
            tmp.SetActive(false);
            obstaclePool.Add(tmp);

        }
    }

    // Update is called once per frame

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!obstaclePool[i].activeInHierarchy)
            {
                return obstaclePool[i];
            }
        }
        return null;
    }
}
