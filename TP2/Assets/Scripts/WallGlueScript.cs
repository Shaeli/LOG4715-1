using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WallGlueScript : MonoBehaviour {

    [SerializeField] LayerMask whatIsWall;
    [SerializeField] float GlueTime = 3.0f;
    [SerializeField] float RefillSpeed = 1f;
    [SerializeField] Image GlueImageFill;
    [SerializeField] float jumpForce=4;

    Transform wallCheck;
    float wallRadius = .2f;
    bool sided;
    bool stuck;
    float currentGlueTime;
    public bool jump { get; set; }

    bool coolDown;
    void Start () {

        wallCheck = transform.Find("WallCheck");
        coolDown = false;
        sided = false;
        stuck = false;
        jump = false;
        currentGlueTime = GlueTime;
        GlueImageFill.fillAmount = 1;
    }
	
	// Update is called once per frame
	void Update () {

        if (jump)
        {
            if (gameObject.GetComponent<PlayerControler>()._Flipped)
            {
                
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                GetComponent<Rigidbody>().AddForce((new Vector3(0f, 1f, 2f) * jumpForce) / 1.5f);
                gameObject.GetComponent<PlayerControler>().FlipCharacter(1);

            }
            else if (!gameObject.GetComponent<PlayerControler>()._Flipped)
            {

                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                GetComponent<Rigidbody>().AddForce((new Vector3(0f, 1f, -2f) * jumpForce) / 1.5f);
                gameObject.GetComponent<PlayerControler>().FlipCharacter(-1);
            }
            jump = false;
            gameObject.GetComponent<PlayerControler>().decompte = 6;
        }
        CheckSided();
        GlueCoolDown();
        CheckWallGlue();
        CheckJump();
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
                stuck = false;
            }
        }
    }
    public void CheckWallGlue()
    {
        if(Input.GetButton("Glue") && sided && !coolDown && !jump)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            currentGlueTime -= Time.deltaTime;
            GlueImageFill.GetComponent<Image>().fillAmount = currentGlueTime / GlueTime;
            if (currentGlueTime <= 0)
            {
                coolDown = true;
            }
            stuck = true;

        }
       else if (currentGlueTime < GlueTime)
        {
            stuck = true;
            coolDown = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
    public void CheckJump()
    {
        if (stuck && Input.GetButton("Jump"))
        {
            jump = true;
          /*  if (gameObject.GetComponent<PlayerControler>()._Flipped)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                GetComponent<Rigidbody>().AddForce((new Vector3(1f,1f, 2f) * jumpForce) / 1.5f);
            }
            else if (!gameObject.GetComponent<PlayerControler>()._Flipped)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                GetComponent<Rigidbody>().AddForce((new Vector3(1f, 1f, 2f) * -jumpForce) / 1.5f);
            }*/
        }
    }
}
