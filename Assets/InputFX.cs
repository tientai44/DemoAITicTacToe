using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFX : MonoBehaviour
{
    [SerializeField] ParticleSystem effect;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 inputmouse = Input.mousePosition;
            Instantiate(effect,Camera.main.ScreenToWorldPoint(inputmouse) + new Vector3(0,0,10),Quaternion.identity);
        }
    }
}
