using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static int score = 0;

    public GameObject gameoverText;
    public GameObject readyText;
    public GameObject clearText;

    public Text scoreText;
    public Text healthText;
    
    public bool gameOver = false;
    public bool gameClear = false;

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else if (Instance)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        readyText.SetActive(false);
        StartCoroutine(ShowReadyText());
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(score >= 1000)
        {
            StageClear();
        }
    }

    public void FighterScored(int num)
    {
        score += num;
        scoreText.text = "Score : " + score;
    }

    public void FighterDead()
    {
        gameoverText.SetActive(true);
        gameOver = true;
    }

    public void FighterHealth()
    {
        healthText.text = "Health : ";
        for (int i = 0; i < PlayerController.currentHealth; i++)
        {
            healthText.text += "♥ ";
        }
    }

    public void StageClear()
    {
        clearText.SetActive(true);
        gameClear = true;
    }

    IEnumerator ShowReadyText()
    {
        int cnt = 0;
        while(cnt < 3)
        {
            readyText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            readyText.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            ++cnt;
        }
    }
}
