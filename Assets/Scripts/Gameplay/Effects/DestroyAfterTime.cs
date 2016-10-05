using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    public float time = 1.5f;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, time);
	}
}
