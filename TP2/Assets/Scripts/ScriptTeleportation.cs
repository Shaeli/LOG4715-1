using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTeleportation : MonoBehaviour
{
    //Cooldown téléportation; temps de reference entre deux teleportations
    [SerializeField] float timecd = 3.0f;
    //cooldown en cours
    [SerializeField] float RefillSpeed = 1f;
    [SerializeField] Image FloatingImagefill;
    bool coolDown;
    //Distance téléportation
    [SerializeField]
    float TeleportationDistance = 5f;

    float Refill;

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
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
        if (Input.GetButtonDown("Teleportation") && !coolDown)
        {
            if (gameObject.GetComponent<PlayerControler>()._Flipped)
            {
                transform.position += new Vector3(0, 0, -TeleportationDistance);
                FloatingImagefill.GetComponent<Image>().fillAmount = 0;
                coolDown = true;
            }
            else
            {
                transform.position += new Vector3(0, 0, TeleportationDistance);
                FloatingImagefill.GetComponent<Image>().fillAmount = 0;
                coolDown = true;
            }
        }
    }

    void TeleportationCoolDown()
    {
        if (coolDown)
        {
            print("oui");
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
