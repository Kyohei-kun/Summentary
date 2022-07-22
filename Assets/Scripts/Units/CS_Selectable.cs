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
            selectedGameObject = transform.Find("Selected").gameObject;
        }
        catch (System.Exception)
        {
            if (this is CS_Ally)
            {
                Debug.LogError("No SelectedGameObject setup in parameter");
                throw;
            }
            selectedGameObject = new GameObject();

        }
    }
}
