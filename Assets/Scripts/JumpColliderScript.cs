using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject player;
    public int score;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.position.y > 1.1)
        {
            other.GetComponent<PlayerScript>().scorePoints += score;
        }
    }
}
