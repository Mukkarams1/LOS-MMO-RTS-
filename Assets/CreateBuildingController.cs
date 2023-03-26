using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBuildingController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Text label;
    public RectTransform contentPanel;
    Transform[] scrollViewChildren;
    int currentscroll = 2;
    int currentBuildingType = 0;
    string[] BuildingTypes = { "Gathering Buildings", "Troops Buildings", "Research & Others" };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        contentPanel.anchoredPosition =
                (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
                - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
    }

    public void scrollnext() {
        currentscroll++;
        if (currentscroll > (contentPanel.childCount - 3)) {
            currentscroll = 2;
        }
        RectTransform tr = contentPanel.transform.GetChild(currentscroll).gameObject.GetComponent<RectTransform>();
        SnapTo(tr);

    }
    public void scrollprev()
    {
        currentscroll--;
        if (currentscroll <2)
        {
            currentscroll = contentPanel.childCount - 3;
        }
        RectTransform tr = contentPanel.transform.GetChild(currentscroll).gameObject.GetComponent<RectTransform>();
        SnapTo(tr);
    }

    public void BuildingNext() {
        currentBuildingType++;
        if (currentBuildingType >= BuildingTypes.Length)
            currentBuildingType = 0;
        label.text = BuildingTypes[currentBuildingType];

    }
    public void BuildingPrev()
    {
        currentBuildingType--;
        if (currentBuildingType < 0)
            currentBuildingType = BuildingTypes.Length-1;
        label.text = BuildingTypes[currentBuildingType];

    }
}
