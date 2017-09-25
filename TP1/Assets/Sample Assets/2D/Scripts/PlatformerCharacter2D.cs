using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour 
{
	bool facingRight = true;							// For determining which way the player is currently facing.
	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float jumpForce = 400f;			// Amount of force added when the player 
    [SerializeField] int jumpNumber = 2;                  // Max jumps possible in a row.
    [SerializeField] float maxJumpForce = 1800f;         // Amount max of force added when the player keep the jump input pressed.
    int nb_jump;
    float currentForceJump;
    float timer;                                        //Amount of time key pressed.

    [Range(0, 1)]
	[SerializeField] float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	
	[SerializeField] bool airControl = false;			// Whether or not a player can steer while jumping;
	[SerializeField] LayerMask whatIsGround;            // A mask determining what is ground to the character
    [SerializeField] LayerMask whatIsWall;

    Transform groundCheck;								// A position marking where to check if the player is grounded.
	float groundedRadius = .5f;							// Radius of the overlap circle to determine if grounded
	bool grounded = false;								// Whether or not the player is grounded.

    Transform wallCheck;
    float wallRadius = .2f;
    bool sided = false;
    int cpt;

    public bool Grounded { get { return grounded; }
    }
    Transform ceilingCheck;								// A position marking where to check for ceilings
	float ceilingRadius = .01f;							// Radius of the overlap circle to determine if the player can stand up
	Animator anim;										// Reference to the player's animator component.



    void Awake()    
	{
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
        wallCheck = transform.Find("WallCheck");
        anim = GetComponent<Animator>();
        nb_jump = jumpNumber;
        currentForceJump = jumpForce;
        cpt = 0;
    }


	void FixedUpdate()
	{
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
        sided = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);
        // Set the vertical animation
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
	}


	public void Move(float move, bool crouch, bool jump, bool jumpUp)
	{
        // If crouching, check to see if the character can stand up
        if (!crouch && anim.GetBool("Crouch"))
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if( Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
				crouch = true;
		}

		// Set whether or not the character is crouching in the animator
		anim.SetBool("Crouch", crouch);

		//only control the player if grounded or airControl is turned on
		if(grounded || airControl && cpt==0 )
		{
			// Reduce the speed if crouching by the crouchSpeed multiplier
			move = (crouch ? move * crouchSpeed : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			anim.SetFloat("Speed", Mathf.Abs(move));

			// Move the character
			GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			
			// If the input is moving the player right and the player is facing left...
			if(move > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(move < 0 && facingRight)
				// ... flip the player.
				Flip();
        }

        // Increment jump force while crouch & jump are held
        if (grounded && crouch && CrossPlatformInput.GetButton("Jump") && (currentForceJump < maxJumpForce))
        {   
            currentForceJump += 5f;
        }

        // If the player should jump from ground...
        if (grounded && ((jump && !crouch) || (crouch && jumpUp))) {
            nb_jump = jumpNumber;
            anim.SetBool("Ground", false);
            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, currentForceJump));

            nb_jump--;
            // Reset current jump force
            currentForceJump = jumpForce;
        } 
        if (!grounded && sided && jump)
        {
            cpt = 30;   
            if(facingRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                Flip();
                GetComponent<Rigidbody2D>().AddForce((new Vector2(-1f, 2f) * jumpForce) / 1.5f);
            } else if(!facingRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                Flip(); 
                GetComponent<Rigidbody2D>().AddForce((new Vector2(1f, 2f) * jumpForce) / 1.5f);
            }
        }
        
        if (!grounded && nb_jump > 0 && jump && !sided) {

            nb_jump--;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, currentForceJump));
        }
        if (cpt != 0)
        {
            cpt--;
        }


    }

	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
