using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    private Rigidbody2D rb;
    GameManager gm;
    public GameObject rod;
    // Start is called before the first frame update
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
            Move();
        } else {
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
    }
}
