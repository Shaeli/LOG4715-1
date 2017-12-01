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
    [SerializeField] AudioClip TPSound;
    [SerializeField] GameObject TPGhost;
    [SerializeField] GameObject TPGhostR;
    [SerializeField] LayerMask myLayerMask ;

    bool coolDown;
    int playerNum = 0;

    Vector3 rayCastStartPoint;

    //Distance téléportation
    [SerializeField] float TeleportationDistance = 5f;

    float Refill;

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        TPGhost.SetActive(false);
        TPGhostR.SetActive(false);
        coolDown = false;
        FloatingImagefill.fillAmount = 1;
        playerNum = GetComponent<Multiplayer>().PlayerNumber;
    }

    // Vérifie les entrées de commandes du joueur
    void Update()
    {
        rayCastStartPoint = new Vector3(transform.position.x, (transform.position.y + transform.localScale.y / 2), transform.position.z);
        TeleportationCoolDown();
        CheckTeleportation();
    }

    void CheckTeleportation()
    {
      if (!coolDown)
      {
            DisplayTpGhost();
            if (!Physics.Raycast(rayCastStartPoint, new Vector3(0, 0, transform.forward.z), TeleportationDistance , myLayerMask))
            {
                
                if (Input.GetButtonUp("Ability" + playerNum))
                {
                    transform.position += new Vector3(0, 0, transform.forward.z * TeleportationDistance);
                    AudioSource.PlayClipAtPoint(TPSound, transform.position);
                    FloatingImagefill.GetComponent<Image>().fillAmount = 0;
                    coolDown = true;
                    TPGhost.SetActive(false);

                }
            }
            else if (Input.GetButtonUp("Ability" + playerNum))
            {
              TPGhostR.SetActive(false);
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
        if (Input.GetButton("Ability" + playerNum) && !Physics.Raycast(rayCastStartPoint, new Vector3(0, 0, transform.forward.z), TeleportationDistance, myLayerMask))
        {
            TPGhost.transform.position = transform.position + new Vector3(0, 0, transform.forward.z * TeleportationDistance);
            TPGhost.SetActive(true);
            TPGhostR.SetActive(false);
        }
        else if (Input.GetButton("Ability" + playerNum) && Physics.Raycast(rayCastStartPoint, new Vector3(0, 0, transform.forward.z), TeleportationDistance, myLayerMask))
        {
            TPGhostR.transform.position = transform.position + new Vector3(0, 0, transform.forward.z * TeleportationDistance);
            TPGhost.SetActive(false);
            TPGhostR.SetActive(true);
        }

    }
}
