using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryPoints : MonoBehaviour
{
    public Material material;
    public Mesh mesh;

    public float x;
    public float y;
    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        GetComponent<MeshRenderer>().material = material;
        mesh.Clear();

        mesh.vertices = new Vector3[]
        {
            
            new Vector3(x-1, y-1, 0),
            new Vector3(x-1, y+1, 0),
            new Vector3(x+1, y+1, 0),
            new Vector3(x+1, y-1, 0),
            new Vector3(x,y,0)
        };
        mesh.colors = new Color[]
        {
            new Color(1f, 0.5f, 1f, 1.0f),
            new Color(1f, 0.5f, 1f, 1.0f),
            new Color(1f, 0.5f, 1f, 1.0f),
            new Color(1f, 0.5f, 1f, 1.0f),
            new Color(1f, 0.5f, 1f, 1.0f)
        };
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3};
    }


    void Move()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isMoving)
        {
            y = mousePosition.y;
            //I'm not sure if we can use this
            //this.transform.position = new Vector3(this.transform.position.x, mousePosition.y, 0);
        }
    }


    void MouseOverAction()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //gets mouse pos
        Debug.Log(mousePosition);
        //Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
        if (Mathf.Abs(mousePosition[0]-x)<=1 && Mathf.Abs(mousePosition[1] - y) <= 1) //hitCollider && hitCollider.transform.tag == "Square"
        {
            //hitCollider.transform.gameObject.
            //GetComponent<Knob>().isMoving = true;
            isMoving = true;
        }

    }

    void MouseClickAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseOverAction();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

    }


    // Update is called once per frame
    void Update()
    {
        MouseClickAction();
        Move();
        mesh.vertices = new Vector3[]
        {

            new Vector3(x-1, y-1, 0),
            new Vector3(x-1, y+1, 0),
            new Vector3(x+1, y+1, 0),
            new Vector3(x+1, y-1, 0),
            new Vector3(x,y,0)
        };
    }

}
