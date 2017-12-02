using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField] private Transform turret;
    [SerializeField] private AudioClip shootingSound;
    private LineRenderer lr;
    private ParticleSystem ps;
    [SerializeField] private float shootInterval = 3f;
    [SerializeField] private float shootingTime = 2f;
    [SerializeField] private float psTime = 0.5f;
    private bool isShooting;
    private float currentTime;
    private bool isPlayingPs;

	// Use this for initialization
	void Start () {
        isShooting = false;
        isPlayingPs = false;
        currentTime = 0f;
        lr = GetComponent<LineRenderer>();
        ps = GetComponent<ParticleSystem>();
        lr.startWidth = 0.2f;
        lr.endWidth = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {

        checkShootingTime();

        if (isShooting)
        {
            Shoot();
        }
        else if (isPlayingPs)
        {
            PlayParticleSystem();
        }
        else
        {
            Patrol();
        }
	}

    void checkShootingTime()
    {
        currentTime += Time.deltaTime;
        if (!isShooting && !isPlayingPs)
        {
            if (currentTime >= shootInterval)
            {
                currentTime = 0;
                isPlayingPs = true;
            }
        }
        else if (isPlayingPs)
        {
            if (currentTime >= psTime)
            {
                isPlayingPs = false;
                isShooting = true;
            }
        }
        else if (isShooting)
        {
            if (currentTime >= shootingTime)
            {
                currentTime = 0;
                isShooting = false;
            }
        }

    }

    void Patrol()
    {
        lr.enabled = false;
    }

    void PlayParticleSystem()
    {
        // Play particle system before shooting
        if (!ps.isPlaying)
        {
            ps.Play();
            AudioSource.PlayClipAtPoint(shootingSound, transform.position, 0.5f);
        }
    }

    void Shoot()
    {
        // Head, position
        lr.SetPosition(0, turret.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);

                // Check if it is player
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player") && hit.transform.gameObject.GetComponent<PlayerHPShield>().canBeDamaged)
                {
                    DamagingObject damageObject = new DamagingObject();
                    damageObject.Damage = 1;
                    hit.transform.SendMessage("GetDamaged", damageObject);
                }
            }
        }
        else
        {
            // Tail position
            lr.SetPosition(1, transform.forward * 5000);
        }

        lr.enabled = true;
    }
}
