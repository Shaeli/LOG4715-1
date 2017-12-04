using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour {

    [SerializeField] GameObject ShieldPillPrefab;
    [SerializeField] float DelayBetweenGeneration;

    public bool Active = true;
    public int MaxCount = 2;
    private int count = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(GeneratePill());
	}

    IEnumerator GeneratePill()
    {
        while (true)
        {
            if (Active && count < MaxCount)
            {
                Instantiate(ShieldPillPrefab, transform.position + Vector3.up, Quaternion.identity, this.transform);
                Active = false;
                count++;
            }
            yield return new WaitForSeconds(DelayBetweenGeneration);
        }
    }
}
