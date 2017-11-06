using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shield : MonoBehaviour {

    [SerializeField]
    Image ShieldBar;

  public int timeShield = 100;

  private int activeShield;

  private bool hasShield = false;

  private bool deleteShield = false;

  public bool shieldIsUp {get ; set;}

  public GameObject shield;

  void Start() {
    shieldIsUp = false;
    shield.SetActive(false);

  }

  void Update() {
        if (activeShield == 0) {
            shield.SetActive(false);
            shieldIsUp = false;
        }
        if (activeShield != 0) {
            activeShield--;
        }
        if (hasShield == true) {
            if (Input.GetButtonDown("Ability") && activeShield == 0) {
            shield.SetActive(true);
            shieldIsUp = true;
            activeShield = timeShield;
            }

        }
        if (activeShield == 1) {
            hasShield = false;
        }
        if (ShieldBar != null)
        {
            ShieldBar.fillAmount = (float)activeShield / timeShield;
        }
  }

  private void OnTriggerEnter(Collider other)
  {
     if (other.gameObject.CompareTag("Shield"))
     {
       other.gameObject.SetActive(false);
       hasShield = true;
     }
   }

}
