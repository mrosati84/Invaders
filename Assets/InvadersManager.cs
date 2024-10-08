using UnityEngine;

public class InvadersManager : MonoBehaviour
{
    [SerializeField]
    private GameObject invader;

    private float time = 0f;

    [SerializeField]
    private float minX = -2f; // left boundary

    [SerializeField]
    private float maxX = 2f; // right boundary

    [SerializeField]
    private float moveUnit = 0.5f; // units of movement

    [SerializeField]
    private float moveInterval = 0.5f; // rate of movement (seconds)

    private int direction = 1;

    [SerializeField]
    private bool move = true; // stop all movements

    private bool goDown = false;

    [SerializeField]
    private GameObject gameManager;

    private GameManager gameManagerScript;

    [SerializeField]
    private GameObject musicManager;

    private MusicManager musicManagerScript;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        musicManagerScript = musicManager.GetComponent<MusicManager>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!move || gameManagerScript.gameOver || gameManagerScript.win)
        {
            return;
        }

        if (time > moveInterval)
        {
            FlipDirection();
            Move();

            time = 0f;
        }

        time += Time.deltaTime;
    }

    private void Move()
    {
        // handle down movement
        if (transform.position.x == minX || transform.position.x == maxX)
        {
            if (goDown)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - moveUnit);
                goDown = false;
                time = 0f;
                moveInterval = Mathf.Clamp(moveInterval - 0.05f, 0.07f, 0.5f);
                musicManagerScript.SpeedUp();
                return;
            }
        }

        // handle lateral movement
        if (transform.position.x >= minX && transform.position.x <= maxX)
        {
            transform.position = new Vector3(transform.position.x + (moveUnit * direction), transform.position.y);
            goDown = true;
        }
    }

    private void FlipDirection()
    {
        if (transform.position.x == minX)
        {
            direction = 1;
        }

        if (transform.position.x == maxX)
        {
            direction = -1;
        }
    }
}
