using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour {
	public GameObject player;
	private SpriteRenderer sr;
	private float rotationDelay;

	void Awake()
	{
		sr = player.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {

		if(rotationDelay > 0.35 && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
		{
			rotationDelay = 0;
			sr.flipX = !sr.flipX;
		}

		if(Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0)
		{
			transform.eulerAngles = new Vector3(0, 0, -45);
		}
		else if(Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0)
		{
			transform.eulerAngles = new Vector3(0, 0, -135);
		}
		else if(Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0)
		{
			transform.eulerAngles = new Vector3(0, 0, 45);
		}
		else if(Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
		{
			transform.eulerAngles = new Vector3(0, 0, 135);
		}
		else if(Input.GetAxis("Horizontal") > 0)
		{
			transform.eulerAngles = new Vector3(0, 0, -90);
		}
		else if(Input.GetAxis("Horizontal") < 0)
		{
			transform.eulerAngles = new Vector3(0, 0, 90);
		}
		else if(Input.GetAxis("Vertical") > 0)
		{
			transform.eulerAngles = new Vector3(0, 0, 0);
		}
		else if(Input.GetAxis("Vertical") < 0)
		{
			transform.eulerAngles = new Vector3(0, 0, 180);
		}

		rotationDelay += Time.deltaTime;
	}
}
