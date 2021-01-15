using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject gameoverText;
    public GameObject readyText;

    public Text scoreText;
    public Text healthText;

    public bool gameOver = false;

    int score = 0;

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
        StartCoroutine("ShowReadyText");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void FighterScored(int num)
    {
        score += num;
        scoreText.text = "Score : " + score.ToString();
    }

    public void FighterDead()
    {
        gameoverText.SetActive(true);
        gameOver = true;
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
