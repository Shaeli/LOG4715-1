using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporters : MonoBehaviour {


	[SerializeField] Vector3 initialPosition;


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.transform.position = initialPosition;
		}
	}
}
