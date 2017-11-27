using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDrone : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;
    private bool goDown = true;
    private bool goUp = false;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        Patrol();
    }

    void Patrol()
    {
        float step = Time.deltaTime * speed;
        if (transform.position.y > bottom.position.y && goDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, bottom.position + new Vector3(0, -0.1f, 0), step);
            if (transform.position.y < bottom.position.y)
            {
                goUp = true;
                goDown = false;
            }
        } else if (transform.position.y < top.position.y && goUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, top.position + new Vector3(0, 0.1f, 0), step);
            if (transform.position.y > top.position.y)
            {
                goUp = false;
                goDown = true;
            }
        }
        
    }

    
}
