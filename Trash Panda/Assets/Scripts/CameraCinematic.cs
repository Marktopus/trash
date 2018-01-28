using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class CameraCinematic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var cinematic = gameObject.GetComponent<ProCamera2DCinematics>();
		cinematic.Play();
	}
}
