using UnityEngine;

public class ShopButton : MonoBehaviour
{
    GameManager gm;
    [SerializeField]
    GameObject RodToPurchase;

    private void Awake() {
        gm = GameManager.Instance;
    }

    public void PurchaseRod(int price) {
        Debug.Log("Pressing the purchase button");
        if (gm.currency >= price) {
            gm.UpgradeFishingRod(RodToPurchase, price);
        }
    }
}
