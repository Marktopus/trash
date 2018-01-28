using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killtheui : MonoBehaviour {
  bool doAThing = true;
  void Awake()
  {
    doAThing = true;
  }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if(doAThing)
    {
      if(LifePosition.canvas != null)
      {
        LifePosition.canvas.SetActive(false);
      }
      else
      {
        Debug.Log("well fuck lol");
      }
      doAThing = false;
    }
	}
}
