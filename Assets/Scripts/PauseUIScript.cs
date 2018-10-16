using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIScript : MonoBehaviour
{
    public bool Pause;
    public GameObject pUI;

    private void Start()
    {
        Pause = false;
        pUI.SetActive(false);
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
    }
    public void Exit()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }
}
