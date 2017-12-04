using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    // Déclaration des constantes
    private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);
    private static readonly Vector3 CameraPosition = new Vector3(10, 2, 0);
    private static readonly Vector3 InverseCameraPosition = new Vector3(-10, 2, 0);

    // Déclaration des variables
    public bool _Grounded { get; set; }
    public bool _Floor { get; set; }
    public bool _Player { get; set; }
    public bool _Flipped { get; set; }
    Animator _Anim { get; set; }
    Rigidbody _Rb { get; set; }
    Camera _MainCamera { get; set; }
    public int decompte { get; set; }
    public GameObject FlipInvariantObjects;

    public Text GameStateText;

    private bool reloadOnce = false;

    private int playerNum;

    // Valeurs exposées
    [SerializeField]
    float MoveSpeed = 5.0f;

    [SerializeField]
    AudioClip LoseSound;

    [SerializeField]
    float JumpForce = 10f;

    [SerializeField]
    LayerMask WhatIsGround;

    [SerializeField]
    float PiledJumpMultiplier = 1.5f;
	
	[SerializeField]
    float LowerBound = -50f;

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Rb = GetComponent<Rigidbody>();
        _MainCamera = Camera.main;
        playerNum = GetComponent<Multiplayer>().PlayerNumber;
    }

    // Utile pour régler des valeurs aux objets
    void Start()
    {
        _Grounded = false;
        _Flipped = false;
        // Check pour PiledJumpMultiplier nul ou negatif 
        if (PiledJumpMultiplier <= 0)
            PiledJumpMultiplier = 1; // Forcer la valeur a 1
    }


    // Vérifie les entrées de commandes du joueur
    void Update()
    {
        if (decompte == 0)
        {
            var horizontal = Input.GetAxis("Horizontal" + playerNum) * MoveSpeed;
            HorizontalMove(horizontal);
            FlipCharacter(horizontal);
        }
        else
        {
            decompte--;
        }
      
        CheckJump();
		
		if( !reloadOnce && transform.position.y < LowerBound) {
			reloadOnce = true;
            if (GameStateText != null)
            {
                GameStateText.text = "Game Over!";
                AudioSource.PlayClipAtPoint(LoseSound, transform.position);

            }
            Invoke("ReloadLevel", 3f);
		}
    }

    // Gère le mouvement horizontal
    void HorizontalMove(float horizontal)
    {
        _Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y, horizontal);
        _Anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
    }

    // Gère le saut du personnage, ainsi que son animation de saut
    void CheckJump()
    {
        if (_Grounded)
        {
            if (Input.GetButtonDown("Jump" + playerNum))
            {
                // If piled jump, add a force
                if (_Player && !_Floor)
                    _Rb.AddForce(new Vector3(0, JumpForce * PiledJumpMultiplier, 0), ForceMode.Impulse);
                else
                    _Rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);

                _Grounded = false;
                _Anim.SetBool("Grounded", false);
                _Anim.SetBool("Jump", true);
            }
        }
    }

    // Gère l'orientation du joueur et les ajustements de la camera
    public void FlipCharacter(float horizontal)
    {
        if (horizontal < 0 && !_Flipped)
        {
            _Flipped = true;
            transform.Rotate(FlipRotation);
            //_MainCamera.transform.Rotate(-FlipRotation);
            var angles = _MainCamera.transform.eulerAngles;
            angles.y = -angles.y;
            if (_MainCamera.gameObject.GetComponent<MultiplayerCamera>() == null)
            {
                _MainCamera.transform.eulerAngles = angles;
                _MainCamera.transform.localPosition = InverseCameraPosition;
            }

            if(FlipInvariantObjects != null)
            {
                //FlipInvariantObjects.GetComponent<RectTransform>().Rotate(FlipRotation);
                var a = FlipInvariantObjects.GetComponent<RectTransform>().eulerAngles;
                a.y = -a.y;
                FlipInvariantObjects.GetComponent<RectTransform>().eulerAngles = a;
            }
        }
        else if (horizontal > 0 && _Flipped)
        {
            _Flipped = false;
            transform.Rotate(-FlipRotation);
            //_MainCamera.transform.Rotate(FlipRotation);
            var angles = _MainCamera.transform.eulerAngles;
            angles.y = -angles.y;
            if (_MainCamera.gameObject.GetComponent<MultiplayerCamera>() == null)
            {
                _MainCamera.transform.eulerAngles = angles;
                _MainCamera.transform.localPosition = CameraPosition;
            }
            if (FlipInvariantObjects != null)
            {
                //FlipInvariantObjects.GetComponent<RectTransform>().Rotate(-FlipRotation);
                var a = FlipInvariantObjects.GetComponent<RectTransform>().eulerAngles;
                a.y = -a.y;
                FlipInvariantObjects.GetComponent<RectTransform>().eulerAngles = a;
            }
        }
    }

    // Collision avec le sol
    void OnCollisionEnter(Collision coll)
    {
        // On s'assure de bien être en contact avec le sol ou le player
        if ((WhatIsGround & (1 << coll.gameObject.layer)) == 0)
            return;
        else
        {
            _Grounded = true;
            _Anim.SetBool("Grounded", _Grounded);
        }

        // On verifie si le joueur est en contact avec un autre joueur ou pas (pour saut empile)
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
            _Player = true;

        // Ajouter le Character au plateforme mouvante
        if (coll.gameObject.layer == LayerMask.NameToLayer("MovingPlatform"))
        {
            transform.parent.parent = coll.transform;
        }

        // Évite une collision avec le plafond
        if (coll.relativeVelocity.y > 0)
        {
            _Grounded = true;
            _Anim.SetBool("Grounded", _Grounded);
        }
    }

    private void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Floor"))
            _Floor = true;
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("MovingPlatform"))
        {
            transform.parent.parent = null;
        }

        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
            _Player = false;

        if (coll.gameObject.layer == LayerMask.NameToLayer("Floor"))
            _Floor = false;
    }
	
	void ReloadLevel()
    {
        Scene loadedLevel = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (loadedLevel.buildIndex);
    }
}
