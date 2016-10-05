using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {
    public Vector3 target;
    public float damage, speed;
	
    // Use this for initialization
	void Start () {
        Vector3 lookPos = target - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, -angle);
    }

    float lifetime = 7f;
	// Update is called once per frame
	void Update () {
        transform.position += transform.right*speed*Time.deltaTime;
        EnemyHitTest();
        lifetime -= Time.deltaTime;
        if (lifetime < 0f)
            Destroy(this);
    }

    void EnemyHitTest()
    {
        var hit = Physics2D.OverlapCircle(transform.position, 0.33f, 1 << 10);
        if(hit)
        {
            var e = hit.GetComponent<Enemy>();
            if (e != null)
                e.DealDamage(damage);
            Destroy(gameObject);
        }
    }
}
