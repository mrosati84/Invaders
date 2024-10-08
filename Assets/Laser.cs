using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.5f;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite explosion;

    [SerializeField]
    private Color explosionColor = Color.red;

    private bool alive = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (transform.position.y >= 4.5)
        {
            spriteRenderer.sprite = explosion;
            spriteRenderer.color = explosionColor;
            alive = false;
            StartCoroutine("Die");
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
