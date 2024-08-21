using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar_Script : MonoBehaviour
{
    public TextMeshProUGUI gameOver;
    int health;
    public int healthMax;
    void Update()
    {
        if (Nave_Script.Instance != null)
        {
            health = Nave_Script.Instance.health;
        }
        else
        {
            gameOver.enabled = true;
            Destroy(gameObject);
        }
            

        if (health < healthMax)
        {
            Destroy(gameObject);
        }
    }
}
