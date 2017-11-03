using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    // Déclaration des constantes
    private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);
    private static readonly Vector3 CameraPosition = new Vector3(10, 1, 0);
    private static readonly Vector3 InverseCameraPosition = new Vector3(-10, 1, 0);

    // Déclaration des variables
    bool _Grounded { get; set; }
    bool _Floor { get; set; }
    bool _Player { get; set; }
    public bool _Flipped { get; set; }
    Animator _Anim { get; set; }
    Rigidbody _Rb { get; set; }
    Camera _MainCamera { get; set; }
    public int decompte { get; set; }

    // Valeurs exposées
    [SerializeField]
    float MoveSpeed = 5.0f;

    [SerializeField]
    float JumpForce = 10f;

    [SerializeField]
    LayerMask WhatIsGround;

    [SerializeField]
    float PiledJumpMultiplier = 1.5f;

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Rb = GetComponent<Rigidbody>();
        _MainCamera = Camera.main;
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
            var horizontal = Input.GetAxis("Horizontal") * MoveSpeed;
            HorizontalMove(horizontal);
            FlipCharacter(horizontal);
        }
        else
        {
            decompte--;
        }
      
        CheckJump();
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
            if (Input.GetButtonDown("Jump"))
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
            _MainCamera.transform.Rotate(-FlipRotation);
            _MainCamera.transform.localPosition = InverseCameraPosition;
        }
        else if (horizontal > 0 && _Flipped)
        {
            _Flipped = false;
            transform.Rotate(-FlipRotation);
            _MainCamera.transform.Rotate(FlipRotation);
            _MainCamera.transform.localPosition = CameraPosition;
        }
    }

    // Collision avec le sol
    void OnCollisionEnter(Collision coll)
    {
        // On s'assure de bien être en contact avec le sol ou le player
        if ((WhatIsGround & (1 << coll.gameObject.layer)) == 0)
            return;

        // On verifie si le joueur est en contact avec un autre joueur ou pas (pour saut empile)
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
            _Player = true;
        else
            _Player = false;

        if (coll.gameObject.layer == LayerMask.NameToLayer("Floor"))
            _Floor = true;
        else
            _Floor = false;

        // Évite une collision avec le plafond
        if (coll.relativeVelocity.y > 0)
        {
            _Grounded = true;
            _Anim.SetBool("Grounded", _Grounded);
        }
    }
}
