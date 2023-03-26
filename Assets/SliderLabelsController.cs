using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SliderLabelsController : MonoBehaviour
{
    public Slider slider;
    public Text labelHandle;
    Text MaxValue;
    // Start is called before the first frame update
    void Start()
    {
        MaxValue = slider.transform.Find("MaxText").GetComponent<Text>();
        MaxValue.text = slider.maxValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        MaxValue.text = ""+slider.maxValue;
        labelHandle.text = ""+slider.value;
    }
}
