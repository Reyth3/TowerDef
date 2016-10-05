using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
    public GameObject ammo;
    public float cooldown, damage, range, shotSpeed;
    [Header("On Upgrade")]
    public float cooldownUpgrade, damageUpgrade, rangeUpgrade, shotSpeedUpgrade;

    private float remainingCooldown;
    private SpriteRenderer sr;
    // Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        remainingCooldown = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
        remainingCooldown -= Time.deltaTime;
        SearchForEnemies();
    }

    void SearchForEnemies()
    {
        var hit = Physics2D.OverlapCircle(transform.position, range, 1 << 10);
        if(hit)
        {
            var e = hit.GetComponent<Enemy>();
            if (e != null && remainingCooldown < 0)
                ShootEnemy(e);
        }
    }

    void ShootEnemy(Enemy e)
    {
        var shot = Instantiate(ammo, transform.position, Quaternion.identity, transform) as GameObject;
        var a = shot.GetComponent<Ammo>();
        a.damage = damage;
        a.speed = shotSpeed;
        a.target = e.transform.position;
        remainingCooldown = cooldown;
    }
}
