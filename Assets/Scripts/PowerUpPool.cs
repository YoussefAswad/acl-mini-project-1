using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : MonoBehaviour
{
    // Start is called before the first frame update
    public static PowerUpPool SharedInstance;
    public List<GameObject> powerUpsPool;
    public GameObject powerUp;
    public int amountToPool;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        powerUpsPool = new List<GameObject> { };
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(powerUp);
            tmp.SetActive(false);
            powerUpsPool.Add(tmp);

        }
    }

    // Update is called once per frame

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!powerUpsPool[i].activeInHierarchy)
            {
                return powerUpsPool[i];
            }
        }
        return null;
    }
}
