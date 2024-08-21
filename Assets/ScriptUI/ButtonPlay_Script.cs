using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPlay_Script : MonoBehaviour
{
    bool goToPlayScene=false;
    public bool inFirstScene;
    // Start is called before the first frame update
    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }
    private void Update()
    {
        if (!inFirstScene)
        {
            if (Nave_Script.Instance == null)
            {
                if (!goToPlayScene)
                {
                    Invoke("PlayScene", 2f);
                    goToPlayScene = true;
                }
            }
        }
    }
    void PlayScene()
    {
        SceneManager.LoadScene(0);
    }

}
