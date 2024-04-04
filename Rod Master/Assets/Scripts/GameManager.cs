using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [Header("Fishing variables")]
    // Used to replace the player's fishing rod when upgraded.
    // Needs to be stored in a static variable to prevent Unity from only spawning the base rod
    private static GameObject staticEquippedRod;
    [SerializeField] GameObject hookObject;
    [SerializeField] GameObject chargeBarObject;
    bool fishingMode = true;
    public bool hookThrown = false;
    public float fishCatchHeight;
    public float rodPowerCharge;

    [Header("Shop variables")]
    readonly string BASE_CURRENCY_TEXT = "Money owned: $";
    public TMPro.TextMeshProUGUI currencyText;
    public int currency;
    public GameObject equippedRod;

    [Header("Level variables")]
    readonly string BASE_MONEY_OWNED_TEXT = "Money owned: ${0}";
    public TMPro.TextMeshProUGUI moneyOwnedText;
    readonly string BASE_FISH_CAUGHT_TEXT = "Caught a {0}\n+${1}";
    public TMPro.TextMeshProUGUI fishCaughtText;


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

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Since some UI elements haven't been created when the GM's Awake() method is called
    // we need to wait until the scene is fully loaded to fix the references
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // Due to how references between scenes are handled with DontDestroyOnLoad it's required
        // to rediscover references in the current scene. 
        // Since GameObject.Find can only locate active objects in the scene we need to manually
        // set the object to be inactive as soon as we find a reference to it.
        GameObject fishCaught = GameObject.FindGameObjectWithTag("FishCaughtText");
        if (fishCaught != null) {
            fishCaughtText = fishCaught.GetComponent<TMPro.TextMeshProUGUI>();
            fishCaught.SetActive(false);
        }

        GameObject moneyOwned = GameObject.FindGameObjectWithTag("MoneyOwnedText");
        if (moneyOwned != null) {
            moneyOwnedText = moneyOwned.GetComponent<TMPro.TextMeshProUGUI>();
        }

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
        if (Input.GetKeyDown(KeyCode.Space) && !hookThrown) {
            ToggleFishingMode();
        }
        UpdateMoneyOwned();

        // TODO: Implement more conditions for throwing the line
        // Throw the fishing line
        if (fishingMode && !hookThrown && Input.GetKeyUp(KeyCode.Mouse0)) {
            CastHook();
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
        SetEquippedRod(rod);
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

    public void SetEquippedRod(GameObject rod) {
        equippedRod = rod;
        staticEquippedRod = rod;
    }

    public void DisplayFishCaughtText(GameObject fishCaught) {
        Fish fish = fishCaught.GetComponent<Fish>();
        fishCaughtText.text = string.Format(BASE_FISH_CAUGHT_TEXT, fish.name, fish.value);
        fishCaughtText.gameObject.SetActive(true);
        StartCoroutine(DisableAfterTimeout(fishCaughtText.gameObject, 1.0f));
    }
    void UpdateMoneyOwned() {
        if (moneyOwnedText) {
            moneyOwnedText.text = string.Format(BASE_MONEY_OWNED_TEXT, currency);
        }
    }

    IEnumerator DisableAfterTimeout(GameObject obj, float timer) {
        yield return new WaitForSeconds(timer);
        obj.SetActive(false);
    }

    Vector3 CalculateCastingAngle() {
        Vector3 castingAngle;
        Vector3 mousePos = Input.mousePosition;
        // Prevent hook from escaping the 2D plane
        mousePos.z = 0;
        Vector3 hookPos = Camera.main.WorldToScreenPoint(hookObject.transform.position);
        // Normalize the angle to prevent the distance of the mouse affecting the power calculation
        castingAngle = (mousePos - hookPos).normalized;
        return castingAngle;
    }

    // Maybe CastLine is a better name?
    void CastHook() {
        float throwMultiplier = 10.0f;
        Vector3 castingAngle = CalculateCastingAngle();
        Rigidbody2D hrb = hookObject.GetComponent<Rigidbody2D>();

        hrb.isKinematic = false;
        hrb.velocity = throwMultiplier * rodPowerCharge * castingAngle;
        chargeBarObject.GetComponent<ChargeBar>().HookThrown();

        hookObject.GetComponent<Hook>().SetReturnPosition(hookObject.transform.position);
        hookThrown = true;
    }

    void RetrieveHook() {
        Rigidbody2D hrb = hookObject.GetComponent<Rigidbody2D>();
        hrb.isKinematic = true;
        chargeBarObject.GetComponent<ChargeBar>().HookRetrieved();
        hookThrown = false;
    }
}
