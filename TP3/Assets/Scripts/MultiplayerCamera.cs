using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerCamera : MonoBehaviour {

    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float zoomFactor;
    [SerializeField] float verticalOffset;

    private float initialZoom;
    private float initialDistance;

	// Use this for initialization
	void Start () {
        Vector3 middle;
        if (player2 != null)
        {
            middle = (player1.position + player2.position) * 0.5f;
        }
        else
        {
            middle = player1.position;
        }

        initialZoom = (middle - transform.position).magnitude;

    }
	
	// Update is called once per frame
	void Update () {
        FollowPlayers();
	}

    void FollowPlayers()
    {
        Vector3 middle;
        float zoomDistance;
        if (player2 != null)
        {
            // Middle point 
            middle = (player1.position + player2.position) * 0.5f;

            // To calculate the zoom of the camera
            zoomDistance = (player1.position - player2.position).magnitude * zoomFactor;
        }
        else
        {
            middle = player1.position;
            zoomDistance = zoomFactor;
        }

        // Move camera a certain distance
        Vector3 cameraDestination;

        if (zoomDistance > initialZoom)
            cameraDestination = middle - transform.forward * zoomDistance;
        else
            cameraDestination = middle - transform.forward * initialZoom;

        cameraDestination.y += verticalOffset;
        transform.position = Vector3.Slerp(transform.position, cameraDestination, Time.deltaTime);

    }
}
