using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        CameraMovement();
    }
    private void Zoom()
    {
        Vector3 position = Camera.main.transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float orthographicSize = Camera.main.orthographicSize;
        if (Input.mouseScrollDelta.y > 0)
        {
            Camera.main.orthographicSize = Mathf.Max(orthographicSize - 1, 1); // Prevent zooming out too much
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            Camera.main.orthographicSize = orthographicSize + 1;
        }
        Camera.main.transform.position = position + (mousePosition - Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    private void CameraMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.position += Vector3.up * Time.deltaTime * GameSetting.Instance.cameraSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.position += Vector3.down * Time.deltaTime * GameSetting.Instance.cameraSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.position += Vector3.left * Time.deltaTime * GameSetting.Instance.cameraSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.position += Vector3.right * Time.deltaTime * GameSetting.Instance.cameraSpeed;
        }
    }
}
