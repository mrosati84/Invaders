using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    public bool gameOver = false;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text winText;

    public bool win = false;

    MusicManager musicManager;

    public void addScore(int s)
    {
        score += s;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
    }

    private void Win()
    {
        win = true;
        winText.gameObject.SetActive(true);

        musicManager.PlayWinMusic();
    }

    void Start()
    {
        musicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
    }

    void Update()
    {
        if (gameOver || win)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Splash");
        }

        if (GameObject.FindGameObjectsWithTag("Invader").Length == 0)
        {
            if (!win)
            {
                Win();
            }
        }
    }
}
