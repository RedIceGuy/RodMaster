using UnityEngine;
using UnityEngine.SceneManagement;

public class Hook : MonoBehaviour
{
    GameManager gm;
    // Speed of reeling in while holding the button
    public float reelSpeed;
    // Multiplier for the reelSpeed if the player is tapping the button
    [SerializeField] float tapSpeedMultiplier = 8.0f;
    // Flag to track whether the player is reeling the line in
    bool isReeling;
    public bool canReelIn = false;
    public bool hooked = false;
    [SerializeField] float tapBuffer;
    Vector2 returnPosition = Vector2.zero;
    Vector2 returnVector = Vector2.up;


    private void Awake() {
        gm = GameManager.Instance;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        gm = GameManager.Instance;
        // Get the relevent hook speed 
        reelSpeed = gm.equippedRod.GetComponent<FishingRod>().HookSpeed;
    }

    void Update()
    {
        // Player can't reel the hook in if it hasn't been cast
        if (!gm.hookThrown) {
            return;
        }
        ReelIn();
    }

    void ReelIn() {
        Vector2 movement = Vector2.zero;
        isReeling = true;

        // Detect rapid clicking of the mouse button
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            // Reeling with an increased speed
            movement = tapSpeedMultiplier * reelSpeed * Time.deltaTime * returnVector;
        }
        // Detect holding down mouse button
        else if (Input.GetKey(KeyCode.Mouse0)) {
            // Reeling at a constant speed
            movement = reelSpeed * Time.deltaTime * returnVector;
        }
        else {
            // Player isn't reeling in
            isReeling = false;
        }
        // Only apply translation if movement is happening
        if (isReeling) {
            transform.Translate(movement);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("OceanBed") || other.CompareTag("Fish")) {
            canReelIn = true;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            // Disable dynamic physics upon reaching the OceanBed
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            // Calculate the path needed to return to the boat
            returnVector = (returnPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        }
        else if (other.gameObject.CompareTag("Boat") && canReelIn) {
            gm.RetrieveHook();
            canReelIn = false;
        }
    }

    public void SetReturnPosition(Vector2 vec) {
        returnPosition = vec;
    }
}
