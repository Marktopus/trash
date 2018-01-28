using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {
	public static void LoadLevel() {
		var currScene = SceneManager.GetActiveScene();
		if(currScene.buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
			SceneManager.LoadScene(currScene.buildIndex + 1);
		}
	}

	public void LoadFirstLevel() {
		var currScene = SceneManager.GetActiveScene();
		if(currScene.buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
			SceneManager.LoadScene(currScene.buildIndex + 1);
		}
	}

	public void LoadMenu() {
		SceneManager.LoadScene(0);
	}

	public static void LoadLosingSceen() {
		SceneManager.LoadScene("LostScene");
	}

	public void QuitGame() {
		Application.Quit();
	}
}
