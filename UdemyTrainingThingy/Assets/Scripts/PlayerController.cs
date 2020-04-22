using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject loosePanel;
    public GameObject pausePanel;

    public GameObject[] gameoverTexts;

    public Text healthDisplay;
    public Text scoreDisplay;

    public int health;
    public int maxHealth = 3;
    public float speed;
    public bool isDead;
    int highscore;
    public int score = 0;
    float input;
    Rigidbody2D rb;
    Animator anim;

    [Range(1f, 3f)]
    public int gameDifficulty;

    public bool WallDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = maxHealth;
        healthDisplay.text = health.ToString();
        scoreDisplay.text = score.ToString();
        isDead = false;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf == true)
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
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
            health = 0;
        }
        healthDisplay.text = health.ToString();

        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        int randomNumber = Random.Range(0, 10);
        speed = 0;
        anim.SetBool("IsDead", true);
        yield return new WaitForSeconds(2);
        isDead = true;
        Destroy(gameObject);
        Time.timeScale = 0;
        loosePanel.SetActive(true);
        gameoverTexts[randomNumber].SetActive(true);
    }

    public void SetHighscore(int score)
    {
        highscore = score;
    }

    public float GetScore()
    {
        return (float)score;
    }

    public void IncreaseScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreDisplay.text = score.ToString();
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
