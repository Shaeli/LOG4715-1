using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shield : MonoBehaviour {

    [SerializeField]
    Image ShieldBar;
    [SerializeField]
    Image ShieldIcon;

    private ShieldGenerator Generator;

  public int timeShield = 100;

  private int activeShield;

  private bool hasShield = false;

  public bool shieldIsUp {get ; set;}

  public GameObject shield;

  void Start() {
        shieldIsUp = false;
        shield.SetActive(false);
        if (ShieldIcon != null)
            ShieldIcon.enabled = false;
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
            if (Input.GetButtonDown("Shield" + GetComponent<Multiplayer>().PlayerNumber) && activeShield == 0) {
                shield.SetActive(true);
                shieldIsUp = true;
                activeShield = timeShield;
                if (Generator != null)
                {
                    Generator.Active = true;
                    Generator = null;
                }
                if (ShieldIcon != null)
                    ShieldIcon.enabled = false;
            }

        }
        if (activeShield == 1) {
            hasShield = false;
            if (ShieldIcon != null)
                ShieldIcon.enabled = false;
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
            Generator = other.gameObject.transform.parent.gameObject.GetComponent<ShieldGenerator>();
            hasShield = true;
            if (ShieldIcon != null)
                ShieldIcon.enabled = true;
     }
   }

}
