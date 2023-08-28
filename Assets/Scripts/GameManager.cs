using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] private GameObject player;



    [Header("Require Components")]
    [SerializeField] private Sprite sprite_Skipit;
    [SerializeField] private Sprite sprite_Ads;

    private void Awake()
    {
        if(instance != this || instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void SetSkipitOrAdsAicon(Image _icon )
    {
        if (DataManager.instance.HasAnySkipitUpForGetReward())
        {
            _icon.sprite = sprite_Skipit;
        }
        else
        {
            _icon.sprite = sprite_Ads;
        }
    }



    public GameObject Player
    {
        get
        {
            return player.transform.GetChild(0).gameObject;
        }
    }
}
