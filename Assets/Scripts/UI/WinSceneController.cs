using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinSceneController : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    private void Start()
    {
        text.text = "";
        text.text += "You win!";
        text.text += "\n";
        text.text += "Cell size: ";
        text.text += GameSetting.Instance.width + "x" + GameSetting.Instance.height;
        text.text += "\n";
        text.text += "Mines: ";
        text.text += GameSetting.Instance.mines;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    

}
