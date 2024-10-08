using UnityEngine;

public class Bunker : MonoBehaviour
{
    private int health = 20;

    [SerializeField]
    private Sprite[] damageSprites;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        health--;

        if (health == 0)
        {
            Destroy(gameObject);
        }

        if (collider.gameObject.tag == "Laser")
        {
            Destroy(collider.gameObject);
        }

        spriteRenderer.sprite = damageSprites[health / 5];
    }
}
