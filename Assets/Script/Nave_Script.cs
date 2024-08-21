using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Nave_Script : MonoBehaviour
{

    float Speed;

    [SerializeField] float tiroDelay = 1;
    float tiroDelayContador = 0;
    
    public ParticleSystem ParticleSystem;
    public GameObject projectilePrefab; // Referência ao prefab do tiro
    public Transform firePoint; // Ponto de origem do tiro

    public Rigidbody2D rdb;

    float sizeX;
    float sizeY;

    Renderer Renderer;
    bool stopMove = false;
    bool invunerability=false;
    float pisca=0.1f;

    public SpriteRenderer fogo;
    // Singleton
    public static Nave_Script Instance { get; private set; }

    public Transform playerTransform;
    public int health=3;
    public int score;

    [SuppressMessage("CodeQuality", "IDE0079:Remove unused variable", Justification = "Variável reservada para uso futuro")]
    public int enemys; // Numero de Asteroids no Mapa
    int enemiesX;
    private void Awake()
    {
        // Destruir este objeto, se uma instância já existe e não é este objeto
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            playerTransform = transform; // Configura a posição do jogador
        }
    }


    void Start()
    {
        sizeX = 1.75f; //2.836f
        sizeY = 1.5f; //2.77f
        Renderer = GetComponent<Renderer>();
        enemiesX = enemys;
    }

    // Update is called once per frame
    void Update()
    {    
        if (stopMove == false)
        {
            Move();
            Tiro();
        }
        else
        {
            fogo.enabled = false;
        }


        if (invunerability == true)
        {
            pisca -= Time.deltaTime;
            if (pisca < 0)
            {
                Renderer.enabled = false;
                pisca = 0.2f;
            }
            else if (pisca < 0.1)
            {
                Renderer.enabled = true;
            }
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rdb.AddRelativeForce(Vector2.up * Speed);
            if(Speed <= 3)
            Speed += 0.0005f;
            fogo.enabled = true;
        }
        else
        {   
            fogo.enabled = false;
            if (Speed<=1)
                Speed = 1.5f;
            else
                Speed -= 0.3f;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles += new Vector3(0, 0, -200) * Time.deltaTime;
        };
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles += new Vector3(0, 0, 200) * Time.deltaTime;
        };
    }

    void Tiro()
    {
        
        if (tiroDelayContador <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space))
            {
                GameObject projectile1 = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                ParticleSystem.Emit(1);
                tiroDelayContador = tiroDelay;
            };
        }
        else
        {
            tiroDelayContador -= Time.deltaTime;
        };
        
    }

    void Invulnerability()
    {
        Renderer.enabled = true;
        stopMove = false;
        invunerability = true;
        
        Invoke("Default", 2f);
    }
    void Default()
    {
        gameObject.layer = 0;
        invunerability = false;
        Renderer.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            health --;
            gameObject.layer = 6;
            Renderer.enabled = false;
            transform.position = Vector3.zero;
            stopMove = true;
            transform.rotation = Quaternion.identity;
            rdb.velocity = Vector3.zero;

            Invoke("Invulnerability", 1.5f);
            if (health <= 0)
            {
               Destroy(gameObject);
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WorldLimit"))
        {
            if (transform.position.x <= -17.75 && transform.position.y <= -10.0f)
            {
                transform.position = new Vector3(17.775f + 0.55f, 10.0f + 0.41f, 0);
            }
            else if (transform.position.x >= 17.75 && transform.position.y <= -10.0f)
            {
                transform.position = new Vector3(-(17.775f + 0.55f), 10.0f + 0.41f, 0);
            }
            else if (transform.position.x <= -17.75 && transform.position.y >= 10.0f)
            {
                transform.position = new Vector3(17.775f + 0.55f, -(10.0f + 0.41f), 0);
            }
            else if (transform.position.x >= 17.75 && transform.position.y >= 10.0f)
            {
                transform.position = new Vector3(-(17.775f + 0.55f), -(10.0f + 0.41f), 0);
            }
            else if (transform.position.x <= -17.75)
            {
                transform.position = new Vector3(17.775f + (sizeY / 2), transform.position.y, 0);
            }
            else if (transform.position.x >= 17.75)
            {
                transform.position = new Vector3(-(17.775f + (sizeY / 2)), transform.position.y, 0);
            }
            else if (transform.position.y <= -10.0f)
            {
                transform.position = new Vector3(transform.position.x, 10.0f + (sizeX / 2), 0);
            }
            else if (transform.position.y >= 10.0f)
            {
                transform.position = new Vector3(transform.position.x, -(10.0f + (sizeX / 2)), 0);
            }

        }

    }

}
