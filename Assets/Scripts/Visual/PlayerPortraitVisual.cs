using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerPortraitVisual : MonoBehaviour {

    public CharacterAsset charAsset;

    public Text CurrentHealthText;
    public Text MaxHealthText;

    public Image PortraitImage;

    public Player playerScript;

    public HealthBar healthBar;



    void Start()
    {
        //MaxHealthText.text = charAsset.MaxHealth.ToString();
        //healthBar.SetMaxHealth(charAsset.MaxHealth);
    }

    void Awake()
    {
        var sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MapScene")
        {
            if (charAsset != null)
                ApplyLookFromAssetInMap();
        }
        else
        {
            if (charAsset != null)
                ApplyLookFromAsset();
        }
    }
    public void ApplyLookFromAsset(int health = 0)
    {
            CurrentHealthText.text = health == 0 ? charAsset.MaxHealth.ToString() : health.ToString();
            MaxHealthText.text = charAsset.MaxHealth.ToString();

            healthBar.SetMaxHealth(charAsset.MaxHealth);
            healthBar.SetHealth(health == 0 ? charAsset.MaxHealth : health);

            PortraitImage.sprite = charAsset.AvatarImage;
    }

    public void ApplyLookFromAssetInMap()
    {
        int currentHealth = PlayerPrefs.HasKey("PlayerHealth") ? PlayerPrefs.GetInt("PlayerHealth") : charAsset.MaxHealth;

        CurrentHealthText.text = currentHealth.ToString();
        MaxHealthText.text = charAsset.MaxHealth.ToString();

        healthBar.SetMaxHealth(charAsset.MaxHealth);
        healthBar.SetHealth(currentHealth);

        PortraitImage.sprite = charAsset.AvatarImage;
    }
    public void TakeDamage(int amount, int healthAfter)
    {
        if (amount > 0)
        {
            DamageEffect.CreateDamageEffect(transform.position, amount);
            CurrentHealthText.text = healthAfter.ToString();
            healthBar.SetHealth(healthAfter);
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
        //Instantiate(GlobalSettings.Instance.ExplosionPrefab, transform.position, Quaternion.identity); // wizualnie dać cherring
        //Sequence s = DOTween.Sequence();
        //s.PrependInterval(2f);
        //s.OnComplete(() => 
        GlobalSettings.Instance.VictoryPanel.SetActive(true);
    }

}
