using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    //[SerializeField] private ESCManager manager;
    public void LoadScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("loadingLevel", 1));
        //PlayerPrefs.GetFloat("Volume");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}