using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInformation : MonoBehaviour
{
    [SerializeField] private Image infoLabel; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnShowToolTipButtonCallback()
    {
        infoLabel.gameObject.SetActive(true);
    }

    public void OnHideToolTipButtonCallback()
    {
        infoLabel.gameObject.SetActive(false);
    }
}
