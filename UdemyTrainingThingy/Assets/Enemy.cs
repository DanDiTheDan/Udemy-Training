using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minSpeed = 1;
    public float maxSpeed = 3;

    public bool debugEnemy;
    public bool zigzagEnemy;

    float speed;
    public float timer;

    PlayerController playerScript;
    public GameObject Reward;
    public GameObject Explosion;
    public GameObject player;

    RewardBlock Loot;

    public int dmg;
    int reward;

    // Start is called before the first frame update
    void Start()
    {
        if (debugEnemy == false)
        {
            if (zigzagEnemy == true)
            {
                float randomRotation = Random.Range(-30f, 30f);
                transform.eulerAngles = new Vector3(0, 0, randomRotation);
            }
            else
            {
                float randomRotation = Random.Range(-15f, 15f);
                transform.eulerAngles = new Vector3(0, 0, randomRotation);
            }
        }
        speed = Random.Range(minSpeed, maxSpeed);
        dmg = (int)speed / 3;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        reward = (int)speed * playerScript.gameDifficulty;
        timer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (zigzagEnemy == true)
        {
            if(timer <= 0)
            {
                ChangeDirection();
                timer = 0.5f;
            }
            else
            {
                if (debugEnemy == true)
                {
                    print("Timer: " + timer);
                }
                timer -= Time.deltaTime;
            }
        }
    }
    void Explode()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void ChangeDirection()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z * -1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerScript.TakeDamage(dmg);
            Explode();
        }
        if(collision.tag == "Ground")
        {
            if (player != null)
            {
                Instantiate(Reward, transform.position, Quaternion.identity);

                Loot = Reward.GetComponent<RewardBlock>();

                Loot.lootAmount = reward;
            }

            Explode();
        }
        if (collision.tag == "NoRewardGround")
        {
            Explode();
        }
    }
}
