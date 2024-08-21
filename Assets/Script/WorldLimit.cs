using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLimit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("NaveBullet"))
        {
            if (collision.transform.position.x <= -17.75)
            {
                collision.transform.position = new Vector3(17.775f + (collision.transform.localScale.y), transform.position.y, 0);
            }
            else if (collision.transform.position.x >= 17.75)
            {
                collision.transform.position = new Vector3(-(17.775f + (collision.transform.localScale.y)), transform.position.y, 0);
            }
            else if (collision.transform.position.y <= -10.0f)
            {
                collision.transform.position = new Vector3(transform.position.x, 10.0f + (collision.transform.localScale.x), 0);
            }
            else if (collision.transform.position.y >= 10.0f)
            {
                collision.transform.position = new Vector3(transform.position.x, -(10.0f + (collision.transform.localScale.x)), 0);
            }
        }     
    }
   
}
