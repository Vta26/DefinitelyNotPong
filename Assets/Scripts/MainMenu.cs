using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject CurrentMenu;
    public GameObject OtherMenu;
    public IEnumerator SwitchScene()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayGame()
    {
        StartCoroutine(SwitchScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator SwitchMenu()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        OtherMenu.SetActive(true);
        CurrentMenu.SetActive(false);
    }

    public void SwitchScreen()
    {
        StartCoroutine(SwitchMenu());
    }
}
