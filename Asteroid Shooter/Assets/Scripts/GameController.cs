using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazard;
    public Vector3 spawnValues;
    public int hazardcount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    private int score;

    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver;
    private bool restart;

    private void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        updateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            for (int i = 0; i < hazardcount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0.0f, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard[Random.Range(0,2)], spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
        restartText.text = "Press 'R' for Restart";
        restart = true;
    }

    void updateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void addScore(int score)
    {
        this.score += score;
        updateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}
