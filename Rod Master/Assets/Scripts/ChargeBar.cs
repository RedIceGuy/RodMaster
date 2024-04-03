using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChargeBar : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] TMP_Text chargingText;
    [SerializeField] float chargeIncrement;
    [SerializeField] float currentCharge;
    [SerializeField] float maxCharge = 100.0f;
    [SerializeField] bool isCharging;
   
    // Player releases the charge button
    public void OnPointerUp(PointerEventData eventData) {
        isCharging = false;
        chargingText.gameObject.SetActive(false);
    }

    // Player is holding the charge button
    public void OnPointerDown(PointerEventData eventData) {
        isCharging = true;
        chargingText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        if (isCharging && currentCharge < maxCharge) {
            currentCharge += chargeIncrement * Time.deltaTime;
        } 
        else if (isCharging && currentCharge > maxCharge) {
            currentCharge = maxCharge;
        }
    }
}
