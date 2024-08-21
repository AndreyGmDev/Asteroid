using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Tiro_Script : MonoBehaviour
{
    [SerializeField] float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;
        Invoke("Destroy", 0.6f);
    }
    void Destroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        };
    }
}
