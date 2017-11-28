using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decompose : MonoBehaviour {

    public GameObject Robot;
    public GameObject UnattachedPieces;

    public void DecomposeMe()
    {
        Destroy(Robot);
        UnattachedPieces.SetActive(true);
        //UnattachedPieces.GetComponent<Rigidbody>().useGravity = true;
    }
}
