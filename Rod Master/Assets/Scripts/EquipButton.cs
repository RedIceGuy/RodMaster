using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour
{
    GameManager gm;
    ShopLogic sl;
    [SerializeField] GameObject RodToEquip;
    Button button;
    TMPro.TextMeshProUGUI equipText;

    readonly string equipped = "Equipped";
    readonly string equip = "Equip";
    readonly string notOwned = "Not Owned";
    private void Awake() {
        gm = GameManager.Instance;
        sl = ShopLogic.Instance;
        button = GetComponent<Button>();
        equipText = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        // Player can only equip fishing rods that they own
        if (sl.ownedFishingRods.Contains(RodToEquip)) {
            // User can't "re-equip" the currently equipped fishing rod
            if (RodToEquip == gm.equippedRod) {
                RodEquipped();
                return;
            } else {
                equipText.text = equip;
            }
        // Player doesn't own the corresponding fishing rod
        } else {
            equipText.text = notOwned;
            button.interactable = false;
        }
    }

    public void EquipRod() {
        gm.SetEquippedRod(RodToEquip);
        RodEquipped();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Sets the current rod as being equipped
    void RodEquipped() {
        equipText.text = equipped;
        button.interactable = false;
    }
}
