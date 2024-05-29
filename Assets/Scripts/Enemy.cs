using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int Speed;
    public int SpeedRotate;

    public Vector3 RelativePosition;

    private Vector3 startPosition;
    private Vector3 endPosition;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + RelativePosition;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Speed > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, Speed*Time.deltaTime);
            if (transform.position == endPosition)
            {
                endPosition = startPosition;
                startPosition = transform.position;
            }
        }
        
        if (SpeedRotate > 0)
        {
            transform.Rotate(new Vector3(0, 0, SpeedRotate * Time.deltaTime));
        }
    }
}
