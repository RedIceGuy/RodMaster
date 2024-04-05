using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    private Rigidbody2D rb;
    GameManager gm;

    [Header("Disabled while moving")]
    [SerializeField] GameObject rodPivot;
    [SerializeField] GameObject hook;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Can't move boat while fishing
        if (!gm.GetFishingMode()) {
            rodPivot.SetActive(false);
            hook.SetActive(false);
            Move();
        } else {
            rodPivot.SetActive(true);
            hook.SetActive(true);
        }
    }

    void Move() {
        Vector2 movement = Vector2.zero;
        // Move left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            movement += Vector2.left;
        }
        // Move right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            movement += Vector2.right;
        }
        // Prevent frame-dependent movement
        movement = speed * Time.deltaTime * movement;
        rb.velocity += movement;

        float xVelocity = rb.velocity.x;
        float deltaVelocity = 0;
        // Max speed to the right
        if (xVelocity > maxSpeed) {
            deltaVelocity = rb.velocity.x - maxSpeed;
        }
        // Max speed to the left
        else if (xVelocity < -maxSpeed) {
            // Notice the negation of maxSpeed
            deltaVelocity = rb.velocity.x - maxSpeed * -1;
        }

        // Clamp down to max speed
        rb.velocity -= new Vector2(deltaVelocity, 0);
    }
}
