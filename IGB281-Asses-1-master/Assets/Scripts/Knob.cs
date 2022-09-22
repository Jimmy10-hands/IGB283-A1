using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour
{
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Move()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isMoving)
        {
            //I'm not sure if we can use this
            this.transform.position = new Vector3(this.transform.position.x,mousePosition.y,0);
        }
    }


    void MouseOverAction()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
        if (hitCollider && hitCollider.transform.tag == "Square")
        {
            hitCollider.transform.gameObject.
            GetComponent<Knob>().isMoving = true;
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

    void ChangeKnobColor()
    {
        if(Input.GetKeyDown("q"))
        {
            GetComponent<SpriteRenderer>().color = Color.white;

        }
        if (Input.GetKeyDown("w"))
        {
            GetComponent<SpriteRenderer>().color = Color.blue;

        }
    }


    // Update is called once per frame
    void Update()
    {
        MouseClickAction();
        Move();
        ChangeKnobColor();

    }
}
