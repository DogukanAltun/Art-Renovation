using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    [SerializeField] private LayerMask WallMask;
    public enum Position { vertical, horizontal };
    private bool levelUp;
    public bool LevelUp { get { return levelUp; } set { levelUp = value; } }
    public Position position;
    private int Counter;
    public bool Left = true;
    public bool Right = true;
    public bool Down = true;
    public bool Up = true;
    public GameObject destination;
    private CanvasManager canvas;
    private LevelManager level;
    private Rigidbody physic;
    public GameObject[] sides;
    [SerializeField] private bool isStop;
    public bool isstop { get { return isStop; } set { isStop = value; } }
    private Vector3 brushPosition;
    private Vector3 directionToTarget;
    public Vector2 startTouchPosition;
    public Vector2 endTouchPosition;
    

    void Start()
    { 
        position = Position.vertical;
        physic = GetComponent<Rigidbody>();
        canvas = FindObjectOfType<CanvasManager>();
        level = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        Touch();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 && isStop == false)
        {
            directionToTarget = Vector3.right * horizontal;
            directionToTarget.Normalize();
            if (horizontal > 0 && Right)
            {
                WallDetect(directionToTarget);
                Counter++;
            }
            else if (horizontal < 0 && Left)
            {
                WallDetect(directionToTarget);
                Counter++;
            }
        }
        else if (vertical != 0 && isStop == false)
        {
            directionToTarget = Vector3.up * vertical;
            directionToTarget.Normalize();
            if (vertical > 0 && Up)
            {
                WallDetect(directionToTarget);
                Counter++;
            }
            else if (vertical < 0 && Down)
            {
                WallDetect(directionToTarget);
                Counter++;
            }
        }
        if (!isStop)
        {
            if(levelUp)
            {
                canvas.NextLevel();
                levelUp = false;
            }
            else if(Counter >= level.countLimit)
            {
                canvas.GameOver();
            }
        }
    }
      private void Touch()
      { 
          if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
          {
              startTouchPosition = Input.GetTouch(0).position;
          }
          if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !isStop)
          {
              endTouchPosition = Input.GetTouch(0).position;
            if (endTouchPosition.x > startTouchPosition.x && Right)
             {
                directionToTarget = Vector3.right;
                directionToTarget.Normalize();
                Debug.Log("Saða");
                WallDetect(directionToTarget);
                Counter++;

             }
            else if (endTouchPosition.x < startTouchPosition.x && Left)
              {
                directionToTarget = Vector3.left;
                directionToTarget.Normalize();
                Debug.Log("Sola");
                WallDetect(directionToTarget);
                Counter++;

              }
            else if (endTouchPosition.y > startTouchPosition.y && Up)
             {
                directionToTarget = Vector3.up;
                directionToTarget.Normalize();
                Debug.Log("Yukarý");
                WallDetect(directionToTarget);
                Counter++;

             }
            else if (endTouchPosition.y < startTouchPosition.y && Down)
              {
                directionToTarget = -Vector3.up;
                directionToTarget.Normalize();
                Debug.Log("Aþaðý");
                WallDetect(directionToTarget);
                Counter++;
              }
          }
      }

    private void WallDetect(Vector3 direction)
    {
        brushPosition = gameObject.transform.position;
        if (Physics.Raycast(brushPosition, direction, out RaycastHit hit, Mathf.Infinity, WallMask))
        {
            isStop = true;
            physic.velocity = direction.normalized*10;
            destination = hit.collider.gameObject;
        }
    }

}

