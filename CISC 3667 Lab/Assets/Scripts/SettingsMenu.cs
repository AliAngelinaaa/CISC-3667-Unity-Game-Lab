using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found in the scene");
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
            audioSource.volume = volumeSlider.value;
        }
    }

    public void OnVolumeSliderChanged()
    {
        if (audioSource != null)
            audioSource.volume = volumeSlider.value;
            PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
    
}
