using UnityEngine;
using System.Collections;

public class LevelData : MonoBehaviour {
    public static LevelData Current;

    public int gold;
	// Use this for initialization
	void Start () {
        if (Current == null)
            Current = this;
        else if (Current != this)
            Destroy(this);
	}
}
