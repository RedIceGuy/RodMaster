using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    // Used to replace the player's fishing rod when upgraded.
    // Needs to be stored in a static variable to prevent Unity from only spawning the base rod
    private static GameObject staticEquippedRod;
    bool fishingMode = true;
    public float fishCatchHeight;

    [Header("Shop variables")]
    readonly string BASE_CURRENCY_TEXT = "Money owned: $";
    public TMPro.TextMeshProUGUI currencyText;
    public int currency;
    public GameObject equippedRod;

    public static GameManager Instance {
        get {
            return _instance;
        }
    }

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            staticEquippedRod = equippedRod;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this) {
            Destroy(gameObject);
        }
        SetNewRod();
    }

    void SetNewRod() {
        GameObject p = GameObject.Find("RodPivot");
        // Only need to update the rod if we are in a fishing level
        if (p) {
            // Remove old rod
            Transform oldRodTransform = null;
            foreach (Transform child in p.transform) {  
                oldRodTransform = child.gameObject.transform;
                Destroy(child.gameObject);
            }
            // Set new rod as child
            GameObject rod = Instantiate(staticEquippedRod, p.transform);
            // Inherit the transform values
            if (oldRodTransform) {
                rod.transform.SetPositionAndRotation(oldRodTransform.position, Quaternion.identity);
                rod.transform.localScale = oldRodTransform.localScale;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ToggleFishingMode();
        }
    }

    public bool GetFishingMode() {
        return fishingMode;
    }

    void ToggleFishingMode() {
        fishingMode = !fishingMode;
    }
    void UpgradeFishingRod(GameObject rod, int price) {
        currency -= price;
        equippedRod = rod;
        staticEquippedRod = rod;
    }

    void UpdateCurrency() {
        currencyText.text = BASE_CURRENCY_TEXT + currency.ToString();
    }

    public void PurchaseRod(GameObject rod, int price) {
        if (currency >= price) {
            UpgradeFishingRod(rod, price);
            Debug.Log(equippedRod);
        }
        UpdateCurrency();
    }

    public void SetCurrencyText(TMPro.TextMeshProUGUI text) {
        currencyText = text;
        UpdateCurrency();
    }
}
