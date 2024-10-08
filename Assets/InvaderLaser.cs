using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderLaser : MonoBehaviour
{
    [SerializeField]
    private int speed = 6;

    [SerializeField]
    private Sprite explosion;

    private SpriteRenderer spriteRenderer;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }

        transform.position = transform.position + (Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Laser")
        {
            Destroy(collider.gameObject); // destroy player laser
        }

        canMove = false;
        spriteRenderer.sprite = explosion;
        StartCoroutine("Die");
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
