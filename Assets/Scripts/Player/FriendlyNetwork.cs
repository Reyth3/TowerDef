using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FriendlyNetwork : MonoBehaviour {

    public float maxHealth = 100;
    internal float curHealth;
    private Slider HealthBar;
    private SpriteRenderer sr;
    // Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        curHealth = maxHealth;
        InstantiateHealthBar();
    }
	
	// Update is called once per frame
	void Update () {
        DetectEnemies();
        UpdateHealthBar();
	}

    void InstantiateHealthBar()
    {
        var prefab = Resources.Load<GameObject>("HUD/HealthBar");
        var hpbar = Instantiate(prefab, transform.position - new Vector3(0f, 2.15f, 0f), Quaternion.identity, transform) as GameObject;
        HealthBar = hpbar.GetComponentInChildren<Slider>();
        HealthBar.transform.GetChild(1).GetComponentInChildren<Image>().color = sr.color;
    }

    void UpdateHealthBar()
    {
        HealthBar.maxValue = maxHealth;
        HealthBar.value = curHealth;
    }

    void DetectEnemies()
    {
        var hit = Physics2D.OverlapCircle(transform.position, 1.5f);
        if(hit)
        {
            var enemy = hit.gameObject.GetComponent<Enemy>();
            if (enemy != null)
                enemy.Attack(this);
        }
    }

    public void Damage(float dmg)
    {
        curHealth -= dmg;
        if (curHealth < 0)
            SceneManager.LoadScene(0);
    }

}
