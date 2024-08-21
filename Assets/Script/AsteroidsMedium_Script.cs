using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidsMedium_Script : MonoBehaviour
{
    float speed = 4.5f;
    float direction;

    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, 0, direction);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NaveBullet") || collision.CompareTag("Player"))
        {
            if (Nave_Script.Instance != null)
            {
                Nave_Script.Instance.enemys--;
                Nave_Script.Instance.score += 50;
            }
            Quaternion rotationQuartenion = transform.rotation;
            Quaternion rotation = Quaternion.Slerp(Quaternion.identity, rotationQuartenion, 0.5f);
            Instantiate(enemy, transform.position, rotation);
            Instantiate(enemy, transform.position, Quaternion.Inverse(rotation));
            Destroy(gameObject);

        }
    }
}
