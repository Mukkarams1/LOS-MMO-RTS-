using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DropDownPanel : MonoBehaviour
{
    public Dropdown dropdown;
    public Text selectedCityName;
    public Button nextBtn;

    private void Start()
    {
        nextBtn.onClick.AddListener(onNextClick);
    }
    private void Update()
    {
        if (selectedCityName.text == "Tokyo")
        {
            nextBtn.gameObject.SetActive(true);
        }
        else
        {
            nextBtn.gameObject.SetActive(false);
        }
    }
    void onNextClick()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        SceneManager.LoadScene(2);
    }

}
