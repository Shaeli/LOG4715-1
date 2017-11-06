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

    [SerializeField] GameObject TPGhost;

    bool coolDown;

    bool buttonDown = false;

    bool rotateOnce = false;

    //Distance téléportation
    [SerializeField] float TeleportationDistance = 5f;

    float Refill;

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        TPGhost.SetActive(false);
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
            DisplayTpGhost();

            if (Input.GetButtonUp("Ability"))
            {
                transform.position += new Vector3(0, 0, transform.forward.z * TeleportationDistance);
                FloatingImagefill.GetComponent<Image>().fillAmount = 0;
                coolDown = true;
                TPGhost.SetActive(false);
                buttonDown = false;
                rotateOnce = false;
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

    void DisplayTpGhost()
    {
        if (Input.GetButton("Ability"))
        {
            TPGhost.transform.position = transform.position + new Vector3(0, 0, transform.forward.z * TeleportationDistance);
            TPGhost.SetActive(true);
        }

    }
}
