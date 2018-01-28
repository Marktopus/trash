using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePosition : MonoBehaviour {
	public int lifeNumber;
  public static GameObject canvas;
  void Start()
  {
    canvas = gameObject.transform.parent.gameObject;
  }

	void Awake()
	{
		UpdateUI();
	}

	public void UpdateUI()
	{
		if(NextLevel.lives < lifeNumber)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
		}
	}
}
