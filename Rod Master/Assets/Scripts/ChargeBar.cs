using TMPro;
using UnityEngine;
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
    public bool canCharge = true;

    void Awake() {
        gm = GameManager.Instance;
        // Prevent the player from "dragging" the charging bar
        chargingSlider.enabled = false;
    }

    void Update() {
        // if(!canCharge) {
        //     return;
        // }

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
}
