using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    readonly string BASE_CURRENCY_TEXT = "Money owned: $";
    public TMPro.TextMeshProUGUI currencyText;
    GameManager gm;
    void Awake() {
        gm = GameManager.Instance;
        UpdateCurrency();
    }

    public void UpdateCurrency() {
        currencyText.text = BASE_CURRENCY_TEXT + gm.currency.ToString();
    }

    public void PurchaseRod(GameObject rod, int price) {
        if (gm.currency >= price) {
            gm.UpgradeFishingRod(rod, price);
        }
        UpdateCurrency();
    }
}
