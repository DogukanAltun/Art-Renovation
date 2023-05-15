using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    private enum Direction { Left, Right, Up, Down }
    [SerializeField] private Direction verticaldirection;
    [SerializeField] private Direction horizontaldirection;
    private Brush brush;
    private BrushEdge edge;
    private BoxCollider box;
    private GameObject screen;
    private int value = 1;
    public static NodeManager instance;
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        screen = GameObject.FindGameObjectWithTag("screen");
        brush = FindObjectOfType<Brush>();
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Edge")
        {
            GameObject parent = other.gameObject.transform.parent.gameObject;
            other.gameObject.transform.SetParent(null);
            parent.transform.SetParent(other.transform);
            Deactivate();
            edge = other.gameObject.GetComponent<BrushEdge>();
            Rotate(other);
        }
    }
    public int GetHitX(Collider col)
    {
        Bounds brush_bounds = gameObject.transform.GetComponent<BoxCollider>().bounds;
        Bounds col_bounds = col.bounds;
        float min_x = Mathf.Max(col_bounds.min.x, brush_bounds.min.x);
        float max_x = Mathf.Min(col_bounds.max.x, brush_bounds.max.x);
        float average = (min_x + max_x) / 2f - col_bounds.min.x;
        if (average > col_bounds.size.x - 0.2f)
        {
            return GetHitY(col);
        }
        else
        {
            return GetHitY(col);
        }
    }

    public int GetHitY(Collider col)
    {
        if (horizontaldirection == Direction.Left && verticaldirection == Direction.Down)
        {
            if (edge.edge == BrushEdge.Edge.bot)
            {
                if (brush.position == Brush.Position.horizontal)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                }
            }
            else
            {
                if (brush.position == Brush.Position.horizontal)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                }
            }
        }
        else if (horizontaldirection == Direction.Right && verticaldirection == Direction.Down)
        {
            if (edge.edge == BrushEdge.Edge.bot)
            {
                if (brush.position == Brush.Position.horizontal)
                {
                    value = -1;
                }
                else
                {
                    value = 1;
                }
            }
            else
            {
                if (brush.position == Brush.Position.horizontal)
                {
                    value = -1;
                }
                else
                {
                    value = 1;
                }
            }
        }
        if (horizontaldirection == Direction.Left && verticaldirection == Direction.Up)
        {
            if (edge.edge == BrushEdge.Edge.bot)
            {
                if (brush.position == Brush.Position.horizontal)
                {
                    value = -1;
                }
                else
                {
                    value = 1;
                }
            }
            else
            {
                if (brush.position == Brush.Position.horizontal)
                {
                    value = -1;
                }
                else
                {
                    value = 1;
                }
            }
        }
        else if (horizontaldirection == Direction.Right && verticaldirection == Direction.Up)
        {
            if (edge.edge == BrushEdge.Edge.bot)
            {
                if (brush.position == Brush.Position.horizontal)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                }
            }
            else
            {
                if (brush.position == Brush.Position.horizontal)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                }
            }
        }
        return value * 90;
    }
    private void Rotate(Collider gameobject)
    {
        gameobject.transform.Rotate(0f, 0f, GetHitX(gameobject));
        GameObject child = gameobject.gameObject.transform.GetChild(0).transform.gameObject;
        child.transform.SetParent(screen.transform);
        gameobject.gameObject.transform.SetParent(child.transform);
        if (brush.position == Brush.Position.vertical)
        {
            brush.position = Brush.Position.horizontal;
        }
        else if (brush.position == Brush.Position.horizontal)
        {
            brush.position = Brush.Position.vertical;
        }
        Cleaner.instance.ChangeValues();
        Cleaner.instance.ChangeMid();
    }


    public void Activate()
    {
        box.enabled = true;
    }

    public void Deactivate()
    {
        box.enabled = false;
    }
}
