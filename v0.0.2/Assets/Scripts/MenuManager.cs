using System.Security;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]private GameObject EpisodeSelector;
    [SerializeField]private GameObject ShowMainMenu;
    [SerializeField]private GameObject ShowOptions;
    public void NewGame()
    {
        ShowMainMenu.SetActive(false);
        EpisodeSelector.SetActive(true);
    }

    public void Cancel()
    {
        EpisodeSelector.SetActive(false);
        ShowMainMenu.SetActive(true);
    }

    public void OpenOptions()
    {
        ShowMainMenu.SetActive(false);
        ShowOptions.SetActive(true);
    }

    public void CloseOptions()
    {
        ShowOptions.SetActive(false);
        ShowMainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Saiu do jogo.");
        Application.Quit();
    }
}
