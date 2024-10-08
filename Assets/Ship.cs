using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public float speed = 2f;

    private float shootCooldown;

    [SerializeField]
    private float shootInterval = 1.0f;

    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private Sprite explosion;

    public bool alive = true;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Text gameOver;

    [SerializeField]
    private GameObject gameManager;

    private GameManager gameManagerScript;

    [SerializeField]
    private float leftLimit = -6.5f;

    [SerializeField]
    private float rightLimit = 6.5f;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip dieSound;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shootCooldown = shootInterval;
        gameManagerScript = gameManager.GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && !gameManagerScript.gameOver)
        {
            Move();
            Shoot();
        }
    }

    private void Move()
    {
        Vector3 newPos = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // transform.position += Vector3.left * speed * Time.deltaTime;
            newPos.x = Mathf.Clamp(transform.position.x - speed * Time.deltaTime, leftLimit, rightLimit);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // transform.position += Vector3.right * speed * Time.deltaTime;
            newPos.x = Mathf.Clamp(transform.position.x + speed * Time.deltaTime, leftLimit, rightLimit);
        }

        transform.position = newPos;
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && shootCooldown > shootInterval)
        {
            Instantiate(laser, new Vector3(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            audioSource.Play(0);
            shootCooldown = 0f;
        }

        shootCooldown += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!alive)
        {
            // prevent to be hit when already dead.
            return;
        }

        Destroy(collider.gameObject); // destroy incoming bullet
        alive = false;

        spriteRenderer.sprite = explosion;

        StopSound();
        audioSource.clip = dieSound;
        audioSource.Play(0);

        StartCoroutine("Die");
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        gameManagerScript.GameOver();
        Destroy(gameObject);
    }
}
