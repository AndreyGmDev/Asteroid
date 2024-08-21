using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score_Script : MonoBehaviour
{
    int score;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Nave_Script.Instance != null)
        {
            score = Nave_Script.Instance.score;
        }
        text.text = score.ToString();
    }
}
