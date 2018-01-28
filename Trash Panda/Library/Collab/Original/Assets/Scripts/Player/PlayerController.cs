using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public float speed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    float xMov = Input.GetAxisRaw("Horizontal");
    Debug.Log(xMov);
    float yMov = Input.GetAxis("Vertical");
    Vector2 movement = new Vector2(xMov, yMov);
    float squareLen = movement.SqrMagnitude();
    if(squareLen > 0.01f)
    {
      movement.Normalize();
      gameObject.GetComponent<Rigidbody2D>().velocity = speed * movement;
    }
    else
    {
      gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    //gameObject.GetComponent<Rigidbody2D>().AddForce(movement);
	}
}
