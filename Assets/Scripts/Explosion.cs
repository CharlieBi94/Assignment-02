using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] int forceMultiplier = 5;

    Rigidbody2D rbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rbody = collision.GetComponent<Rigidbody2D>();
            collision.GetComponent<PlayerController>().exploding = true;
            Vector2 forceApplied = new Vector2((collision.transform.position.x - transform.position.x) * forceMultiplier, (collision.transform.position.y - transform.position.y) * forceMultiplier/4);
            Debug.Log(forceApplied);
            rbody.AddRelativeForce(forceApplied, ForceMode2D.Impulse);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<PlayerController>().exploding = false;
    }

    private void OnDestroy()
    {
        if (rbody != null)
        {
            rbody.GetComponent<PlayerController>().exploding = false;
        }
    }

}
