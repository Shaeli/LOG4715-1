using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingDrone : MonoBehaviour {

    [SerializeField] private Transform player;
    [SerializeField] private float speed = 1f;
    private static Animator anim;
    private static Rigidbody rb;
    private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);

    private float extremityPosition;
    private bool flipped;

    // Use this for initialization
    void Start ()
    {
        flipped = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        float platformLength = transform.parent.transform.localScale.z;
        extremityPosition = platformLength / 2f * transform.localScale.z - transform.localScale.z / 2f;
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

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Attack();
        }
    }

    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Attack();
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", true);
        }
    }

    void Walk()
    {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isWalking", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) // Only move after the attack animation has finished playing
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed * transform.forward.z);
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
    void FlipCharacter()
    {
        if (transform.localPosition.z > extremityPosition && !flipped)
        {
            flipped = true;
            transform.Rotate(FlipRotation);
        }
        else if (transform.localPosition.z < -extremityPosition && flipped) 
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
