using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
      NextLevel.LoadLevel();
    }
  }
}