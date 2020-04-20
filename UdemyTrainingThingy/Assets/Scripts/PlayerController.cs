using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;
    public float speed;
    int highscore;
    public int score = 0;
    float input;
    Rigidbody2D rb;
    Animator anim;

    public bool WallDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    void Update()
    {
        if (input != 0 && WallDetected == false)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
        if (input > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (input < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetHighscore(int score)
    {
        highscore = score;
    }

    public void IncreaseScore(int scoreAmount)
    {
        score += scoreAmount;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "WallDetect")
        {
            WallDetected = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "WallDetect")
        {
            WallDetected = false;
        }
    }
}
