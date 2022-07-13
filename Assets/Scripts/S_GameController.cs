using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_GameController : MonoBehaviour
{
    Camera cam;
    Vector3 startPosition;

    [SerializeField] NavMeshAgent playerAgent;
    [SerializeField] Transform selectionAreaTransform;
    //LayerMask groundLayer;

    private void Start()
    {
        selectionAreaTransform.gameObject.SetActive(false);
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && Input.GetMouseButtonDown(1))
        {
            playerAgent.SetDestination(hit.point);
        }

        if (Input.GetMouseButtonDown(0)) //Left mouse button pressed
        {
            selectionAreaTransform.gameObject.SetActive(true);
            startPosition = hit.point;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = hit.point;
            Vector3 lowerLeft = new Vector3(Mathf.Min(startPosition.x, currentMousePosition.x), startPosition.y+.1f, Mathf.Min(startPosition.z, currentMousePosition.z));
            Vector3 upperRight = new Vector3(Mathf.Max(startPosition.x, currentMousePosition.x), startPosition.y+.1f, Mathf.Max(startPosition.z, currentMousePosition.z));

            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = upperRight - lowerLeft;
        }

        if (Input.GetMouseButtonUp(0)) //Left mouse button released
        {
            selectionAreaTransform.gameObject.SetActive(false);
            Collider[] hitColliders = Physics.OverlapBox((startPosition + hit.point) / 2, Abs(startPosition - hit.point), Quaternion.identity);

            foreach (Collider collider in hitColliders)
            {
                Debug.Log(collider);
            }
        }
    }

    private Vector3 Abs(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }
}
