using UnityEngine;
using UnityEditor;
using System.Collections;

public class MenuOptions {
    [MenuItem("Scriptable Objects/Units Database")]
    public static void CreateDialogues()
    {
        ScriptableObjectUtility.CreateAsset<UnitsTable>();
    }
}
