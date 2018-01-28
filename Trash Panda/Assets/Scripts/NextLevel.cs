using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour 
{
  public static int lives = 3;
	public static void LoadLevel() 
  {
    lives = 3;
		var currScene = SceneManager.GetActiveScene();
		if(currScene.buildIndex < SceneManager.sceneCountInBuildSettings - 1) 
    {
			SceneManager.LoadScene(currScene.buildIndex + 1);
			
			/// 5 is the ending scene
			if(currScene.buildIndex != 5) 
			{
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
				SceneManager.SetActiveScene(currScene);
			}
			
			if(currScene.name == "End" || currScene.name == "LostScene")
			{
        LifePosition.canvas.SetActive(false);
				//var uiObjects = GameObject.FindGameObjectsWithTag("UI");
				//for(var i = 0; i < uiObjects.Length; i++)
				//{
				//	Destroy(uiObjects[i]);
				//}
			}
		}
	}

	public void LoadFirstLevel() 
  {
		var currScene = SceneManager.GetActiveScene();
		if(currScene.buildIndex < SceneManager.sceneCountInBuildSettings - 1) 
    {
      if(LifePosition.canvas)
      {
        LifePosition.canvas.SetActive(true);
      }
      lives = 3;
			SceneManager.LoadScene(currScene.buildIndex + 1);
			SceneManager.LoadScene("UI", LoadSceneMode.Additive);
			SceneManager.SetActiveScene(currScene);
		}
	}

	public void LoadMenu() 
  {
    if(LifePosition.canvas)
    {
      LifePosition.canvas.SetActive(false);
    }
		SceneManager.LoadScene(0);
	}

	public static void LoadLosingSceen() 
  {
    --lives;
		var currScene = SceneManager.GetActiveScene();		
    if(lives > 0)
    {

      SceneManager.LoadScene(currScene.buildIndex);
			SceneManager.LoadScene("UI", LoadSceneMode.Additive);
			SceneManager.SetActiveScene(currScene);
    }
    else 
    {
      LifePosition.canvas.SetActive(false);
      SceneManager.LoadScene("LostScene");
    }
	}

	public void QuitGame() 
  {
		Application.Quit();
	}
}
