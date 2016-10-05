using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour {
    Transform levelData;
    Text goldText;
	// Use this for initialization
	void Start () {
        levelData = transform.GetChild(1);
        goldText = levelData.GetChild(0).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        goldText.text = string.Format("${0}", LevelData.Current.gold);
	}
}
