using UnityEngine;
using UnityEngine.UI;

public class SwipeControlle : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private int pageCount;
    [SerializeField] private float gapBetweenImage;
    [SerializeField] private bool release;
    [SerializeField] private float targetPos;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float minThreshold;

    private int currentPageIndex;


    private void Start()
    {
        gapBetweenImage = 1f / (pageCount - 1f);
        print("Page count " + pageCount);
        print(gapBetweenImage);
    }

    private void Update()
    {
        if (release)
        {
            scrollView.horizontalNormalizedPosition = Mathf.Lerp(scrollView.horizontalNormalizedPosition, targetPos, 0.1f);
        }
    }

    public void OnBeginDrag()
    {
        release = false;
    }


    public void OnEndDrag()
    {
        if(scrollView.velocity.x > minThreshold || targetPos - scrollView.horizontalNormalizedPosition > gapBetweenImage * 0.5f)
        {
            if (targetPos > 0f) 
            {
                targetPos -= gapBetweenImage;
                currentPageIndex++;
            }
        }else if(scrollView.velocity.x < minThreshold * -1 || scrollView.horizontalNormalizedPosition - targetPos > gapBetweenImage * 0.5f)
        {
            if(targetPos < 1f)
            {
                targetPos += gapBetweenImage;
                currentPageIndex--;
            }
        }

        scrollView.velocity = Vector2.zero;
        release = true;
    }



}
