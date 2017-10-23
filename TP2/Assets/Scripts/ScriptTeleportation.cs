using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTeleportation : MonoBehaviour
{
    //Cooldown téléportation; temps de reference entre deux teleportations
    [SerializeField]
    int CoolDownTeleportation = 60;
    //cooldown en cours
    int CoolDownTeleportationTmp;

    //Distance téléportation
    [SerializeField]
    float TeleportationDistance = 5f;

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        CoolDownTeleportationTmp = 0;
    }

    // Vérifie les entrées de commandes du joueur
    void Update()
    {
        CheckTeleportation();
        if (CoolDownTeleportationTmp != 0)
        {
            CoolDownTeleportationTmp--;
        }
    }

    void CheckTeleportation()
    {
            if (Input.GetButtonDown("Teleportation") && CoolDownTeleportationTmp==0)
            {
                if (gameObject.GetComponent<PlayerControler>()._Flipped)
                {
                    transform.position += new Vector3(0, 0, -TeleportationDistance);
                     CoolDownTeleportationTmp = CoolDownTeleportation;
                }
                else
                {
                   transform.position += new Vector3(0, 0, TeleportationDistance);
                   CoolDownTeleportationTmp = CoolDownTeleportation;
                }
            }
    }

}
