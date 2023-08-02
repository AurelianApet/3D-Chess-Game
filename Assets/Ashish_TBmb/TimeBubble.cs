using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBubble : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{		
		if (!other.GetComponent<TimeObject>())
			other.gameObject.AddComponent<TimeObject>();

		other.GetComponent<TimeObject>().IsTimeWombActive = true;
		other.GetComponent<TimeObject>().LocalTime = 0.5f;
	}

	void OnTriggerStay(Collider other)
	{		
		float dist = Vector3.Distance(transform.position, other.transform.position);

		dist = Mathf.Clamp(dist, 0, 2.5f);

		if (dist <= 2.5)
		{
			other.GetComponent<TimeObject>().LocalTime = 0.5f * dist;
		}
		else
		{
			other.GetComponent<TimeObject>().LocalTime = 1.0f;
		}
	}

	void OnTriggerExit(Collider other)
	{		
		other.GetComponent<Rigidbody>().drag = 0.0f;
		other.GetComponent<Rigidbody>().angularDrag = 0.05f;
		other.GetComponent<TimeObject>().IsTimeWombActive = false;
		other.GetComponent<TimeObject>().LocalTime = 1.0f;
	}


}
