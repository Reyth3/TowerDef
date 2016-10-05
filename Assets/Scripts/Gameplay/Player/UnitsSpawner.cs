using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitsSpawner : MonoBehaviour {

    UnitsTable table;
    bool isSelecting = false;
    GameObject selector;
    GameObject unit;
    Transform buttons;
	// Use this for initialization
	void Start () {
        table = Resources.Load<UnitsTable>("Data/Units");
        InstantiateUnitButtons();
    }
	
	// Update is called once per frame
	void Update () {
        if (isSelecting)
            PickGridItem();
    }

    void InstantiateUnitButtons()
    {
        if(buttons == null)
            buttons = GameObject.Find("Defenders").transform.GetChild(0);
        var prefab = Resources.Load<GameObject>("HUD/UnitButtonTemplate");
        for(int i = 0; i < table.units.Count; i++)
        {
            var t = (Instantiate(prefab, buttons, false) as GameObject).transform;
            var b = t.GetComponentInChildren<Button>();
            b.GetComponentInChildren<Text>().text = table.units[i].name;
            t.GetChild(1).GetComponent<Text>().text = "$" + table.units[i].price;
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(() =>
            {
                var id = b.transform.parent.GetSiblingIndex();
                var u = table.units[id];
                if (LevelData.Current.gold >= u.price)
                {
                    var unitPrefab = Resources.Load<GameObject>("Units/" + u.prefabName);
                    SpawnUnit(unitPrefab);
                    LevelData.Current.gold -= u.price;
                }
                else Debug.LogWarningFormat("Not enough money! Currently: {0}; Needed: {1}", LevelData.Current.gold, u.price);
            });
        }
    }

    void DisableUnitButtons()
    {
        if(buttons == null)
            buttons = GameObject.Find("Defenders").transform.GetChild(0);
        buttons.gameObject.SetActive(false);
    }

    void EnableUnitButtons()
    {
        if (buttons == null)
            buttons = GameObject.Find("Defenders").transform.GetChild(0);
        buttons.gameObject.SetActive(true);
    }

    void PickGridItem()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.OverlapPoint(pos, 1 << 8);
        if(hit)
        {
            selector.transform.position = hit.transform.position;
            if (Input.GetMouseButtonUp(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                ConfirmSelection(hit.transform);
        }
    }

    void ConfirmSelection(Transform cell)
    {
        isSelecting = false;
        Destroy(selector);
        var u = Instantiate(unit, cell.position, Quaternion.identity, cell) as GameObject;
        u.layer = 9;
        EnableUnitButtons();
    }

    public void SpawnUnit(GameObject unit)
    {
        DisableUnitButtons();
        isSelecting = true;
        this.unit = unit;
        selector = Instantiate(Resources.Load<GameObject>("HUD/Selector"));
    }
}
