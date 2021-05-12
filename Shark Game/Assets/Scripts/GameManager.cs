using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Fishpool fishpool;
    public GameObject shark;

    public float timer;
    public int score;
    public int lives;

    public int eaten = 0;

    public bool endGame;

    public static GameManager manager;

    [SerializeField]
    Text timerText;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text livesText;

    [SerializeField]
    Text endGameText;

    // Start is called before the first frame update
    void Start()
    {
        if(manager == null)
            manager = this;

        ResetGame();
        SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(!endGame)
            timer -= Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.R) && endGame)
        {
            ResetGame();
        }

        if(lives == 0 || timer < 0f)
        {
            endGame = true;
            endGameText.gameObject.SetActive(true);
        }

        if(eaten >= 5)
        {
            eaten = 0;
            fishpool.Repopulate();
            SelectTarget();
        }

        timerText.text = Mathf.Floor(timer / 60f).ToString() + ":" + Mathf.Ceil(Mathf.Floor(timer) % 60).ToString("00");
        scoreText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString() + " / 3";
    }

    void ResetGame()
    {
        endGameText.gameObject.SetActive(false);
        shark.GetComponent<MoveShark>().ReturnToZero();
        timer = 300f;
        score = 0;
        lives = 3;
        eaten = 0;
        endGame = false;
        fishpool.ResetFish();
        SelectTarget();
    }

    public static void LowerLives()
    {
        manager.lives--;
        manager.eaten++;
        manager.score -= 5;
        manager.SelectTarget();
    }

    public static void AddScore()
    {
        manager.eaten++;
        manager.score += 10;
        manager.SelectTarget();
    }

    public void SelectTarget()
    {
        foreach(GameObject f in fishpool.fishList)
        {
            f.GetComponent<Fish>().tagged = false;
        }

        while (true) {
            int temp = Random.Range(0, fishpool.poolSize);
            if (fishpool.fishList[temp].activeInHierarchy)
            {
                fishpool.fishList[temp].GetComponent<Fish>().tagged = true;
                break;
            }
        }
    }
}
