using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovementKoopa : MonoBehaviour
{
    public float speed = 1f;
    public float range;
    private float distToPlayer;
    public Vector2 direction = Vector2.left;
    public Transform player;
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    private bool flip;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
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

        Vector3 scale = transform.localScale;;

        if(player.transform.position.x > transform.position.x) 
        {
            scale.x = Mathf.Abs(scale.x)*-1 * (flip ? -1 : 1);
            transform.Translate(speed * Time.deltaTime, y:0, z:0);
        } else {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            transform.Translate(speed * Time.deltaTime * -1, y:0, z:0);
        }
        transform.localScale = scale;
    
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
