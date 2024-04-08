using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    [SerializeField] AudioSource _audioSource;

    [Header("Audio files")]
    [SerializeField] AudioClip fishCaught;
    [SerializeField] AudioClip fishHooked;
    [SerializeField] AudioClip buttonPressed;
    [SerializeField] AudioClip paddleBoat;
    [SerializeField] AudioClip waterSplash;

    public static AudioManager Instance {
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
    }

    void PlayClip(AudioClip clip) {
        _audioSource.PlayOneShot(clip);
    }

    public void PlayFishCaught() {
        PlayClip(fishCaught);
    }
    
    public void PlayFishHooked() {
        PlayClip(fishHooked);
    }
    
    public void PlayButtonPressed() {
        PlayClip(buttonPressed);
    }
    
    public void PlayPaddleBoat() {
        PlayClip(paddleBoat);
    }
    
    public void PlayWaterSplash() {
        PlayClip(waterSplash);
    }
    
}
