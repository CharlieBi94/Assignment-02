using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] pathPoints;
    public float speed;
    private int currentIndex;
    private int nextIndex;
    private float error = 0.01f;

    Rigidbody2D rbody;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        transform.position = pathPoints[currentIndex].transform.position;
        
        rbody= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Mathf.Abs(transform.position.x - pathPoints[currentIndex+1].position.x));
        if (currentIndex + 2 <= pathPoints.Length)
        {
            nextIndex = currentIndex + 1;
        }
        else
        {
            currentIndex = -1;
            nextIndex = 0;
        }
        //Debug.Log("Current Index:" +currentIndex);
        //Debug.Log("NextIndex:"+ nextIndex);

        if(Mathf.Abs(transform.position.x - pathPoints[nextIndex].position.x) <= error && Mathf.Abs(transform.position.x - pathPoints[nextIndex].position.x) < error)
        {
            currentIndex++;
        }
        MoveTowards(pathPoints[nextIndex].position);

        
    }

    private void MoveTowards(Vector3 location)
    {
        transform.position = Vector3.MoveTowards(transform.position, location, Time.deltaTime * speed);
    }






    
}
