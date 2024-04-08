using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    AudioManager _audioManager;
    private void Awake() {
        _audioManager = AudioManager.Instance;
    }

    public void ButtonPressed() {
        _audioManager.PlayButtonPressed();
    }
}
