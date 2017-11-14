using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpAffichage : MonoBehaviour {

    [SerializeField]
    int CoolDownAffichage = 10;


    int CoolDownAffichagetmp;


    public GameObject[] images;
	// Use this for initialization
	void Start () {
       foreach(GameObject image in images)
        {
            image.SetActive(false);

        }
	}
	
	// Update is called once per frame
	void Update () {
        CheckPopUp();
        if(CoolDownAffichagetmp == 1)
        {
            foreach (GameObject image in images)
            {
                image.SetActive(false);

            }
        }
        if (CoolDownAffichagetmp!=0)
        {
            CoolDownAffichagetmp--;
        }

    }

    void CheckPopUp()
    {
        float dh = Input.GetAxis("DHorizontal");
        float dv = Input.GetAxis("DVertical");

        Debug.Log("dh: " + dh);
        Debug.Log("dv: " + dv);

        if(dv > 0 && CoolDownAffichagetmp==0)
        {
         
               images[0].SetActive(true);
               CoolDownAffichagetmp = CoolDownAffichage;

            
            
        }
        else if(dv < 0 && CoolDownAffichagetmp == 0)
        {
                images[1].SetActive(true);
                CoolDownAffichagetmp = CoolDownAffichage;

        }
        else if (dh > 0 && CoolDownAffichagetmp == 0)
        {
            images[2].SetActive(true);
            CoolDownAffichagetmp = CoolDownAffichage;

        }
        else if (dh < 0 && CoolDownAffichagetmp == 0)
        {
            images[3].SetActive(true);
            CoolDownAffichagetmp = CoolDownAffichage;

        }
    }
}
