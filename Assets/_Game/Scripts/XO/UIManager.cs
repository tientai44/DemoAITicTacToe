using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject GamePlay;

    private void Awake()
    {
        instance = this;
    }

}
