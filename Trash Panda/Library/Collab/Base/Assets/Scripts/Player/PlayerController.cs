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

    float xMov = Input.GetAxis("Horizontal");
    float yMov = Input.GetAxis("Vertical");
    Vector2 movement = new Vector2(xMov, yMov);
    movement.Normalize();
    movement.Scale(new Vector2(speed, speed));
    gameObject.GetComponent<Rigidbody2D>().velocity = movement;
    //gameObject.GetComponent<Rigidbody2D>().AddForce(movement);
	}
}
