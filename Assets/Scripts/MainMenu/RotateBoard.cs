using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RotateBoard : MonoBehaviour
{
    //private float _startingPosition;
    //private float startingPosition;

    //public Vector3 targetAngle = new(0f, 345f, 0f);

    private Vector3 currentAngle;

    public float speed = 0.01f;
    float timeCount = 0.0f;

    public void Start()
    {
        currentAngle = transform.eulerAngles;
    }

    //public float rotatespeed = 10f;

    public void Update()
    {
        currentAngle = new Vector3(
        Mathf.LerpAngle(currentAngle.x, currentAngle.x + 1f, speed),
            Mathf.LerpAngle(currentAngle.y, currentAngle.y + 1f, speed),
            Mathf.LerpAngle(currentAngle.z, currentAngle.z + 1f, speed));

        transform.eulerAngles = currentAngle;
        timeCount += Time.deltaTime;

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    switch (touch.phase)
        //    {
        //        case TouchPhase.Began:
        //            _startingPosition = touch.position.x;
        //            break;
        //        case TouchPhase.Moved:
        //            if (startingPosition > touch.position.x)
        //            {
        //                transform.Rotate(Vector3.back, -turnspeed * Time.deltaTime);
        //            }
        //            else if (startingPosition < touch.position.x)
        //            {
        //                transform.Rotate(Vector3.back, rotatespeed * Time.deltaTime);
        //            }
        //            break;
        //        case TouchPhase.Ended:
        //            Debug.Log("Touch Phase Ended.")
        //            break;
        //    }
        //}
    }

    //private Touch touch;
    //private Vector2 oldTouchPosition;
    //private Vector2 NewTouchPosition;
    //[SerializeField]
    //private float keepRotateSpeed = 10f;

    //[SerializeField]
    //private float deltaThreshold = 5f;


    //private void Update()
    //{
    //    RotateThings();
    //}
    //private void RotateThings()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        touch = Input.GetTouch(0);
    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            oldTouchPosition = touch.position;
    //            NewTouchPosition = touch.position;
    //        }

    //        else if (touch.phase == TouchPhase.Moved)
    //        {
    //            oldTouchPosition = NewTouchPosition;
    //            NewTouchPosition = touch.position;
    //        }

    //        float delta = Mathf.Abs(oldTouchPosition.x - NewTouchPosition.x);
    //        if (/*touch.phase != TouchPhase.Stationary &&*/ delta >= deltaThreshold)
    //        {
    //            Vector2 rotDirection = oldTouchPosition - NewTouchPosition;
    //            Debug.Log(delta);
    //            if (rotDirection.x < 0)
    //            {
    //                RotateRight();
    //            }

    //            else if (rotDirection.x > 0)
    //            {
    //                RotateLeft();
    //            }
    //        }
    //    }
    //}

    //void RotateLeft()
    //{
    //    transform.rotation = Quaternion.Euler(0f, 1.5f * keepRotateSpeed, 0f) * transform.rotation;
    //}

    //void RotateRight()
    //{
    //    transform.rotation = Quaternion.Euler(0f, -1.5f * keepRotateSpeed, 0f) * transform.rotation;
    //}

}
