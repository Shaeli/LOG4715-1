using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {

    int StartingHP = 3;

    [SerializeField][Range(0,1)]
    float HealthBarAnimationRate = 0.5f;

    public Image HealthBar;

    public int CurrentHP
    {
        get { return currentHP; }
        set
        {
            currentHP = value;
            HealthBar.color = healthColors[currentHP];
        }
    }

    /* * * * * * * * * * * *
     * Internal variables
     * * * * * * * * * * * */

    private int currentHP;
    private static Color[] healthColors = { Color.gray, Color.red, Color.yellow, Color.green };
    private DamagingObject wasDamaged = null;

    // Use this for initialization
    void Start () {
        CurrentHP = StartingHP;
        HealthBar.fillAmount = 1f;
    }

    void Update()
    {
        // Update HealthBar UI.
        if (HealthBar.fillAmount > ((float) CurrentHP / StartingHP)) {
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, (float)CurrentHP / StartingHP, HealthBarAnimationRate);
        }

        // Apply damage once this frame.
        if (wasDamaged != null)
        {
            GetDamaged(wasDamaged);
        }
    }

    /// <summary>
    /// Get the damaging object and apply the defined damage to self.
    /// </summary>
    /// <param name="collision">Collision resulting of OnCollisionEnter.</param>
    void OnCollisionEnter(Collision collision)
    {
        DamagingObject damagingObject = collision.gameObject.GetComponent<DamagingObject>();
        if (damagingObject != null)
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
        if (damagingObject != null)
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
        Debug.Log("Getting damaged!");
        CurrentHP -= damagingObject.Damage;
        wasDamaged = null;
    }
}
