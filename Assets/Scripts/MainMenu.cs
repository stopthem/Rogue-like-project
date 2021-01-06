using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject deletePanel;

    public void StartButton()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void DeleteSave()
    {
        deletePanel.SetActive(true);
    }
    public void ConfirmDelete()
    {
        deletePanel.SetActive(false);
        PlayerPrefs.DeleteKey("ninja");
        PlayerPrefs.DeleteKey("devil");

    }
    public void CancelDelete()
    {
        deletePanel.SetActive(false);
    }
}
