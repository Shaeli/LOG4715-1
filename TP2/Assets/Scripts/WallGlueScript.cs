using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WallGlueScript : MonoBehaviour {

    [SerializeField] LayerMask whatIsWall;
    [SerializeField] float GlueTime = 3.0f;
    [SerializeField] float RefillSpeed = 1f;
    [SerializeField] Image GlueImageFill;

    Transform wallCheck;
    float wallRadius = .2f;
    bool sided = false;

    float currentGlueTime;

    bool coolDown;
    void Start () {

        wallCheck = transform.Find("WallCheck");
        coolDown = false;
        currentGlueTime = GlueTime;
        GlueImageFill.fillAmount = 1;
    }
	
	// Update is called once per frame
	void Update () {
        CheckSided();
        GlueCoolDown();
        CheckWallGlue();
    }
    public void CheckSided()
    {
        Collider[] hitColliders = Physics.OverlapSphere(wallCheck.position, wallRadius, whatIsWall);
        if (hitColliders.Length != 0)
        {
            sided = true;
        }
        else
        {
            sided = false;
        }
    }

    public void GlueCoolDown()
    {
        if (coolDown)
        {
            currentGlueTime += Time.deltaTime * RefillSpeed;
            GlueImageFill.GetComponent<Image>().fillAmount = currentGlueTime / GlueTime;

            if (currentGlueTime >= GlueTime)
            {
                coolDown = false;
            }
        }
    }
    public void CheckWallGlue()
    {
        if(Input.GetButton("Glue") && sided && !coolDown)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            currentGlueTime -= Time.deltaTime;
            GlueImageFill.GetComponent<Image>().fillAmount = currentGlueTime / GlueTime;
            if (currentGlueTime <= 0)
            {
                coolDown = true;
            }
        }
        else if (currentGlueTime < GlueTime)
        {
            coolDown = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
