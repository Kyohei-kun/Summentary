using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Selected_Dictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();

    public void addSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!(selectedTable.ContainsKey(id)) && go.GetComponent<CS_Unit>() != null)
        {
            selectedTable.Add(id, go);
            go.GetComponent<CS_Unit>().SetSelectedVisible(true);
            Debug.Log("Added " + id + " to selected dict");
        }
    }

    public void deselect(int id)
    {
        selectedTable.Remove(id);
    }

    public void deselectAll()
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
