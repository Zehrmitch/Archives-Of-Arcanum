using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    private float timeLeft = 120;
    private int playerScore = 0;
    public Transform player; // player.position.x.ToString();
    public TextMeshProUGUI timeText;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = "Time Left: " + (int)timeLeft;
        if (timeLeft < 0.1f)
        {
            SceneManager.LoadScene("Prototype_1");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        CountScore();
    }

    void CountScore()
    {
        playerScore = playerScore + (int)(timeLeft * 10);
    }
}
