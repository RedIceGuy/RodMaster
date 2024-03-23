using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    GameManager gm;
    ShopLogic sl;
    [SerializeField] GameObject RodToPurchase;
    Button button;
    TMPro.TextMeshProUGUI priceText;
    public int price = 0;

    private void Awake() {
        gm = GameManager.Instance;
        sl = ShopLogic.Instance;
        button = GetComponent<Button>();
        priceText = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        // If the player already owns the fishing rod assigned to the button
        if (sl.ownedFishingRods.Contains(RodToPurchase)) {
            priceText.text = "Already owned";
            button.interactable = false;
        }
        else if (gm.currency < price) {
            button.interactable = false;
        }
    }

    public void PurchaseRod() {
        gm.PurchaseRod(RodToPurchase, price);
        if (sl.ownedFishingRods == null) {
            Debug.LogWarning("Owned Rods is NULL!");
        }
        sl.ownedFishingRods.Add(RodToPurchase);
        button.interactable = false;
    }
}
