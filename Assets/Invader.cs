using UnityEngine;
using System;
using System.Collections;

public class Invader : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    private int currentSprite = 0;

    private SpriteRenderer spriteRenderer;

    private float time = 0f;

    [SerializeField]
    private int score = 10;

    private bool alive = true;

    private GameManager gameManagerScript;

    [SerializeField]
    private Sprite explosionSprite;

    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private float shootProbability = 0.0005f;

    [SerializeField]
    private GameObject player;

    private AudioSource audioSource;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[currentSprite];
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (time > 1 && alive)
        {
            AnimateSprite();
            time = 0;
        }

        if (CanShoot() && !gameManagerScript.gameOver)
        {
            Shoot();
        }

        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (alive)
        {
            if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Bunker")
            {
                gameManagerScript.GameOver();
            }

            gameManagerScript.addScore(score);
            spriteRenderer.sprite = explosionSprite;
            alive = false;

            audioSource.Play(0);
            player.GetComponent<Ship>().StopSound();

            Destroy(collider.gameObject);
            StartCoroutine("Die");
        }
    }

    private void Shoot()
    {
        if (UnityEngine.Random.Range(0f, 1f) < shootProbability)
        {
            Instantiate(laser, transform.position, Quaternion.identity);
        }
    }

    private bool CanShoot()
    {
        // blue alien means can shoot
        RaycastHit2D ray = Physics2D.Raycast(transform.position + Vector3.down * 0.4f, Vector3.down);
        Debug.DrawRay(transform.position + Vector3.down * 0.4f, Vector3.down, Color.red);

        if (ray.collider)
        {
            if (ray.collider.gameObject.tag == "Invader" || ray.collider.gameObject.tag == "InvaderLaser")
            {
                // spriteRenderer.color = Color.white;
                return false;
            }
            else
            {
                // spriteRenderer.color = Color.blue;
                return true;
            }
        }
        else
        {
            // spriteRenderer.color = Color.blue;
            return true;
        }
    }

    private void AnimateSprite()
    {
        currentSprite = Math.Abs(currentSprite - 1);
        spriteRenderer.sprite = sprites[currentSprite];
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
