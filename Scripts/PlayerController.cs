using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public float jumpForce = 5;

    public GameObject loseScreenUI;

    public int score, highScore;

    public Text scoreUI, highScoreUI;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("High Score : ");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
    }

    void PlayerJump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
            AudioManager.singleton.PlaySound(0);
        }
    }

    void AddScore()
    {
        score++;
        scoreUI.text = score.ToString();
        AudioManager.singleton.PlaySound(1);
    }

    void PlayerLose()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("High Score : ", highScore);
        }
        highScoreUI.text = "High Score : " + highScore.ToString();
        loseScreenUI.SetActive(true);
        Time.timeScale = 0;
        AudioManager.singleton.PlaySound(2);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("obstacle"))
        {
            PlayerLose();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("score"))
        {
            AddScore();
        }

    }
}
