using UnityEngine;
using UnityEngine.SceneManagement;
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
            RodBought();
        }
        // If the player can't afford to buy the rod
        else if (gm.currency < price) {
            button.interactable = false;
        }
    }

    public void PurchaseRod() {
        gm.PurchaseRod(RodToPurchase, price);
        sl.ownedFishingRods.Add(RodToPurchase);
        RodBought();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RodBought() {
        priceText.text = "Already owned";
        // Different color for when rod is owned vs. when it can't be afforded
        var colors = button.colors;
        colors.disabledColor = Color.yellow;
        button.colors = colors;

        button.interactable = false;
    }
}
