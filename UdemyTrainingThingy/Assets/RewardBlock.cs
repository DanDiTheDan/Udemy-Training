using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBlock : MonoBehaviour
{
    float timer = 5;

    public int lootAmount;

    bool despawning = false;

    public Animator anim;
    public GameObject particles;
    PlayerController playerController;
    public GameObject player;

    void Start()
    {
        if (player != null)
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        if (player == null || playerController == null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
        if(timer <= 2 && despawning == false)
        {
            anim.SetBool("despawning", true);
            despawning = true;
        }
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player != null && playerController != null)
            {
                Instantiate(particles, transform.position, Quaternion.identity);
                Debug.Log("Picked up " + lootAmount);
                playerController.IncreaseScore(lootAmount);
                Destroy(gameObject);
            }
        }
    }
}
