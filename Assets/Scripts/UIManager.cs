using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Transform CameraTarget;
    public Image FadeImage;

    public Transform cameraStartTrans;
    public Transform CameraFaceTarget;

    public GameObject DressRoom;
    public GameObject ShoesRoom;

    public RectTransform LeftBar;
    public RectTransform RightBar;

    public Toggle toggleFirstColor;

    public GameObject ButtonOk;
    public GameObject NextDayText;


    public void MoveToRoom(RoomType currentRoom)
    { 

        ButtonOk.SetActive(false);

        if (currentRoom == RoomType.Dress)
        {
            FadeImage.DOFade(1, 0.5f).OnComplete(() => MoveToolbars(0f, 0.2f, currentRoom));
            
        }
        else if(currentRoom == RoomType.Shoes)
        {

            LeftBar.DOAnchorMin(new Vector2(1f, 0f), -1f);
            LeftBar.DOAnchorMax(new Vector2(1.2f, 1f), -1f);

            RightBar.gameObject.SetActive(false);
            FadeImage.DOFade(1, 0.5f).SetDelay(1.5f).OnComplete(() => MoveToolbars(0f, 0.2f, currentRoom));
        }
    }

    public void MoveToolbars(float endValue, float duration, RoomType currRoom)
    {
       DressRoom.SetActive(currRoom == RoomType.Dress);
       ShoesRoom.SetActive(currRoom == RoomType.Shoes);

      if(currRoom == RoomType.Shoes)
        {

            LeftBar.DOAnchorMin(new Vector2(0.8f, 0f), 0.3f).SetDelay(2f);
            LeftBar.DOAnchorMax(new Vector2(1f, 1f), 0.3f).SetDelay(2f).OnComplete(() => ButtonOk.SetActive(true));

            FadeImage.DOFade(endValue, duration).SetDelay(0.2f);
        }
        else if (currRoom == RoomType.Dress)
        {
            LeftBar.DOAnchorMin(new Vector2(-0.2f, 0f), 0f);
            LeftBar.DOAnchorMax(new Vector2(0f, 1f), 0f);

            RightBar.gameObject.SetActive(true);
            RightBar.DOAnchorMin(new Vector2(1f, 0f), 0f);
            RightBar.DOAnchorMax(new Vector2(1.2f, 1f), 0f);

            StartCoroutine(ShowNextDay());
        }
        
    }

    IEnumerator ShowNextDay()
    {
        NextDayText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        NextDayText.SetActive(false);

        FadeImage.DOFade(0, 0.2f);


        LeftBar.DOAnchorMin(new Vector2(0f, 0f), 0.3f).SetDelay(0.3f);
        LeftBar.DOAnchorMax(new Vector2(0.2f, 1f), 0.3f).SetDelay(0.3f);


        RightBar.DOAnchorMin(new Vector2(0.8f, 0f), 0.3f).SetDelay(0.3f);
        RightBar.DOAnchorMax(new Vector2(1f, 1f), 0.3f).SetDelay(0.3f).OnComplete(() => ButtonOk.SetActive(true));
    }

}
