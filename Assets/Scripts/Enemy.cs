using UnityEngine;
public interface ISpeedInteractable
{
    void onHit(Vector2 direction, Vector2 launchForce);
}
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, ISpeedInteractable
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D col;
    [SerializeField] float acceptance;
    [SerializeField] float targetRadius;
    [SerializeField] float speed;
    [SerializeField] Vector2 speedR;
    [SerializeField] bool randSpeed;
    [SerializeField] float pauseTime;
    [SerializeField] SpriteRenderer spriteRenderer;
    float timer;
    static Vector2 yBounds;
    public bool dead = false;
    public int EnemyType;
    Vector2 target;
    bool st = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        yBounds.x = -0.6f;
        yBounds.y = 0.5f;
        if (randSpeed) {
            speed = Random.Range(speedR.x,speedR.y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) {
            return;
        }
        if ((target == null || Vector2.Distance(target, transform.position) <= acceptance)||st)
        {
            st = false;
            switch (EnemyType)
            {
                case 0:
                    target = new Vector2(this.transform.position.x + Random.Range(-targetRadius, targetRadius), transform.position.y);

                    break;
                case 1:
                    target = (Vector2)transform.position + Random.insideUnitCircle * targetRadius;
                    target = new Vector2(target.x, Mathf.Clamp(target.y, yBounds.x, yBounds.y));
                    break;
            }
            timer = 0;
        }
        spriteRenderer.flipX = (target.x - transform.position.x)<0;
        if (timer >= pauseTime) {
            transform.Translate((target-(Vector2)transform.position).normalized*speed*Time.deltaTime);
            return;
        }
        timer += Time.deltaTime;
    }

    public void onHit(Vector2 direction, Vector2 launchForce)
    {
        if (dead) { return; }
        dead = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.isTrigger = true;
        Vector2 dir = direction - 2 * direction * (Vector2.Dot(direction, Vector2.up));
        rb.AddForce(new Vector2(dir.x * launchForce.x, launchForce.y) * 0.2f * Time.deltaTime, ForceMode2D.Impulse);
        Spawner.spawnCount--;
        GameManager.rageMeter++;
        Destroy(gameObject, 2);
    }
}
