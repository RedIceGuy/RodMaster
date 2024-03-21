using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    public TMPro.TextMeshProUGUI currencyText;
    GameManager gm;
    void Awake() {
        gm = GameManager.Instance;
        gm.SetCurrencyText(currencyText);
    }
}
