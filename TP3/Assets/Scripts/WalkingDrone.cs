using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingDrone : MonoBehaviour {

    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform leftExtremety;
    [SerializeField] private Transform rightExtremety;
    private static Animator anim;
    private static Rigidbody rb;
    private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);

    private float extremityPosition;
    private bool flipped;

    public Transform player { get; set; }

    // Use this for initialization
    void Start ()
    {
        flipped = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        float platformLength = transform.parent.transform.localScale.z;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (transform.parent.GetComponent<EnemyPlatform>().playerArrived)
            {
                FlipBeforeAttack();
            }
            else
            {
                FlipCharacter();
            }

            Walk();
        }
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Attack();
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Attack();
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", true);
        }
    }

    void Walk()
    {
        anim.SetBool("isWalking", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) // Only move after the attack animation has finished playing
        {
            float step = speed * Time.deltaTime;
            if (transform.forward.z > 0)
                transform.position = Vector3.MoveTowards(transform.position, rightExtremety.position, step);
            else
                transform.position = Vector3.MoveTowards(transform.position, leftExtremety.position, step);

            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed * transform.forward.z);
        }
    }

    void Attack()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walking")) // Stop moving when the walking animation has finished playing
        { 
            rb.velocity = new Vector3(0, 0, 0); // Stop moving 
        }
    }

    // Gère l'orientation du joueur et les ajustements de la camera
    //         extremityPosition = platformLength / 2f * transform.localScale.z - transform.localScale.z / 2f;

    void FlipCharacter()
    {
        if (transform.localPosition.z > rightExtremety.localPosition.z - transform.localScale.z / 2f && !flipped)
        {
            flipped = true;
            transform.Rotate(FlipRotation);
        }
        else if (transform.localPosition.z < leftExtremety.localPosition.z + transform.localScale.z / 2f && flipped) 
        {
            flipped = false;
            transform.Rotate(-FlipRotation);
        }
    }

    void FlipBeforeAttack()
    {
        if (player.position.z - transform.position.z < 0 && !flipped) // Player on left side but looking at right
        {
            flipped = true;
            transform.Rotate(FlipRotation);
        }
        else if (player.position.z - transform.position.z > 0 && flipped) // Player on right side but looking at left
        {
            flipped = false;
            transform.Rotate(-FlipRotation);
        }
    }
}
