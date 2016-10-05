using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Vector3 direction;
    public float speed,damage,health;
    public int Gold { get { return (5 + 2 * Random.Range(0, 2)) + Random.Range(0, 2); } }
    public Color color = new Color(1, 1, 1);

    private SpriteRenderer sr;
    private BinaryEffect be;
    // Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.color = color;
        be = GetComponent<BinaryEffect>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!Physics2D.Raycast(transform.position, direction, 0.5f, 1 << 8))
            transform.position += direction * speed * Time.deltaTime;
        else DecideTurn();
	}

    void DecideTurn()
    {
        Vector3 pos = transform.position, left = Quaternion.Euler(0, 0, -90) * direction, right = Quaternion.Euler(0, 0, 90) * direction;
        bool canLeft = false, canRight = false;
        if (!Physics2D.Raycast(pos, left, 1f, 1 << 8))
            canLeft = true;
        if (!Physics2D.Raycast(pos, right, 1f, 1 << 8))
            canRight = true;
        if (canLeft && canRight)
            if (Random.Range(0, 2) == 0)
                direction = left;
            else direction = right;
        else if (canLeft)
            direction = left;
        else if (canRight)
            direction = right;
        else direction = -direction;
    }

    public void Attack(FriendlyNetwork fn)
    {
        StartCoroutine(_attack(fn));
    }

    IEnumerator _attack(FriendlyNetwork fn)
    {
        be.Explode();
        var hits = Random.Range(3, 6);
        var dmg = damage / hits;
        GetComponent<Collider2D>().enabled = false;
        for (int i = 0; i < hits; i++)
        {
            fn.Damage(Random.Range(0f, 1f) > 0.9f ? dmg * 1.5f : dmg);
            yield return new WaitForSeconds(0.045f);
        }
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

    public void DealDamage(float dmg)
    {
        health -= dmg;
        Debug.Log("Dmg: " + dmg + " Remaining HP: " + health);
        if (health <= 0f)
        {
            LevelData.Current.gold += Gold;
            Destroy(gameObject);
        }
    }
}
