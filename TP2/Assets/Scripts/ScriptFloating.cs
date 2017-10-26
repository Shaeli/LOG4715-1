using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptFloating : MonoBehaviour {

    // Cool down du Flottaison
    [SerializeField] float CoolDownFloat = 3.0f;
    [SerializeField] Image CoolDownFloatImage;
    [SerializeField] Image CoolDownFloatfill;

    // Temps que le joueur peut rester dans l'air
    [SerializeField] float FloatingTime = 3.0f;
    [SerializeField] Image FloatingTimeImage;
    [SerializeField] Image FloatingImagefill;

    float currentFloatingTime;
    float currentCoolDownTime;

    // Boolean pour activer le cooldown
    bool coolDown;

    // Use this for initialization
    void Start () {
        coolDown = false;
        FloatingImagefill.fillAmount = 1;
        CoolDownFloatfill.fillAmount = 0;
        currentFloatingTime = FloatingTime;
        currentCoolDownTime = CoolDownFloat;
    }
	
	// Update is called once per frame
	void Update () {

        // Verifier si le joueur peut flotter
        FloatCoolDown();

        // Laisser flotter le joueur s'il appuie sur la touche pour flotter
        Float();
    }

    void Float()
    {
        if (Input.GetButton("Float") && !coolDown)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Floating! Fill: " + FloatingTimeImage.GetComponent<Image>().fillAmount);
            currentFloatingTime -= Time.deltaTime;
            FloatingImagefill.GetComponent<Image>().fillAmount = 1f - currentFloatingTime / FloatingTime;
            // Consider player as floor when he is floating
            gameObject.layer = LayerMask.NameToLayer("Floor");

            // Dseactiver la flottaison jusqu'a la fin du cooldown
            if (currentFloatingTime <= 0)
            {
                Debug.Log("Finished 3s of floating!");
                coolDown = true;
                //gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation; // opt2
                //currentFloatingTime = 3.0f; // opt2
                //FloatingImagefill.GetComponent<Image>().fillAmount = 1.0f; // opt2
            } 
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            currentFloatingTime = 3.0f;
            FloatingImagefill.GetComponent<Image>().fillAmount = 1.0f;
        } // opt2

        /*else if (currentFloatingTime < FloatingTime)
        {
            coolDown = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            // Set layer back to normal
            gameObject.layer = LayerMask.NameToLayer("Default");
        }*/ //opt 1
    }

    void FloatCoolDown()
    {
        if (coolDown)
        {
            Debug.Log("Cooling down! Fill: " + CoolDownFloatfill.GetComponent<Image>().fillAmount); // opt2
            currentCoolDownTime -= Time.deltaTime; // opt2
            CoolDownFloatfill.GetComponent<Image>().fillAmount = currentCoolDownTime / CoolDownFloat; // opt2

            //currentFloatingTime += Time.deltaTime; // opt1
            //FloatingImagefill.GetComponent<Image>().fillAmount = 1f - currentFloatingTime / FloatingTime; // opt1


            // Retour à la normale // op2
            if (currentCoolDownTime <= 0)
            {
                coolDown = false;
                Debug.Log("Cooling down done!");
                currentCoolDownTime = 3.0f;
                CoolDownFloatfill.GetComponent<Image>().fillAmount = 0.0f; 
            }

            /*if (currentFloatingTime >= FloatingTime)
            {
                coolDown = false;
            }*/ //opt 1
        }

    }
}
