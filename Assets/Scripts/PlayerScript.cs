using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0, 5)]
    public int healthPoints = 5;
    [Range(0, 10)]
    public int abilityPoints = 10;
    public int scorePoints = 0;
    public bool invincibility = false;

    public float movmentStep;
    public MeshCollider planeMesh;
    public AudioSource audioSource;
    public AudioClip obstacleClip;
    public AudioClip PowerUpHealthClip;
    public AudioClip PowerUpAbilityClip;
    private Vector3 xBounds;
    private float yDefault;
    void Start()
    {
        Bounds planeBounds = planeMesh.bounds;
        xBounds = new Vector2(planeBounds.center.x + planeBounds.extents.x, planeBounds.center.x - planeBounds.extents.x);
        yDefault = transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Right();
        }
        if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Left();
        }
        if (Input.GetKeyDown("q"))
        {
            Ability();
        }
        if (Input.GetKeyDown("g"))
            AddHealth();
        if (Input.GetKeyDown("h"))
            healthPoints--;
        if (Input.GetKeyDown("j"))
            AddAbility();
        if (Input.GetKeyDown("k"))
            if (abilityPoints > 0)
                abilityPoints--;
        if (Input.GetKeyDown("l"))
            invincibility = !invincibility;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    bool CheckOutOfBounds(Vector3 newPos)
    {
        if (newPos.x <= xBounds.x && newPos.x >= xBounds.y)
            return true;
        return false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (!invincibility)
            {
                healthPoints -= other.GetComponent<ObstacleScript>().damage;
            }
            //Destroy(other.gameObject);
            audioSource.PlayOneShot(obstacleClip, 1);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("PowerUp"))
        {
            /*
            if (other.name.Equals("Health PowerUp Variant(Clone)"))
                AddHealth();
            if (other.name.Equals("Ability PowerUp Variant(Clone)"))
                AddAbility();
                */
            //Destroy(other.gameObject);
            if (other.GetComponent<PowerUpScript>().isHealth)
            {
                AddHealth();
                audioSource.PlayOneShot(PowerUpHealthClip, 1);
            }
            else
            {
                AddAbility();
                audioSource.PlayOneShot(PowerUpAbilityClip, 1);
            }

            other.gameObject.SetActive(false);
        }

    }
    public void AddHealth()
    {
        if (healthPoints < 5)
            healthPoints++;
    }
    public void AddAbility()
    {
        if (abilityPoints < 10)
            abilityPoints++;
    }
    public void DestroyAll(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject gameObject in gameObjects)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void Jump()
    {
        if (transform.position.y <= yDefault + 0.1)
        {

            if (abilityPoints >= 1)
            {
                Vector3 newele = transform.position + Vector3.up * movmentStep;
                transform.position = newele;
                abilityPoints--;
            }
        }
    }

    public void Ability()
    {
        if (abilityPoints >= 5)
        {
            DestroyAll("Obstacle");
            abilityPoints -= 5;
        }
    }

    public void Left()
    {
        Vector3 newPos = transform.position + Vector3.left * movmentStep;
        if (CheckOutOfBounds(newPos))
            transform.position = newPos;

    }

    public void Right()
    {
        Vector3 newPos = transform.position + Vector3.right * movmentStep;
        if (CheckOutOfBounds(newPos))
            transform.position = newPos;

    }
}