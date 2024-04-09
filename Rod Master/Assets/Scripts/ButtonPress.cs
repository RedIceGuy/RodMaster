using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    AudioManager _audioManager;
    private void Awake() {
        _audioManager = AudioManager.Instance;
    }

    public void ButtonPressed() {
        // Prevent edgecase of NullReferenceException on Scene transitions
        if (_audioManager) {
            _audioManager.PlayButtonPressed();
        } else {
            AudioManager.Instance.PlayButtonPressed();
        }
    }
}
