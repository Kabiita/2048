using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Swipte : MonoBehaviour
{
    Touch initTouch;
    bool swiped;
    public float speed = 4.0f;
    private Vector2 direction = Vector2.zero;
    void FixedUpdate()
    {
        touch();
    }
    void touch()
    {
        foreach (Touch touched in Input.touches)
        {
            if (touched.phase == TouchPhase.Began)
            {
                initTouch = touched;
            }
            else if (touched.phase == TouchPhase.Moved && !swiped)
            {
                float xmoved = initTouch.position.x - touched.position.x;
                float ymoved = initTouch.position.y - touched.position.y;
                float distance = Mathf.Sqrt((xmoved * xmoved) + (ymoved * ymoved));
                bool swipedLeft = Mathf.Abs(xmoved) > Mathf.Abs(ymoved);

                if (distance > 40f)
                {
                    if (swipedLeft==true && xmoved > 0)
                    {
                        direction = Vector2.left;
                        Debug.Log("Swipped Left");
                    }
                    else if (swipedLeft==true && xmoved < 0)
                    {
                        direction = Vector2.right;
                        Debug.Log("Swipped Right");
                    }
                    else if (swipedLeft == false && ymoved > 0)
                    {
                        direction = Vector2.down;
                        Debug.Log("Swipped Down");
                    }
                    else if (swipedLeft == false && ymoved < 0)
                    {
                        direction = Vector2.up;
                        Debug.Log("Swipped Up");
                    }
                    direction.Normalize();
                    swiped = true;
                }
            }
            else if (touched.phase == TouchPhase.Ended)
            {
                initTouch = new Touch();
                swiped = false;
            }
        }
    }
}