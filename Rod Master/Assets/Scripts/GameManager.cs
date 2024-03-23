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
        BoatRod();
    }

    void SetNewRod() {
        GameObject p = GameObject.Find("RodPivot");
        Debug.Log(equippedRod);
        // Only need to update the rod if we are in a fishing level
        if (p) {
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

    void Rod() {
        // Get the parent of the fishing rod
        GameObject pivot = GameObject.Find("RodPivot");
        
        // TODO: Try removing the rod from the boat prefab and use the GM to instantiate the everytime rod instead
        foreach (Transform oldRod in pivot.transform) {
            // PrefabUtility.ReplacePrefabAssetOfPrefabInstance(PrefabUtility.ConvertToPrefabInstance(oldRod.gameObject, pivot, ConvertToPrefabInstanceSettings.Equals, InteractionMode.AutomatedAction), equippedRod, InteractionMode.AutomatedAction);
        }
        // GameObject oldRod = PrefabUtility.GetCorrespondingObjectFromSource(gameObject)
    }

    void BoatRod() {
        // Get the parent of the fishing rod
        GameObject pivot = GameObject.Find("RodPivot");
        Instantiate(equippedRod, pivot.transform);
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
            Debug.Log(equippedRod);
        }
        UpdateCurrency();
    }

    public void SetCurrencyText(TMPro.TextMeshProUGUI text) {
        currencyText = text;
        UpdateCurrency();
    }
}
