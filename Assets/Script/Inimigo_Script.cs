using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Inimigo_Script : MonoBehaviour
{
    
    [SerializeField] float Speed = 10; // Velocidade do Inimigo

    [SerializeField] Transform firePoint; // Posição do Tiro Inimigo
    [SerializeField] GameObject projectileEnemyPrefab; // Tiro Inimigo
    [SerializeField] float tiroDelay; // Timer Tiro Inimigo
    float tiroDelayContador;

    Transform playerTransform; // Transform do Player
    float playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (Nave_Script.Instance != null)
        {
            playerTransform = Nave_Script.Instance.playerTransform;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        playerPosition = playerTransform.transform.position.x;
        Down();
        if (transform.position.y <= 8)
        {
            Move();
            Tiro();
        } 
    }

    void Move()
    {
        if(transform.position.x < playerPosition) 
        {
            transform.position += new Vector3(1,0,0) * Time.deltaTime * Speed;
        }
        else if (transform.position.x > playerPosition)
        {
            transform.position -= new Vector3(1, 0, 0) * Time.deltaTime * Speed;
        }
        else if(transform.position.x == playerPosition)
        {
            return;
        };   
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.CompareTag("NaveBullet"))
        {
            Destroy(gameObject);
        }; 
    }

    void Tiro()
    {
        if (tiroDelayContador <= 0)
        {
            if (transform.position.x < (playerPosition + 1.3) && transform.position.x > (playerPosition - 1.3))
            {
                GameObject ProjetilEnemy = Instantiate(projectileEnemyPrefab, firePoint.position, firePoint.rotation);
                tiroDelayContador = tiroDelay;
            }
        }
        else
        {
            tiroDelayContador -= Time.deltaTime;
        };
        
    }

    void Down()
    {
        if (transform.position.y > 8)
        {
            transform.position += new Vector3(0,-1, 0) * Time.deltaTime * Speed;
        }
    }
}
