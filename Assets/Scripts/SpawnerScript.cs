using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0, 100)]
    public float powerUpPercentage;
    public GameObject obsaclePrefab;
    public GameObject powerUpPrefab;
    public Material[] materials;
    //public GameObject[] obstacles;

    public GameObject healthPowerUp;
    public GameObject abilityPowerUp;
    public MeshCollider planeMesh;
    private float[] obstacleX;
    //private Vector3 zBounds;
    private float laneWidth;
    public float timer;
    private float currentTimer;
    private Vector3 defaultObstacleScale;

    void Start()
    {
        Bounds planeBounds = planeMesh.bounds;
        float[] xBounds = { planeBounds.center.x + planeBounds.extents.x, planeBounds.center.x - planeBounds.extents.x };
        //zBounds = new Vector2(planeBounds.center.z + planeBounds.extents.z, planeBounds.center.z - planeBounds.extents.z);
        float planeWidth = Mathf.Abs(xBounds[0]) + Mathf.Abs(xBounds[1]);
        laneWidth = planeWidth / 3;

        obstacleX = new float[] { transform.position.x - laneWidth, 0f, transform.position.x + laneWidth }; ;
        defaultObstacleScale = obsaclePrefab.transform.localScale;
        currentTimer = 0;
    }


    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;

        if (currentTimer <= 0)
        {
            if (Random.value > powerUpPercentage / 100f)
            {
                float obstacleType = Random.value;
                float obstacleZ = transform.position.z;
                if (obstacleType > 0.6f)
                {
                    float obstacleY = transform.position.y + obsaclePrefab.transform.localScale.y / 2;
                    //Instantiate(obstacles[0], new Vector3(obstacleX[Random.Range(0, 3)], obstacleY, obstacleZ), obstacles[0].transform.rotation);
                    GameObject obstacle = ObstaclePool.SharedInstance.GetPooledObject();
                    if (obstacle != null)
                    {
                        obstacle.transform.position = new Vector3(obstacleX[Random.Range(0, 3)], obstacleY, obstacleZ);
                        obstacle.transform.rotation = transform.rotation;
                        obstacle.transform.localScale = new Vector3(defaultObstacleScale.x, obstacle.transform.localScale.y, obstacle.transform.localScale.z);
                        obstacle.GetComponent<MeshRenderer>().material = materials[0];
                        obstacle.GetComponent<ObstacleScript>().damage = 3;
                        obstacle.GetComponentInChildren<JumpColliderScript>().score = 3;
                        obstacle.SetActive(true);
                        Debug.Log("Init");
                    }
                }
                else if (obstacleType > 0.2f)
                {
                    float obstacleY = transform.position.y + obsaclePrefab.transform.localScale.y / 2;
                    //Instantiate(obstacles[1], new Vector3(obstacleX[Random.Range(0, 2)] + (int)laneWidth / 2, obstacleY, obstacleZ), obstacles[1].transform.rotation);
                    GameObject obstacle = ObstaclePool.SharedInstance.GetPooledObject();
                    if (obstacle != null)
                    {
                        obstacle.transform.position = new Vector3(obstacleX[Random.Range(0, 2)] + (int)laneWidth / 2, obstacleY, obstacleZ);
                        obstacle.transform.rotation = transform.rotation;
                        obstacle.transform.localScale = new Vector3(2 * defaultObstacleScale.x, obstacle.transform.localScale.y, obstacle.transform.localScale.z);
                        obstacle.GetComponent<MeshRenderer>().material = materials[1];
                        obstacle.GetComponent<ObstacleScript>().damage = 2;
                        obstacle.GetComponentInChildren<JumpColliderScript>().score = 2;
                        obstacle.SetActive(true);
                    }
                }
                else
                {
                    float obstacleY = transform.position.y + obsaclePrefab.transform.localScale.y / 2;
                    //Instantiate(obstacles[2], new Vector3(obstacleX[1], obstacleY, obstacleZ), obstacles[2].transform.rotation);
                    GameObject obstacle = ObstaclePool.SharedInstance.GetPooledObject();
                    if (obstacle != null)
                    {
                        obstacle.transform.position = new Vector3(obstacleX[1], obstacleY, obstacleZ);
                        obstacle.transform.rotation = transform.rotation;
                        obstacle.transform.localScale = new Vector3(3 * defaultObstacleScale.x, obstacle.transform.localScale.y, obstacle.transform.localScale.z);
                        obstacle.GetComponent<MeshRenderer>().material = materials[2];
                        obstacle.GetComponent<ObstacleScript>().damage = 1;
                        obstacle.GetComponentInChildren<JumpColliderScript>().score = 1;
                        obstacle.SetActive(true);
                    }
                }
                //var position = new Vector3(transform.position.x + laneWidth, transform.position.y + obstacle1W.transform.localScale.y / 2, transform.position.z);
                //Instantiate(obstacle1W, position, obstacle1W.transform.rotation);

            }
            else
            {
                bool lane1 = RandomBoolean();
                bool lane2 = RandomBoolean();
                bool lane3 = RandomBoolean();

                if (lane1)
                    SpawnPowerUp(0);
                if (lane2)
                    SpawnPowerUp(1);
                if (lane3)
                    SpawnPowerUp(2);
            }
            currentTimer = timer;
        }

    }
    bool RandomBoolean()
    {
        return (Random.value > 0.5f);
    }

    void SpawnPowerUp(int laneNum)
    {
        //GameObject choosenPowerUp = healthPowerUp;
        //if (RandomBoolean())
        //choosenPowerUp = abilityPowerUp;
        //Instantiate(choosenPowerUp, new Vector3(obstacleX[laneNum], choosenPowerUp.transform.position.y, transform.position.z), choosenPowerUp.transform.rotation);
        GameObject powerUp = PowerUpPool.SharedInstance.GetPooledObject();
        if (powerUp != null)
        {
            powerUp.transform.position = new Vector3(obstacleX[laneNum], powerUpPrefab.transform.position.y, transform.position.z);
            powerUp.transform.rotation = transform.rotation;
            powerUp.GetComponent<PowerUpScript>().isHealth = RandomBoolean();
            if (powerUp.GetComponent<PowerUpScript>().isHealth)
                powerUp.GetComponent<MeshRenderer>().material = materials[3];
            else
                powerUp.GetComponent<MeshRenderer>().material = materials[4];
            powerUp.SetActive(true);
        }
    }

}
