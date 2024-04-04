using System.Collections;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float holdSpeed = 5f;
    float clickSpeed;
    public bool hooked = false;
    [SerializeField] float tapBuffer;
    bool canTap = true;

    private void Awake() {
        clickSpeed = holdSpeed * 3.0f;
    }

    void Update()
    {
        // float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = Vector2.zero;
        // Detect rapid clicking of the mouse button
        if (Input.GetKeyDown(KeyCode.Mouse0) && canTap) {
            Debug.Log("Tapping");
            movement = clickSpeed * Time.deltaTime * Vector2.up;
            // StartCoroutine(TapCooldown(tapBuffer));
        }
        // Detect holding down mouse button
        else if (Input.GetKey(KeyCode.Mouse0)) {
            Debug.Log("Holding");
            movement = holdSpeed * Time.deltaTime * Vector2.up;
        }

        transform.Translate(movement);
    }

    // Cooldown between taps to differentiate between taps and holds
    IEnumerator TapCooldown(float duration) {
        canTap = false;
        yield return new WaitForSeconds(duration);
        canTap = true;
    }
}
