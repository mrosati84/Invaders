using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] sounds;

    [SerializeField]
    private AudioClip winMusic;

    private int currentClip = 0;

    private float time = 0f;

    [SerializeField]
    private float interval = 1f; // min 0.25

    private AudioSource audioSource;

    private GameManager gameManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void PlayWinMusic()
    {
        Debug.Log("PLAY WIN");
        audioSource.Stop();
        audioSource.clip = winMusic;
        audioSource.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (time > interval && !(gameManager.gameOver || gameManager.win))
        {
            // do stuff
            audioSource.clip = sounds[currentClip];
            audioSource.Play(0);

            if (currentClip == sounds.Length - 1)
            {
                currentClip = 0;
            }
            else
            {
                currentClip++;
            }

            time = 0;
        }

        time += Time.deltaTime;
    }

    public void SpeedUp()
    {
        interval = Mathf.Clamp(interval - 0.15f, 0.25f, 1f);
    }
}
