using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    GameManager gm;
    [SerializeField]
    GameObject RodToPurchase;
    public Button button;
    public int price = 0;

    private void Awake() {
        gm = GameManager.Instance;
        button = GetComponent<Button>();

        if (gm.currency < price) {
            button.interactable = false;
        }
    }

    public void PurchaseRod() {
        gm.PurchaseRod(RodToPurchase, price);
        button.interactable = false;
    }
}
