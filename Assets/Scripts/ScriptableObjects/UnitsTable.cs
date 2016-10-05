using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class UnitsTable : ScriptableObject {

    public List<UnitInfo> units = new List<UnitInfo>();
    public UnitInfo GetUnitInfo(int id)
    {
        if (id < units.Count)
            return units[id];
        else return null;
    }

    public UnitInfo GetUnitInfo(string prefab)
    {
        return units.Where(o => o.prefabName == prefab).FirstOrDefault();
    }
}

[Serializable]
public class UnitInfo : System.Object
{
    public string name;
    public string description;
    public string prefabName;
    public int price;
}