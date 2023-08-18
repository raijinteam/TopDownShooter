using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class NavigationMenuAnimation : MonoBehaviour
{
    [Header("All panel")]
    [SerializeField] private GameObject[] all_MenuPanel;



    [SerializeField] private RectTransform[] all_MenusBG; // ALL BUTTONS RECT TRANSFORM
    [SerializeField] private RectTransform[] all_MenuNameTexts;
    [SerializeField] private RectTransform[] all_MenuIcons;

    [SerializeField] private float animationDuration = 0.3f;
    [SerializeField] private float xPositionOffset = 0.2f;

	private void Start()
	{     
       OnClick_MenuActivate(2);     
    }


	//BUTTON CALLBACK FUNCTION CALL WHEN BUTTON PRESS
	public void OnClick_MenuActivate(int index)
    {
        float startingPosition = 0;
        if (UiManager.instance.CanChangeMenus)
        {
            for (int i = 0; i < all_MenusBG.Length; i++)
            {
                //I IS EQULS TO IDEX INCREASE SIZE OF BUTTON AND SET ACTIVE THAT BUTTON
                if (i == index)
                {

                    all_MenusBG[i].DOAnchorMin(new Vector2(startingPosition, all_MenusBG[i].anchorMin.y), animationDuration);
                    startingPosition += xPositionOffset;
                    all_MenusBG[i].DOAnchorMax(new Vector2(startingPosition, 1.2f), animationDuration);
					all_MenuNameTexts[i].DOAnchorPos(new Vector2(all_MenuNameTexts[i].anchoredPosition.x, 80), animationDuration);
					all_MenuIcons[i].DOAnchorMin(new Vector2(all_MenuIcons[i].anchorMin.x, 0.6f), animationDuration);
					all_MenuIcons[i].DOAnchorMax(new Vector2(all_MenuIcons[i].anchorMax.x, 0.9f), animationDuration);
					all_MenuIcons[i].transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), animationDuration);
					all_MenuPanel[i].SetActive(true);

                }
                //DECEREASE SIZE OF ALL OTHER BUTTONS
                else
                {
                    all_MenusBG[i].DOAnchorMin(new Vector2(startingPosition, all_MenusBG[i].anchorMin.y), animationDuration);
                    startingPosition += xPositionOffset - 0.0375f;
                    all_MenusBG[i].DOAnchorMax(new Vector2(startingPosition, .9f), animationDuration);
                    all_MenuNameTexts[i].DOAnchorPos(new Vector2(all_MenuNameTexts[i].anchoredPosition.x, -315f), animationDuration);
                    all_MenuNameTexts[i].DOAnchorMax(new Vector2(all_MenuNameTexts[i].anchorMax.x, 0.1f), animationDuration);
					all_MenuIcons[i].DOAnchorMin(new Vector2(all_MenuIcons[i].anchorMin.x, 0.5f), animationDuration);
					all_MenuIcons[i].DOAnchorMax(new Vector2(all_MenuIcons[i].anchorMax.x, 0.5f), animationDuration);
					all_MenuIcons[i].transform.DOScale(new Vector3(1f, 1f, 1f), animationDuration);
					all_MenuPanel[i].SetActive(false);
                }
            }

            
        }

    }


}
