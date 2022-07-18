using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Selected_Dictionary : MonoBehaviour
{
    Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();

    public Dictionary<int, GameObject> SelectedTable { get => selectedTable; set => selectedTable = value; }

    public bool AddSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!(selectedTable.ContainsKey(id)) && go.GetComponent<CS_Ally>() != null)
        {
            selectedTable.Add(id, go);
            go.GetComponent<CS_Ally>().SetSelectedVisible(true);
            return true;
        }
        else return false;
    }

    public void Deselect(int id)
    {        
        selectedTable[id].GetComponent<CS_Ally>().SetSelectedVisible(false);
        selectedTable.Remove(id);
    }

    public void DeselectAll()
    {
        foreach (KeyValuePair<int, GameObject> pair in selectedTable)
        {
            if (pair.Value != null)
            {
                pair.Value.GetComponent<CS_Ally>().SetSelectedVisible(false);
            }
        }
        selectedTable.Clear();
    }
}
