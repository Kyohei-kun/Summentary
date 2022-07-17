using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Selected_Dictionary : MonoBehaviour
{
    Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();

    public Dictionary<int, GameObject> SelectedTable { get => selectedTable; set => selectedTable = value; }

    public void AddSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!(selectedTable.ContainsKey(id)) && go.GetComponent<CS_Unit>() != null)
        {
            selectedTable.Add(id, go);
            go.GetComponent<CS_Unit>().SetSelectedVisible(true);
        }
    }

    public void Deselect(int id)
    {        
        selectedTable[id].GetComponent<CS_Unit>().SetSelectedVisible(false);
        selectedTable.Remove(id);
    }

    public void DeselectAll()
    {
        foreach (KeyValuePair<int, GameObject> pair in selectedTable)
        {
            if (pair.Value != null)
            {
                pair.Value.GetComponent<CS_Unit>().SetSelectedVisible(false);
            }
        }
        selectedTable.Clear();
    }
}
