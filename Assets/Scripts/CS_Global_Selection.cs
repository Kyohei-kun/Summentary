using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Global_Selection : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject debugSphere;
    [SerializeField] GameObject rootMeshSelection;

    CS_Selected_Dictionary selected_table;
    List<GameObject> goThisfixedFrame;
    RaycastHit hit;

    bool dragSelect;

    //Collider variables
    //=======================================================//

    MeshCollider selectionBox;
    Mesh selectionMesh;

    Vector3 p1;
    Vector3 p2;

    //the corners of our 2d selection box
    Vector2[] corners;

    //the vertices of our meshcollider
    Vector3[] verts;
    Vector3[] vecs;
    private bool triggerTaked;

    void Start()
    {
        selected_table = GetComponent<CS_Selected_Dictionary>();
        dragSelect = false;
        goThisfixedFrame = new List<GameObject>();
    }

    void Update()
    {
        rootMeshSelection.transform.position = Vector3.zero;
        
        //1. when left mouse button clicked (but not released)
        if (Input.GetMouseButtonDown(0))
        {
            p1 = Input.mousePosition;
            selected_table.DeselectAll();
        }

        //2. while left mouse button held
        if (Input.GetMouseButton(0))
        {
            if ((p1 - Input.mousePosition).magnitude > 40)
            {
                dragSelect = true;
            }

            if (dragSelect == false) //single select
            {
                Ray ray = Camera.main.ScreenPointToRay(p1);

                if (Physics.Raycast(ray, out hit, 50000.0f))
                {
                    if (Input.GetKey(KeyCode.LeftShift)) //inclusive select
                    {
                        selected_table.AddSelected(hit.transform.gameObject);
                    }
                    else //exclusive selected
                    {
                        selected_table.DeselectAll();
                        selected_table.AddSelected(hit.transform.gameObject);
                    }
                }
                else //if we didnt hit something
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        //do nothing
                    }
                    else
                    {
                        selected_table.DeselectAll();
                    }
                }
            }
            else //marquee select
            {
                verts = new Vector3[4];
                vecs = new Vector3[4];
                int i = 0;
                p2 = Input.mousePosition;
                corners = GetBoundingBox(p1, p2);

                foreach (Vector2 corner in corners)
                {
                    Ray ray = Camera.main.ScreenPointToRay(corner);

                    if (Physics.Raycast(ray, out hit, 50000.0f, layerMask))
                    {
                        //Debug.Log(hit.point);
                        verts[i] = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        vecs[i] = ray.origin - hit.point;
                        Debug.DrawLine(Camera.main.ScreenToWorldPoint(corner), hit.point, Color.red, 1.0f);
                    }
                    i++;
                }
                //generate the mesh
                selectionMesh = GenerateSelectionMesh(verts, vecs);

                //--------------------------     debug volume selection     --------------------------
                //MeshFilter filter = gameObject.AddComponent<MeshFilter>();
                //filter.mesh = selectionMesh;
                //MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
                //Destroy(filter, 0.02f);
                //Destroy(renderer, 0.02f);

                
                

                selectionBox = rootMeshSelection.AddComponent<MeshCollider>();
                selectionBox.sharedMesh = selectionMesh;
                selectionBox.convex = true;
                selectionBox.isTrigger = true;
                Destroy(selectionBox, 0.02f);
                rootMeshSelection.transform.localRotation = Quaternion.Euler(new Vector3(-45, 0, 0));
            }//end marquee select
        }

        //3. when mouse button comes up
        if (Input.GetMouseButtonUp(0))
        {
            dragSelect = false;
        }
    }

    private void FixedUpdate()
    {
        if (dragSelect)
        {
            if (triggerTaked)
            {
                if (goThisfixedFrame.Count > 0)
                {
                    foreach (GameObject go in goThisfixedFrame)
                    {
                        if (selected_table.SelectedTable.ContainsValue(go) == false)
                        {
                            selected_table.AddSelected(go);
                        }
                    }

                    List<int> tempToDelete = new List<int>();

                    foreach (GameObject go in selected_table.SelectedTable.Values)
                    {
                        if (goThisfixedFrame.Contains(go) == false)
                        {
                            tempToDelete.Add(go.GetInstanceID());
                        }
                    }
                    foreach (int nb in tempToDelete)
                    {
                        selected_table.Deselect(nb);
                    }
                    goThisfixedFrame.Clear();
                    triggerTaked = false;
                    rootMeshSelection.transform.rotation = Quaternion.identity;
                }
                else
                {
                    //Debug.Log("DeselectAll");
                    selected_table.DeselectAll();
                }
                goThisfixedFrame.Clear();
                triggerTaked = false;
                rootMeshSelection.transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            goThisfixedFrame.Clear();
            rootMeshSelection.transform.rotation = Quaternion.identity;
        }
    }

    private void OnGUI()
    {
        if (dragSelect == true)
        {
            var rect = CS_Utils.GetScreenRect(p1, Input.mousePosition);
            CS_Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.10f));
            CS_Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    //create a bounding box (4 corners in order) from the start and end mouse position
    Vector2[] GetBoundingBox(Vector2 p1, Vector2 p2)
    {
        // Min and Max to get 2 corners of rectangle regardless of drag direction.
        var bottomLeft = Vector3.Min(p1, p2);
        var topRight = Vector3.Max(p1, p2);

        // 0 = top left; 1 = top right; 2 = bottom left; 3 = bottom right;
        Vector2[] corners =
        {
            new Vector2(bottomLeft.x, topRight.y),
            new Vector2(topRight.x, topRight.y),
            new Vector2(bottomLeft.x, bottomLeft.y),
            new Vector2(topRight.x, bottomLeft.y)
        };
        return corners;

    }

    //generate a mesh from the 4 bottom points
    Mesh GenerateSelectionMesh(Vector3[] corners, Vector3[] vecs)
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube

        for (int i = 0; i < 4; i++)
        {
            verts[i] = corners[i];
        }

        for (int j = 4; j < 8; j++)
        {
            verts[j] = corners[j - 4] + vecs[j - 4];
        }

        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;

        //--------------------------     debug corner     --------------------------
        //foreach (Vector3 item in verts)
        //{
        //    GameObject temp = Instantiate(debugSphere);
        //    temp.transform.position = item;
        //}

        return selectionMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerTaked = true;
        if (other.gameObject.GetComponent<CS_Ally>() != null)
        {
            goThisfixedFrame.Add(other.gameObject);
        }
    }

}