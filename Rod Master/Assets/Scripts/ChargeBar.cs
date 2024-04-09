using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    GameManager gm;
    [SerializeField] TMP_Text chargingText;
    [SerializeField] Slider chargingSlider;
    [SerializeField] float chargeIncrement;
    [SerializeField] float currentCharge;
    [SerializeField] float maxCharge = 100.0f;
    [SerializeField] bool isCharging;
    bool canCharge;

    void Awake() {
        gm = GameManager.Instance;
        // Prevent the player from "dragging" the charging bar
        chargingSlider.interactable = false;
        canCharge = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        gm = GameManager.Instance;
    }

    void Update() {
        // Prevent charging outside of the casting state
        if(!canCharge || !gm.canThrowHook) {
            return;
        }

        isCharging = Input.GetKey(KeyCode.Mouse0);

        if (isCharging && currentCharge < maxCharge) {
            currentCharge += chargeIncrement * Time.deltaTime;
        } 
        else if (isCharging && currentCharge > maxCharge) {
            currentCharge = maxCharge;
        }
        // Update the charge visually
        chargingSlider.value = currentCharge;
        // Update the charge for gameplay
        gm.rodPowerCharge = currentCharge;
    }

    public void HookThrown() {
        canCharge = false;
        chargingText.gameObject.SetActive(false);
        chargingSlider.gameObject.SetActive(false);
        currentCharge = 0;
    }

    public void HookRetrieved() {
        canCharge = true;
        chargingSlider.value = 0;
        chargingText.gameObject.SetActive(true);
        chargingSlider.gameObject.SetActive(true);
    }

    // Reset the current power of the charge
    public void ResetCharge() {
        chargingSlider.value = 0;
        currentCharge = 0;
    }
}
