using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    public Brush brush;
    float height = Screen.height;
    float dimension;
    private void Start()
    {
        brush = FindObjectOfType<Brush>();
        dimension = height;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
                if (gameObject.tag == "Vertical")
                {
                    GetHitY(collision.collider);
                }
                else if (gameObject.tag == "Horizontal")
                {
                    GetHitX(collision.collider);
                }
                brush.isstop = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(gameObject.tag == "Vertical")
            {
                GetOutY();
            }
            else if(gameObject.tag == "Horizontal")
            {
                GetOutX();
            }
        }
    }

    public void GetOutX()
    {
        brush.Left = true;
        brush.Right = true;
    }

    public void GetOutY()
    {
        brush.Up = true;
        brush.Down = true;
    }

    public void GetHitX(Collider col)
    {
       Bounds brush_bounds = gameObject.transform.GetComponent<BoxCollider>().bounds;
       Bounds col_bounds = col.bounds;
       float min_x = Mathf.Max(col_bounds.min.x, brush_bounds.min.x);
       float max_x = Mathf.Min(col_bounds.max.x, brush_bounds.max.x);
       float average = (min_x + max_x) / 2f - col_bounds.min.x;
       if (average > col_bounds.size.x - 0.2f)
       {
            brush.Right = false;
       }
       else if (average < 0.8)
       {
            brush.Left = false;
       }
    }

    public void GetHitY(Collider col)
    {
        Bounds brush_bounds = gameObject.transform.GetComponent<BoxCollider>().bounds;
        Bounds col_bounds = col.bounds;
        float min_y = Mathf.Max(col_bounds.min.y, brush_bounds.min.y);
        float max_y = Mathf.Min(col_bounds.max.y, brush_bounds.max.y);
        float average = (min_y + max_y) / 2f - col_bounds.min.y;
        if (average > col_bounds.size.y - 0.2f)
        {
             brush.Up = false;
        }
        else if (average < 0.8)
        {
            brush.Down = false;
        }
    }


}
