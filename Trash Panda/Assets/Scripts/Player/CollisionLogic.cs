using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionLogic : MonoBehaviour 
{

  void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.tag == "Enemy")
    {
      NextLevel.LoadLosingSceen();
    }

    if(collision.gameObject.name == "trash1")
    {
      var currScene = SceneManager.GetActiveScene();
      if(currScene.buildIndex + 1 == 5)
      {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UI"));
        var uiObjects = GameObject.FindGameObjectsWithTag("UI");
				for(var i = 0; i < uiObjects.Length; i++)
				{
					Destroy(uiObjects[i]);
				}
        SceneManager.SetActiveScene(currScene);
      }
      NextLevel.LoadLevel();
    }
  }
}