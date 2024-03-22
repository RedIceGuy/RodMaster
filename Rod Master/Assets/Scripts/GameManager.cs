using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    bool fishingMode = true;
    public float fishCatchHeight;

    [Header("Shop variables")]
    readonly string BASE_CURRENCY_TEXT = "Money owned: $";
    public TMPro.TextMeshProUGUI currencyText;
    public int currency;
    public GameObject equippedRod;

    public static GameManager Instance {
        get {
            if(_instance == null) {
                // Debug.Log("GameManager is NULL");
            }
            return _instance;
        }
    }

    private void Awake() {
        if (_instance == null) {
            _instance = this;
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
        // Debug.Log("SetNewRod");
        if (p) {
            // Debug.Log("P exists");
            // Remove old rod
            Transform oldRodTransform = null;
            foreach (Transform child in p.transform) {  
                oldRodTransform = child.gameObject.transform;
                Destroy(child.gameObject);
            }
            // Set new rod as child
            GameObject rod = Instantiate(equippedRod, p.transform);
            if (oldRodTransform) {
                rod.transform.SetPositionAndRotation(oldRodTransform.position, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ToggleFishingMode();
            // Debug.Log(fishingMode ? "Fishing!" : "Moving!");
        }
    }

    public bool GetFishingMode() {
        return fishingMode;
    }

    void ToggleFishingMode() {
        fishingMode = !fishingMode;
    }
    void UpgradeFishingRod(GameObject betterRod, int price) {
        currency -= price;
        equippedRod = betterRod;
    }

    void UpdateCurrency() {
        currencyText.text = BASE_CURRENCY_TEXT + currency.ToString();
    }

    public void PurchaseRod(GameObject rod, int price) {
        if (currency >= price) {
            UpgradeFishingRod(rod, price);
        }
        UpdateCurrency();
    }

    public void SetCurrencyText(TMPro.TextMeshProUGUI text) {
        currencyText = text;
        UpdateCurrency();
    }
}
