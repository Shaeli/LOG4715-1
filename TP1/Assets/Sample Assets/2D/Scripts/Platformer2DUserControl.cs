using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour 
{
	private PlatformerCharacter2D character;
    private bool jump, crouch, jumpUp;


	void Awake()
	{
		character = GetComponent<PlatformerCharacter2D>();
	}

    void Update ()
    {
        // Read the jump down/up and crouch input in Update so button presses aren't missed.
#if CROSS_PLATFORM_INPUT
        if (CrossPlatformInput.GetButtonDown("Jump")) jump = true;
        if (CrossPlatformInput.GetButton("Crouch")) crouch = true;
        if (CrossPlatformInput.GetButtonUp("Jump")) jumpUp = true;

#else
		if (Input.GetButtonDown("Jump")) jump = true;
        if (Input.GetButton("Crouch")) crouch = true;
        if (Input.GetButtonUp("Jump")) jumpUp = true;
#endif

    }

	void FixedUpdate()
	{
        // Read the inputs.
		#if CROSS_PLATFORM_INPUT
		float h = CrossPlatformInput.GetAxis("Horizontal");
		#else
		float h = Input.GetAxis("Horizontal");
		#endif

		// Pass all parameters to the character control script.
		character.Move( h, crouch , jump, jumpUp );

        // Reset the jump input once it has been used.
	    jump = false;
        jumpUp = false;
        crouch = false;
	}
}
