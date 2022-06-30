using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerPortraitVisual : MonoBehaviour {

    public CharacterAsset charAsset;

    public Text HealthText;
    public Text CurrentHealthText;

    public Image PortraitImage;

    public Player playerScript;

    public HealthBar healthBar;

    public int currentHealth;
    void Start()
    {
        currentHealth = playerScript.CurrentHealth;

        healthBar.SetMaxHealth(charAsset.MaxHealth);

        healthBar.SetHealth(currentHealth);
    }
  
    void Awake()
	{
        if (charAsset != null)
			ApplyLookFromAsset();
	}

	public void ApplyLookFromAsset()
    {
        HealthText.text = charAsset.MaxHealth.ToString();
        CurrentHealthText.text = playerScript.CurrentHealth.ToString();

        PortraitImage.sprite = charAsset.AvatarImage;
    }

    public void TakeDamage(int amount, int healthAfter)
    {
        if (amount > 0)
        {
            DamageEffect.CreateDamageEffect(transform.position, amount);
            HealthText.text = healthAfter.ToString();
            healthBar.SetHealth(healthAfter);

            CurrentHealthText.text = healthAfter.ToString();
            currentHealth = healthAfter;
        }
    }

    public void Explode()
    {
        Instantiate(GlobalSettings.Instance.ExplosionPrefab, transform.position, Quaternion.identity); // dać umarcie
        Sequence s = DOTween.Sequence();
        s.PrependInterval(0.1f);
        s.OnComplete(() => GlobalSettings.Instance.GameOverPanel.SetActive(true));
    }

    public void CherryUp()
    {
        Instantiate(GlobalSettings.Instance.ExplosionPrefab, transform.position, Quaternion.identity); // wizualnie dać cherring
        Sequence s = DOTween.Sequence();
        s.PrependInterval(2f);
        s.OnComplete(() => GlobalSettings.Instance.VictoryPanel.SetActive(true));
    }

}
