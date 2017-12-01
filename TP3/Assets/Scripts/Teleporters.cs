using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporters : MonoBehaviour {

    [SerializeField] AudioClip teleportSound;
	[SerializeField] Vector3 initialPosition;


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
            AudioSource.PlayClipAtPoint(teleportSound, transform.position);
            other.transform.position = initialPosition;
		}
	}
}
