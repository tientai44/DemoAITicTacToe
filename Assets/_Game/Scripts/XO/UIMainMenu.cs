using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    int IndexMapSelected;
    [SerializeField] RectTransform modeUI;
    [SerializeField] float speedMove=100;
    [SerializeField] TMP_InputField inputField;
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
        if (IndexMapSelected < 3)
        {
            GameManager.instance.ChooseMap(IndexMapSelected, GameMode.PVB);
        }
        else
        {
            if (inputField.text.Length > 0)
            {
                int val = int.Parse(inputField.text);
                if (val > 7)
                {
                    GameManager.instance.ChooseMap(IndexMapSelected, GameMode.PVB, int.Parse(inputField.text));
                }
            }
            else
            {
                return;
            }
        }
        gameObject.SetActive(false);
    }
    public void PVPButton()
    {
        if (IndexMapSelected < 3)
        {
            GameManager.instance.ChooseMap(IndexMapSelected, GameMode.PVP);
        }
        else
        {
            if (inputField.text.Length > 0)
            {
                GameManager.instance.ChooseMap(IndexMapSelected, GameMode.PVP, int.Parse(inputField.text));
            }
            else
            {
                return;
            }
        }
        gameObject.SetActive(false);

    }

}
