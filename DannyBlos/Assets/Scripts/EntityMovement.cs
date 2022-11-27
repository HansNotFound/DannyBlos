using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public float range;
    private float distToPlayer;

    public float shootingRange;
    public GameObject bullet;
    public GameObject bulletParent;

    public float fireRate = 1f;
    private float nextFireTime;
    public Vector2 direction = Vector2.left;
    public Transform player;
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    private bool mustPatrol;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
        nextFireTime = Time.time;
    }

    private void OnBecameVisible()
    {
        #if UNITY_EDITOR
        enabled = !EditorApplication.isPaused;
        #else
        enabled = true;
        #endif
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

        if (rigidbody.Raycast(direction)) {
            direction = -direction;
        }

        if (rigidbody.Raycast(Vector2.down)) {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }

        if (direction.x > 0f) {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        } else if (direction.x < 0f) {
            transform.localEulerAngles = Vector3.zero;
        }

        distToPlayer = Vector2.Distance(transform.position, player.position);
        Debug.Log(distToPlayer);
        if (distToPlayer <= range && nextFireTime < Time.time) 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.fixedDeltaTime);
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
        nextFireTime = Time.time + fireRate;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
        // Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
