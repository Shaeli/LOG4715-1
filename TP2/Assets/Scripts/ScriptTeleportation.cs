using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptTeleportation : MonoBehaviour
{
    //Cooldown téléportation; temps de reference entre deux teleportations
    [SerializeField] float timecd = 3.0f;
    //cooldown en cours
    [SerializeField] float RefillSpeed = 1f;
    [SerializeField] Image FloatingImagefill;

    [SerializeField] GameObject showTPRight;
    [SerializeField] GameObject showTPLeft;
    bool coolDown;
    //Distance téléportation
    [SerializeField]
    float TeleportationDistance = 5f;

    float Refill;

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        showTPRight.SetActive(false);
        showTPLeft.SetActive(false);
        coolDown = false;
        FloatingImagefill.fillAmount = 1;
    }

    // Vérifie les entrées de commandes du joueur
    void Update()
    {
        TeleportationCoolDown();
        CheckTeleportation();

    }

    void CheckTeleportation()
    {
      if (!coolDown)
      {
        if (Input.GetButtonDown("Teleportation"))
        {
          if (gameObject.GetComponent<PlayerControler>()._Flipped)
          {
            showTPLeft.transform.position += new Vector3(-TeleportationDistance * 50, 0, 0);
            showTPLeft.SetActive(true);
          }
          else
          {
            showTPRight.transform.position += new Vector3(TeleportationDistance * 50, 0, 0);
            showTPRight.SetActive(true);
          }
        }
        if (Input.GetButtonUp("Teleportation"))
        {
          if (gameObject.GetComponent<PlayerControler>()._Flipped)
          {
            transform.position += new Vector3(0, 0, -TeleportationDistance);
            FloatingImagefill.GetComponent<Image>().fillAmount = 0;
            coolDown = true;
            showTPLeft.SetActive(false);
            showTPRight.SetActive(false);
          }
          else
          {
            transform.position += new Vector3(0, 0, TeleportationDistance);
            FloatingImagefill.GetComponent<Image>().fillAmount = 0;
            coolDown = true;
            showTPRight.SetActive(false);
            showTPLeft.SetActive(false);
          }
        }
      }
    }

    void TeleportationCoolDown()
    {
        if (coolDown)
        {
            Refill += Time.deltaTime * RefillSpeed;
            FloatingImagefill.GetComponent<Image>().fillAmount = Refill / timecd;
            if (FloatingImagefill.fillAmount == 1)
            {
                coolDown = false;
                Refill = 0;
            }
        }

    }
}
