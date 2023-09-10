using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    int IndexMapSelected;
    [SerializeField] RectTransform modeUI;
    [SerializeField] float speedMove=100;

    public void NextMap()
    {
        if (IndexMapSelected < modeUI.childCount-1)
        {
            IndexMapSelected++;
            //modeUI.anchoredPosition -= Vector2.right * 650;
            float posX = -modeUI.GetChild(IndexMapSelected).GetComponent<RectTransform>().anchoredPosition.x;
            StartCoroutine(IEMoveModeUI(new Vector2(posX, modeUI.anchoredPosition.y)));
        }
    }
    public void PrevMap()
    {
        if (IndexMapSelected > 0)
        {
            IndexMapSelected--;
            //modeUI.anchoredPosition += Vector2.right * 650;
            float posX = -modeUI.GetChild(IndexMapSelected).GetComponent<RectTransform>().anchoredPosition.x;
            StartCoroutine(IEMoveModeUI(new Vector2(posX, modeUI.anchoredPosition.y)));
        }
    }
    IEnumerator IEMoveModeUI(Vector2 destination)
    {
        for(int i = 0; i < 1000; i++)
        {
            modeUI.anchoredPosition = Vector2.MoveTowards(modeUI.anchoredPosition, destination, speedMove* Time.fixedDeltaTime);
            if (destination == modeUI.anchoredPosition)
            {
                yield break;
            }
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            
        }
    }
    public void PVBButton()
    {
        GameManager.instance.ChooseMap(IndexMapSelected,GameMode.PVB);
        gameObject.SetActive(false);
    }
    public void PVPButton()
    {
        GameManager.instance.ChooseMap(IndexMapSelected,GameMode.PVP);
        gameObject.SetActive(false);
    }
    public void OnClickHost()
    {
        NetworkManager.Singleton.StartHost();
        GameManager.instance.ChooseMap(IndexMapSelected, GameMode.PVPO);
        gameObject.SetActive(false);
    }

    public void OnClickJoin()
    {
        StartCoroutine(Join());
        GameManager.instance.ChooseMap(IndexMapSelected, GameMode.PVPO);
        gameObject.SetActive(false);
    }
    private IEnumerator Join()
    {
        NetworkManager.Singleton.StartClient();
        yield return null;

    }
}
