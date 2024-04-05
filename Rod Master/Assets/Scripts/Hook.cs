using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hook : MonoBehaviour
{
    // Speed of reeling in while holding the button
    public float reelSpeed = 5.0f;
    // Multiplier for the reelSpeed if the player is tapping the button
    [SerializeField] float tapSpeedMultiplier = 2.0f;
    // Flag to track whether the player is reeling the line in
    bool isReeling;

    public bool hooked = false;
    [SerializeField] float tapBuffer;
    bool canTap = true;
    Vector2 returnPosition = Vector2.zero;
    Vector2 returnVector = Vector2.up;
    GameManager gm;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        gm = GameManager.Instance;
        // Get the relevent hook speed 
        reelSpeed = gm.equippedRod.GetComponent<FishingRod>().HookSpeed;
    }

    private void Awake() {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && canTap) {
            StartCoroutine(TapCooldown(tapBuffer));

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

    // Cooldown between taps to differentiate between taps and holds
    IEnumerator TapCooldown(float duration) {
        canTap = false;
        yield return new WaitForSeconds(duration);
        canTap = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("OceanBed")) {
            Rigidbody2D rb =GetComponent<Rigidbody2D>();
            // Disable dynamic physics upon reaching the OceanBed
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            // Calculate the path needed to return to the boat
            returnVector = (returnPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        }
    }

    public void SetReturnPosition(Vector2 vec) {
        returnPosition = vec;
    }
}
