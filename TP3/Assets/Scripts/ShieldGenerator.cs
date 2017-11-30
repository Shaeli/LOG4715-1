using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour {

    [SerializeField] GameObject ShieldPillPrefab;
    [SerializeField] float DelayBetweenGeneration;

    public bool Active = true;

	// Use this for initialization
	void Start () {
        StartCoroutine(GeneratePill());
	}

    IEnumerator GeneratePill()
    {
        while (true)
        {
            if (Active)
            {
                Instantiate(ShieldPillPrefab, transform.position + Vector3.up, Quaternion.identity, this.transform);
                Active = false;
            }
            yield return new WaitForSeconds(DelayBetweenGeneration);
        }
    }
}
