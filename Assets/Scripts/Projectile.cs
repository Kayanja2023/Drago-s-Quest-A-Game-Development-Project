
using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField] private float speed;
    private float direction;
    private bool hit;

    private Animator anim;
    private BoxCollider2D boxCollider;
    private float lifetime;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        lifetime += Time.deltaTime;
        if (lifetime > 3) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        // Check if we've hit an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            // Get the enemy's Health script and cause damage
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1);
            }
        }
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        if (direction < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
  
}

