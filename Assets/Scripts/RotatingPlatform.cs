using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField]bool rotateForwards;
    [SerializeField] bool rotateBackwards;
    [SerializeField]int rotationSpeed = 100;
    float rotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        rotateForwards = false;
        rotateBackwards = false;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("RotateForward");
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rotateForwards = true;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(rotation!= 0)
    //    {
    //        rotateBackwards = true;
    //    }
    //}

    IEnumerator RotateForward()
    {

        if (rotateForwards && rotation <= 180 && rotateBackwards == false)
        {
            yield return new WaitForSeconds(0.1f);
            rotation += Time.deltaTime * rotationSpeed;
            if(rotation > 180)
            {
                rotateForwards = false;
            }
            rotation = Mathf.Clamp(rotation, 0.1f, 180);
            transform.parent.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        else if (rotateForwards == false && rotation > 0)
        {
            yield return new WaitForSeconds(2);
            rotateBackwards = true;
            StartCoroutine("RotateBack");
        }
    }

    IEnumerator RotateBack()
    {
        if(rotateBackwards && rotation > 0 && rotateForwards==false)
        {
            yield return new WaitForSeconds(0.1f);
            rotation = rotation - Time.deltaTime * rotationSpeed;
            rotation = Mathf.Clamp(rotation, 0, 180);
            transform.parent.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        else
        {
            rotateBackwards = false;
        }
    }
}
