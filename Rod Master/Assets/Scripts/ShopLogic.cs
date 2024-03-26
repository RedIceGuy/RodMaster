using System.Collections.Generic;
using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    private static ShopLogic _instance;
    public TMPro.TextMeshProUGUI currencyText;
    GameManager gm;
    public List<GameObject> ownedFishingRods = new();
    public static ShopLogic Instance {
        get {
            return _instance;
        }
    }
    void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this) {
            Destroy(gameObject);
        }

        gm = GameManager.Instance;
        gm.SetCurrencyText(currencyText);
    }
}
