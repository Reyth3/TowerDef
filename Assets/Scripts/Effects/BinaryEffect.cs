using UnityEngine;
using System.Collections;

public class BinaryEffect : MonoBehaviour {

    public float scale = 1f;
    private SpriteRenderer sr;
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        InstantiateParticles();
    }

    void InstantiateParticles()
    {
        var prefab = Resources.Load<ParticleSystem>("Particles/Bits");
        var bits = Instantiate(prefab, transform.position, Quaternion.identity, transform) as ParticleSystem;
        bits.startColor = sr.color;
        bits.startLifetime *= scale;
        bits.startSize *= scale;
        bits.startSpeed *= scale;
    }

    public void Explode()
    {
        var prefab = Resources.Load<ParticleSystem>("Particles/BitsExplossion");
        var bits = Instantiate(prefab, transform.position, Quaternion.identity) as ParticleSystem;
        bits.startColor = sr.color;
    }
}
