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
    public bool wallJump { get; set; }

    bool coolDown;
    void Start () {

        wallCheck = transform.Find("WallCheck");
        coolDown = false;
        sided = false;
        stuck = false;
        wallJump = false;
        currentGlueTime = GlueTime;
        GlueImageFill.fillAmount = 1;
    }
	
	// Update is called once per frame
	void Update () {

        CheckSautMural();
        CheckSided();
        GlueCoolDown();
        CheckJump();
        CheckWallGlue();
        
    }

    public void CheckSautMural()
    {
        if (wallJump)
        {
            if (gameObject.GetComponent<PlayerControler>()._Flipped)
            {

                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                GetComponent<Rigidbody>().AddForce((new Vector3(0f, 2f, 2f) * jumpForce) / 1.5f);
                gameObject.GetComponent<PlayerControler>().FlipCharacter(1);

            }
            else if (!gameObject.GetComponent<PlayerControler>()._Flipped)
            {

                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                GetComponent<Rigidbody>().AddForce((new Vector3(0f, 2f, -2f) * jumpForce) / 1.5f);
                gameObject.GetComponent<PlayerControler>().FlipCharacter(-1);
            }
            wallJump = false;
            stuck = false;
            gameObject.GetComponent<PlayerControler>().decompte = 15;
        }
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
        if(sided && !coolDown)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            currentGlueTime -= Time.deltaTime;
            GlueImageFill.GetComponent<Image>().fillAmount = currentGlueTime / GlueTime;
            if (currentGlueTime <= 0)
            {
                coolDown = true;
                stuck = false;
            }
            stuck = true;

        }
       else if (currentGlueTime < GlueTime)
        {
            stuck = false;
            coolDown = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
    public void CheckJump()
    {
        if (stuck && Input.GetButton("Jump"))
        {
            wallJump = true;
        }
    }

}
