using UnityEngine;
using UnityEngine.UI;

public class ESCManager : MonoBehaviour
{
    public Slider volumeSlider;
    [SerializeField] private AudioSource audioSource;
    private float currentVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
            currentVolume = 0.5f;
        if (currentVolume != volumeSlider.value)
        {
            //currentVolume = PlayerPrefs.GetFloat("Volume", volumeSlider.value);
            //volumeSlider.value = currentVolume;
            //audioSource.volume = currentVolume;
            PlayerPrefs.SetFloat("Volume", volumeSlider.value);
            PlayerPrefs.Save();
            currentVolume = volumeSlider.value;
        }
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}