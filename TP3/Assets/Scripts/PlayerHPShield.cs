using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPShield : MonoBehaviour {

    int StartingHP = 3;

    [SerializeField][Range(0,1)]
    float HealthBarAnimationRate = 0.5f;
    [SerializeField] AudioClip Damage;

    public Image HealthBar;

    public float DamageCooldown = 1f;
    public float FlashingRate = 0.5f;
    public GameObject ModelRoot;

    public int CurrentHP
    {
        get { return currentHP; }
        set
        {
            currentHP = value;
            currentHP = Mathf.Max(currentHP, 0);
            currentHP = Mathf.Min(currentHP, StartingHP);
            HealthBar.color = healthColors[currentHP];
        }
    }

    /* * * * * * * * * * * *
     * Internal variables
     * * * * * * * * * * * */

    private int currentHP;
    private static Color[] healthColors = { Color.gray, Color.red, Color.yellow, Color.green };
    private DamagingObject wasDamaged = null;
    private bool canBeDamaged = true;
    private float flashingTimer = 0f;

    // Use this for initialization
    void Start () {
        CurrentHP = StartingHP;
        HealthBar.fillAmount = 1f;
    }

    void Update()
    {
        // Update HealthBar UI.
        if (HealthBar.fillAmount != ((float) CurrentHP / StartingHP)) {
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, (float)CurrentHP / StartingHP, HealthBarAnimationRate);
        }

        // Apply damage once this frame.
        if (wasDamaged != null)
        {
            GetDamaged(wasDamaged);
            AudioSource.PlayClipAtPoint(Damage, transform.position);
        }

        if (!canBeDamaged && CurrentHP > 0)
        {
            flashingTimer += Time.deltaTime;
            if (ModelRoot.activeSelf && flashingTimer > FlashingRate)
            {
                ModelRoot.SetActive(false);
                flashingTimer = 0f;
            }
            else if(!ModelRoot.activeSelf && flashingTimer > FlashingRate)
            {
                ModelRoot.SetActive(true);
                flashingTimer = 0f;
            }
        }
        else if (CurrentHP > 0 && !ModelRoot.activeSelf)
        {
            ModelRoot.SetActive(true);
        }
    }

    /// <summary>
    /// Get the damaging object and apply the defined damage to self.
    /// </summary>
    /// <param name="collision">Collision resulting of OnCollisionEnter.</param>
    void OnCollisionEnter(Collision collision)
    {
        DamagingObject damagingObject = collision.gameObject.GetComponent<DamagingObject>();
        if (damagingObject != null && (gameObject.GetComponent<Shield>() == null || !gameObject.GetComponent<Shield>().shieldIsUp) && canBeDamaged)
        {
            wasDamaged = damagingObject;
        }
    }

    /// <summary>
    /// Get the damaging object and apply the defined damage to self.
    /// </summary>
    /// <param name="other">Collider of the object with which this collided.</param>
    void OnTriggerEnter(Collider other)
    {
        DamagingObject damagingObject = other.gameObject.GetComponent<DamagingObject>();
        if (damagingObject != null && (gameObject.GetComponent<Shield>() == null || !gameObject.GetComponent<Shield>().shieldIsUp) && canBeDamaged)
        {
            wasDamaged = damagingObject;
        }
    }

    /// <summary>
    /// Apply damage then reset wasDamaged variable.
    /// </summary>
    /// <param name="damagingObject"></param>
    private void GetDamaged(DamagingObject damagingObject)
    {
        CurrentHP -= damagingObject.Damage;
        wasDamaged = null;
        canBeDamaged = false;
        Invoke("EnableCanBeDamaged", DamageCooldown);
    }

    private void EnableCanBeDamaged()
    {
        canBeDamaged = true;
    }
}
