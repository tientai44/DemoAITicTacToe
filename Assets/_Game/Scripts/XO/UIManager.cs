using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => NetworkManager.Singleton.SceneManager != null);
        PanelLoading.Instance.Init();
    }
   

}
