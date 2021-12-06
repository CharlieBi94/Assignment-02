using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingPlatform : MonoBehaviour
{
    [SerializeField] Sprite breakingSprite;

    [SerializeField] GameObject platform;

    [SerializeField] GameObject explosion;

    bool isBreaking;
    // Start is called before the first frame update
    void Start()
    {
        isBreaking = false;
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger detected");
        if (collision.CompareTag("Player") && isBreaking == false)
        {
            StartCoroutine("BreakPlatform", collision.gameObject);
        }
    }



    IEnumerator BreakPlatform(GameObject player)
    {
        Debug.Log("BreakPlatfrom");
        isBreaking = true;
        yield return new WaitForSeconds(0.2f);
        platform.GetComponent<SpriteRenderer>().sprite = breakingSprite;
        yield return new WaitForSeconds(1.5f);
        Instantiate(explosion, transform.position, Quaternion.identity);
        player.transform.parent = null;
        Destroy(platform);
    }

}
