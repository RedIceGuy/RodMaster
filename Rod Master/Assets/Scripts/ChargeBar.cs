using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] TMP_Text chargingText;
    [SerializeField] Slider chargingSlider;
    [SerializeField] float chargeIncrement;
    [SerializeField] float currentCharge;
    [SerializeField] float maxCharge = 100.0f;
    [SerializeField] bool isCharging;
    public bool canCharge;

    void Update() {
        if(!canCharge) {
            return;
        }

        isCharging = Input.GetKey(KeyCode.Mouse0);

        if (isCharging && currentCharge < maxCharge) {
            currentCharge += chargeIncrement * Time.deltaTime;
        } 
        else if (isCharging && currentCharge > maxCharge) {
            currentCharge = maxCharge;
        }
        chargingSlider.value = currentCharge;
    }
}
