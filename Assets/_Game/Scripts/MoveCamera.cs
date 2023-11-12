using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public static MoveCamera Instance;
    public float sensitivity = 2.0f;
    private Vector3 intialPos;
    private void Awake()
    {
        Instance = this;
        intialPos = transform.position;
    }
    public void OnInit()
    {
        transform.position = intialPos;
    }

    void Update()
    {
        // Ki?m tra khi chu?t ph?i ???c gi?
        if (Input.GetMouseButton(1))
        {
            // L?y giá tr? di chuy?n c?a chu?t
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Di chuy?n camera theo h??ng vu?t chu?t ph?i
            transform.Translate(new Vector3(-mouseX, -mouseY, 0) * sensitivity, Space.World);
        }
    }
}
