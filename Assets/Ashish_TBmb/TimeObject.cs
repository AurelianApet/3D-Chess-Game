using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour 
{
	public float LocalTime = 1.0f;

	public bool IsTimeWombActive = false;

	void Update () 
	{
		if(IsTimeWombActive)
		{
			//prevent objects from going too fast?
			LocalTime = Mathf.Clamp(LocalTime, 0, 1);

			GetComponent<Rigidbody>().drag = 5 / LocalTime;
			GetComponent<Rigidbody>().angularDrag = 5 / LocalTime;
		}
	}
}
