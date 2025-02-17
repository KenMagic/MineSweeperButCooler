
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;
    public int width = 10;
    public int height = 10;

    public int mines = 10;

    public float cameraSpeed = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetParameters(int width, int height, int mines)
    {
        this.width = width;
        this.height = height;
        this.mines = mines;
    }

    public void SetCameraSpeed(float speed)
    {
        cameraSpeed = speed;
    }

    

}
