using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingCondition : MonoBehaviour {

	void OnCollision2D(Collider2D collider)
	{
		Debug.Log("HIT");
		if(collider.gameObject.name == "Player")
		{

		}
	}
}
