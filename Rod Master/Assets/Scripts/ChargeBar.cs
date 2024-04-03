using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] TMP_Text chargingText;
    [SerializeField] Slider chargingSlider;
    [SerializeField] float chargeIncrement;
    [SerializeField] float currentCharge;
    [SerializeField] float maxCharge = 100.0f;
    [SerializeField] bool isCharging;
   
    // Player releases the charge button
    public void OnPointerUp(PointerEventData eventData) {
        isCharging = false;
    }

    // Player is holding the charge button
    public void OnPointerDown(PointerEventData eventData) {
        isCharging = true;
    }

    void Update() {
        if (isCharging && currentCharge < maxCharge) {
            currentCharge += chargeIncrement * Time.deltaTime;
        } 
        else if (isCharging && currentCharge > maxCharge) {
            currentCharge = maxCharge;
        }
        chargingSlider.value = currentCharge;
    }
}
