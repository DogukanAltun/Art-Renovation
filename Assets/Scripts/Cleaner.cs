using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private LayerMask PictureMask;
    [SerializeField] private Texture2D _dirtMaskBase;
    [SerializeField] private Texture2D _brush;
    [SerializeField] private GameObject Mid;
    private Material _material;
    private Brush brushh;
    private bool letstart;
    private Texture2D _templateDirtMask;
    private Vector3 directionToTarget;
    private Vector3 brushPosition;
    private Color pixelDirtMask;
    private int pixelX;
    private int pixelY;
    private bool horizontal;
    private float width;
    private float height;
    private float totalPixels;
    private float cleanedPixels;
    public static Cleaner instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        letstart = true;
        height = _dirtMaskBase.height / 3f;
        width = height / 6f;
        directionToTarget = new Vector3(1, 0, 1);
        brushh = FindObjectOfType<Brush>();
        Mid = brushh.sides[0];
        totalPixels = _dirtMaskBase.width * _dirtMaskBase.height;
    }

    private void Update()
    {
        brushPosition = Mid.transform.position;
        Hitter();
    }

    public void Hitter() 
    {
        if (brushh.isstop)
        {
            if (Physics.Raycast(brushPosition, directionToTarget, out RaycastHit hit, Mathf.Infinity, PictureMask))
            {
                Vector2 textureCoord = hit.textureCoord;

                pixelX = (int)(textureCoord.x * _templateDirtMask.width);
                pixelY = (int)(textureCoord.y * _templateDirtMask.height);

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixelDirt = _brush.GetPixel(x, y);
                        pixelDirtMask = _templateDirtMask.GetPixel(pixelX + x, pixelY + y);
                        if (pixelDirtMask != Color.black)
                        {
                            _templateDirtMask.SetPixel(pixelX + x, pixelY + y, new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
                            cleanedPixels++;
                        }
                    }
                }
            }
        }
        _templateDirtMask.Apply();
        float percentage = ((float)cleanedPixels / (float)totalPixels) * 100;
        if(percentage > 98 && letstart)
        {
            brushh.LevelUp = true;
            letstart = false;
        }
    }

    public void ChangeValues()
    {
        float temporary = width;
        width = height;
        height = temporary;
        if (horizontal == false)
        {
            horizontal = true;
            width = width * 1.2f;
        }
        else
        {
            horizontal = false;
            height = height * 0.83f;
        }
    }

    public void ChangeMid()
    {
        float rotat = Mid.transform.parent.transform.rotation.eulerAngles.z;
        if (rotat == 0)
        {
            Mid = brushh.sides[0];
        }
        else if (rotat == 90)
        {
            Mid = brushh.sides[1];
        }
        else if(rotat == 180)
        {
            Mid = brushh.sides[2];
        }
        else if(rotat == 270)
        {
            Mid = brushh.sides[3];
        }
    }


    public void CreateTexture(Material art)
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels(_dirtMaskBase.GetPixels());
        _templateDirtMask.Apply();
        _material = art;
        _material.SetTexture("_Mask", _templateDirtMask);
    }
}
