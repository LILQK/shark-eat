using System;
using DG.Tweening;
using UnityEngine;

public class FishMovement : MonoBehaviour
{

    public float fishSpeed = 2;
    public float slowedSpeed = 1;
    public Camera cam;
    
    public ControlType controlType;
    public SerialReader reader;

    public float leftMinAngle = 10f;
    public float rightMinAngle = 10f;
    public float topMinAngle = 10f;
    public float bottomMinAngle = 10f;
    
    private bool goingLeft = false;
    
    public bool canMove = true;
    
    public float leftBound = 0.05f;
    public float rightBound = 0.95f;
    public float topBound = 0.1f;
    public float bottomBound = 0.95f;
    public enum ControlType
    {
        arduino,
        wasd
    }

    public bool isSlowed = false;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (!canMove) return;
        var pos = transform.position;
        
        float _leftBound = cam.ViewportToWorldPoint(new Vector3(this.leftBound, 0, 0)).x;
        float _rightBound = cam.ViewportToWorldPoint(new Vector3(this.rightBound, 0, 0)).x;
        float _topBound = cam.ViewportToWorldPoint(new Vector3(0, this.topBound, 0)).y;
        float _bottomBound = cam.ViewportToWorldPoint(new Vector3(0, this.bottomBound, 0)).y;

        
        goingLeft = false;
        var currentSpeed = fishSpeed;
        if (isSlowed)
        {
            currentSpeed = slowedSpeed;
        }
        if (controlType == ControlType.arduino)
        {
            if (reader.value1 < leftMinAngle)
            {
                goingLeft = true;
                pos.x -= currentSpeed * Time.deltaTime;
            }else if (reader.value1 > rightMinAngle)
            {
                pos.x += currentSpeed * Time.deltaTime;
            }
            
            if (reader.value2 < topMinAngle)
            {
                pos.y += currentSpeed * Time.deltaTime;
            }else if (reader.value2 > bottomMinAngle)
            {
                pos.y -= currentSpeed * Time.deltaTime;
            }
        }
        else
        {
            goingLeft = Input.GetAxis("Horizontal") < 0.1f;
            pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * fishSpeed;
            pos.y += Input.GetAxis("Vertical") * Time.deltaTime * fishSpeed;
        }
        
        pos.x = Mathf.Clamp(pos.x, _leftBound, _rightBound);
        pos.y = Mathf.Clamp(pos.y, _topBound, _bottomBound);
        transform.position = pos;

        if (goingLeft)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
        }
        else
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
    }

    private void OnDrawGizmos()
    {
        if (cam == null) cam = Camera.main;

        // Calcula los límites según la cámara
        float _leftBound = cam.ViewportToWorldPoint(new Vector3(this.leftBound, 0, 0)).x;
        float _rightBound = cam.ViewportToWorldPoint(new Vector3(this.rightBound, 0, 0)).x;
        float _topBound = cam.ViewportToWorldPoint(new Vector3(0, this.topBound, 0)).y;
        float _bottomBound = cam.ViewportToWorldPoint(new Vector3(0, this.leftBound, 0)).y;

        // Dibuja líneas en los límites
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(_leftBound, _bottomBound, 0), new Vector3(leftBound, topBound, 0));
        Gizmos.DrawLine(new Vector3(_rightBound, _bottomBound, 0), new Vector3(rightBound, topBound, 0));
        Gizmos.DrawLine(new Vector3(_leftBound, _topBound, 0), new Vector3(rightBound, topBound, 0));
        Gizmos.DrawLine(new Vector3(_leftBound, _bottomBound, 0), new Vector3(rightBound, bottomBound, 0));
    }
    
}
