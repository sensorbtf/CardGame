using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OneCreatureManager : MonoBehaviour 
{
    public CardAsset cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public Text MaxHealthText;
    public Text CurrentHealthText;
    public Text AttackText;
    [Header("Image References")]
    public Image CreatureGraphicImage;
    //public Image CreatureGlowImage;
    public HealthBar healthBar;

    void Awake()
    {
        if (cardAsset != null)
            ReadCreatureFromAsset();
    }

    private bool canAttackNow = false;
    public bool CanAttackNow
    {
        get
        {
            return canAttackNow;
        }

        set
        {
            canAttackNow = value;

            //CreatureGlowImage.enabled = value;
        }
    }

    public void ReadCreatureFromAsset()
    {
        // Change the card graphic sprite
        CreatureGraphicImage.sprite = cardAsset.CardImage;

        AttackText.text = cardAsset.Attack.ToString();
        MaxHealthText.text = cardAsset.MaxHealth.ToString();
        CurrentHealthText.text = cardAsset.MaxHealth.ToString();

        healthBar.SetMaxHealth(cardAsset.MaxHealth);

        if (PreviewManager != null)
        {
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }
    }	

    public void TakeDamage(int amount, int healthAfter)
    {
        if (amount > 0)
        {
            DamageEffect.CreateDamageEffect(transform.position, amount);
            MaxHealthText.text = healthAfter.ToString();
            healthBar.SetHealth(healthAfter);
            CurrentHealthText.text = healthAfter.ToString();
        }
    }
}
