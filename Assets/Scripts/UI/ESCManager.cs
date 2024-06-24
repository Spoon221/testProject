using UnityEngine;
using UnityEngine.UI;

public class ESCManager : MonoBehaviour
{
    private float currentVolume;

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}