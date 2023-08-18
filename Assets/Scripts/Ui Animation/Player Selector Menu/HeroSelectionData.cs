using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroSelectionData : MonoBehaviour
{
    public Image img_BG;
    public Image img_Glow;
    public Image img_Hero;
    public Image img_SelectedBG;
    public Image img_Lock;
    public TextMeshProUGUI txt_currentHeroLevel;
    public Image img_upgradeAvaliable;
    public Slider slider_cards;
    public Image img_SliderBG;
    public TextMeshProUGUI txt_Cards;


    public void SetPlayerBGForItsType(Sprite _BG , Color _glowColor)
    {
        img_BG.sprite = _BG;
        img_Glow.color = _glowColor;
    }
}
