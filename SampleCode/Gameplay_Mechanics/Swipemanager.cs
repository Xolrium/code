using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDir
{
    None = 0,
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8
}

public class SwipeManager : MonoBehaviour {

    private static SwipeManager instance;
    public static SwipeManager Instance{get {return instance;}}

    public SwipeDir Dir { set; get; }

    private Vector3 touchPos;
    private float swipeResX = 100.0f;
    private float swipeResY = 120.0f;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        Dir = SwipeDir.None;

        if (Input.GetMouseButtonDown(0))
        {
            touchPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            
            Vector2 deltaSwipe = touchPos - Input.mousePosition;
            if (Mathf.Abs(deltaSwipe.x) > swipeResX)
            {
                Dir |= (deltaSwipe.x < 0) ? SwipeDir.Right : SwipeDir.Left;
            }
            if (Mathf.Abs(deltaSwipe.y) > swipeResY)
            {
                Dir |= (deltaSwipe.y < 0) ? SwipeDir.Up : SwipeDir.Down;
            }
        }
    }
    public bool IsSwiping(SwipeDir dir)
    {
        return (Dir & dir) == dir;
    }
}
