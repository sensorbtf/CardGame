using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerPortraitVisual : MonoBehaviour {

    public CharacterAsset charAsset;
    [Header("Text Component References")]
    //public Text NameText;
    public Text HealthText;
    [Header("Image References")]
    public Image PortraitImage;

    public Player playerScript;

    public HealthBar healthBar;

    void Start()
    {
        healthBar.SetMaxHealth(charAsset.MaxHealth);
    }
  
    void Awake()
	{
        if (charAsset != null)
			ApplyLookFromAsset();
	}

	public void ApplyLookFromAsset()
    {
        HealthText.text = charAsset.MaxHealth.ToString();
        PortraitImage.sprite = charAsset.AvatarImage;
    }

    public void TakeDamage(int amount, int healthAfter)
    {
        if (amount > 0)
        {
            DamageEffect.CreateDamageEffect(transform.position, amount);
            HealthText.text = healthAfter.ToString();
            healthBar.SetHealth(playerScript.Health); //
            healthBar.SetHealth(healthAfter);  //////// Both arent working
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
