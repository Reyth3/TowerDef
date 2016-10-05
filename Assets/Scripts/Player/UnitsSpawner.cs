using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitsSpawner : MonoBehaviour {

    bool isSelecting = false;
    GameObject selector;
    GameObject unit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isSelecting)
            PickGridItem();
	}

    void DisableUnitButtons()
    {
        var buttons = GameObject.Find("Defenders").transform.GetChild(0);
        foreach (Transform b in buttons)
            b.GetComponent<Button>().enabled = false;
    }

    void EnableUnitButtons()
    {
        var buttons = GameObject.Find("Defenders").transform.GetChild(0);
        foreach (Transform b in buttons)
            b.GetComponent<Button>().enabled = true;
    }

    void PickGridItem()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.OverlapPoint(pos, 1 << 8);
        if(hit)
        {
            selector.transform.position = transform.position = hit.transform.position;
            if (Input.GetMouseButtonUp(0))
                ConfirmSelection(hit.transform);
        }
    }

    void ConfirmSelection(Transform cell)
    {
        isSelecting = false;
        Destroy(selector);
        unit = Instantiate(Resources.Load<GameObject>("Units/Unit1"), cell.position, Quaternion.identity, cell) as GameObject;
        unit.layer = 9;
        //EnableUnitButtons();
    }

    public void SpawnTestUnit()
    {
        //DisableUnitButtons();
        isSelecting = true;
        selector = Instantiate(Resources.Load<GameObject>("HUD/Selector"));
    }
}
