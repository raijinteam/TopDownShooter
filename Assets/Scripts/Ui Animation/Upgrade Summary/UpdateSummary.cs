using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UpdateSummary : MonoBehaviour
{

    [SerializeField] private float increaseCount = 2f;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private CanvasGroup continueButton;

    [SerializeField] private float animationDuration = 0.5f;


    private void Start()
    {
        nameText = gameObject.transform.GetChild(2).gameObject.GetComponent <TextMeshProUGUI> ();
        levelText = gameObject.transform.GetChild(3).gameObject.GetComponent <TextMeshProUGUI> ();
        descriptionText = gameObject.transform.GetChild(4).gameObject.GetComponent <TextMeshProUGUI> ();

        nameText.DOFade(0, 0.01f);
        levelText.DOFade(0, 0.01f);
        descriptionText.DOFade(0, 0.01f);
    }

    public TextMeshProUGUI GetNameText()
    {
        return nameText;
    }

    public TextMeshProUGUI GetLevelText()
    {
        return levelText;
    }

    public TextMeshProUGUI GetDescriptiontext()
    {
        return descriptionText;
    }


    private void DescriptionTextAnimation()
    {
        descriptionText.DOFade(1, animationDuration);
    }


}
