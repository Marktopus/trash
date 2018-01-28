using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLogic : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
  {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
  {
	}

  void OnCollisionEnter2D(Collision2D collision)
  {
    //Debug.Log("COLLIDED YAY");
    ContactPoint2D[] contacts = { };
    gameObject.GetComponent<Collider2D>().GetContacts(contacts);
  }

  void OnCollisionStay2D(Collision2D collision) 
  {
    //Debug.Log("COLLIDE STAY");
  }
} 