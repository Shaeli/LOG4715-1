using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {

    [SerializeField]
    int StartingHP = 3;

    public int CurrentHP { get { return currentHP; } }

    private int currentHP;

    // Use this for initialization
    void Start () {
        currentHP = StartingHP;
	}

    void OnCollisionEnter(Collision collision)
    {
        DamagingObject damagingObject = collision.gameObject.GetComponent<DamagingObject>();
        if (damagingObject != null)
        {
            GetDamaged(damagingObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        DamagingObject damagingObject = other.gameObject.GetComponent<DamagingObject>();
        if (damagingObject != null)
        {
            GetDamaged(damagingObject);
        }
    }

    private void GetDamaged(DamagingObject damagingObject)
    {
        Debug.Log("Getting damaged!");
        currentHP -= damagingObject.Damage;
    }
}
