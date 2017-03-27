using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameProcess : MonoBehaviour
{
    public Text scoreText;
    public int score;
    public GameObject gameOverPanel;
    public Text finalScore;
    //public GameObject player;
    public GameObject airGenerator;
    public GameObject groundGenerator;

    public bool isGameOver;

    // Use this for initialization
    void Start()
    {
        score = 0;
        isGameOver = false;
        //player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update ()
    {
        scoreText.text = score.ToString();
    }

    public void gameOver()
    {
        isGameOver = true;
        airGenerator.SetActive(false);
        groundGenerator.SetActive(false);
        //player.SetActive(false);
        finalScore.text += score.ToString();
        gameOverPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void gameRestart()
    {
        SceneManager.LoadScene("Game");
    }
}
