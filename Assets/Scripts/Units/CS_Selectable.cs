using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Selectable : MonoBehaviour
{
    protected GameObject selectedGameObject;

    public virtual void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    protected virtual void Start()
    {
        try
        {
            selectedGameObject = RecursiveFindChild(transform, "Selected").gameObject;
        }
        catch (System.Exception)
        {
            selectedGameObject = new GameObject();
            Debug.LogError("Not selected Gameobject on " + gameObject.name);
        }

        selectedGameObject.SetActive(false);
    }

    protected Transform RecursiveFindChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }
            else
            {
                Transform found = RecursiveFindChild(child, name);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }
}
