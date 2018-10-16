using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIScript : MonoBehaviour
{
    public bool Pause;
    public GameObject pUI;
    public GameObject oUI;
    public GameObject gUI;

    private void Start()
    {
        Pause = false;
        pUI.SetActive(false);
        oUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        OnOff();

    }

    void OnOff()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Pause)
            {
                pUI.SetActive(true);
                Pause = true;
                Time.timeScale = 0f;
            }
            else
            {
                pUI.SetActive(false);
                oUI.SetActive(false);
                Pause = false;
                Time.timeScale = 1f;
            }
        }
    }

    public void Restart()
    {
        Debug.Log("Restart!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Options()
    {
        Debug.Log("Options!");
        oUI.SetActive(true);
        pUI.SetActive(false);
    }
    public void Exit()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }
    public void Back()
    {
        Debug.Log("Back");
        pUI.SetActive(true);
        oUI.SetActive(false);
    }
}
