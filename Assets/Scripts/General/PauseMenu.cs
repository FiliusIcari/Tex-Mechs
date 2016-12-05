using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject PauseUI;

	private bool paused = false;

	void Start()
	{
		PauseUI.SetActive (false);
	}

	void Update()
	{
		if(Input.GetButtonDown("Pause"))
		{
			paused = !paused;
		}

		if (paused) 
		{
			PauseUI.SetActive (true);
			Time.timeScale = 0;
		}
		if (!paused) 
		{
			PauseUI.SetActive (false);
			Time.timeScale = 1;
		}
	}

	public void Resume ()
	{
		paused = false;
	}

	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene (0);
	}

	public void Quit ()
	{
		Application.Quit();
	}

}
