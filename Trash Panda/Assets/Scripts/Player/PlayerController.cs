using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
  public float speed;
  public float maxSpeed;

  public AudioSource pitterPatter;

	// Use this for initialization
	void Start () 
  {
	}
	
	// Update is called once per frame
	void FixedUpdate () 
  {

    float xMov = Input.GetAxisRaw("Horizontal");
    float yMov = Input.GetAxisRaw("Vertical");

    float endSpeed = speed;
    endSpeed += Input.GetAxisRaw("Run") * speed;

    if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
      if(!pitterPatter.isPlaying) {
        pitterPatter.Play();
      }
    }


    Vector2 movement = new Vector2(xMov, yMov);
    float squareLen = movement.SqrMagnitude();
    if(squareLen > 0.01f)
    {
      movement.Normalize();
      gameObject.transform.Translate(movement.x * endSpeed * Time.fixedDeltaTime, movement.y * endSpeed * Time.fixedDeltaTime, 0.0f, Space.World);
    }
	}
}
