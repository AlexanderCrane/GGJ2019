using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
//        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void unpause()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }

}
