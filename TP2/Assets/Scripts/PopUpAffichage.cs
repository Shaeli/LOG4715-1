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
        if(Input.GetButtonDown("FlecheHaut") && CoolDownAffichagetmp==0)
        {
            if(gameObject.GetComponent<PlayerControler>()._Flipped)
            {
                images[0].SetActive(true);
                CoolDownAffichagetmp = CoolDownAffichage;
            }
            else
            {
               images[1].SetActive(true);
               CoolDownAffichagetmp = CoolDownAffichage;
            }
            
        }
        if(Input.GetButtonDown("FlecheBas") && CoolDownAffichagetmp == 0)
        {
            if (gameObject.GetComponent<PlayerControler>()._Flipped)
            {
                images[2].SetActive(true);
                CoolDownAffichagetmp = CoolDownAffichage;
            }
            else
            {
                images[3].SetActive(true);
                CoolDownAffichagetmp = CoolDownAffichage;
            }
        }
    }
}
