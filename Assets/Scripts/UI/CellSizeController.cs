
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CellSizeController : MonoBehaviour
{
    public GameObject inputField;
    public GameObject TutoralPanel;

    private bool isTutorialOpen = false;

    public void OpenTutorial()
    {
        isTutorialOpen = true;
        inputField.GetComponent<InputField>().interactable=false;
        TutoralPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        isTutorialOpen = false;
        inputField.GetComponent<InputField>().interactable=true;
        TutoralPanel.SetActive(false);
    }
    public int width = 10;
    public int height = 10;
    public int mines = 10;
    void Start()
    {
        TutoralPanel.SetActive(false);
    }
    public void SetParameters(int width, int height, int mines)
    {
        GameSetting.Instance.SetParameters(width, height,mines);
    }
    public int GetParameters()
    {
        string cellSize = inputField.GetComponent<InputField>().text.ToString();
        int cellSizeInt = 10;
        try
        {
            cellSizeInt = int.Parse(cellSize);
            if(5<=cellSizeInt && cellSizeInt<=32)
            {
                mines = cellSizeInt * cellSizeInt * 1/10;
                return cellSizeInt;
            }
            else
            {
                return 10;
            }
        }
        catch (System.Exception)
        {
            return cellSizeInt;
        }
    }
    public void StartGame()
    {
        if(!isTutorialOpen)
        {
            int size = GetParameters();
        SetParameters(size, size, mines);
        Debug.Log("Width: " + size + " Height: " + size);
        SceneManager.LoadScene("SampleScene");
        } 
    }
}
