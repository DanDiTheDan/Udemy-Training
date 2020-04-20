using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minSpeed = 1;
    public float maxSpeed = 3;

    float speed;

    PlayerController playerScript;
    public int dmg;

    // Start is called before the first frame update
    void Start()
    {
        float randomRotation = Random.Range(-15f, 15f);
        transform.eulerAngles = new Vector3(0, 0, randomRotation);
        speed = Random.Range(minSpeed, maxSpeed);
        dmg = (int)speed / 3;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerScript.TakeDamage(dmg);
            Destroy(gameObject);
        }
        if(collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
