using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPanelUI : MonoBehaviour
{
    public Text userNameTxt;
    public Text userLvlTxt;
    public Text foodTxt;
    public Text gunPowderTxt;
    public Text metalTxt;
    public Text woodTxt;
    public Text goldTxt;
    public Text diamondTxt;
    public Button userPanelBtn;
    public Image AvatarImage;
    public Sprite AvatarDefaultSprite;
    public static UserPanelUI instance;
    public static int CurrentUserLvl = 0;

    private void Start()
    {
        userPanelBtn.onClick.AddListener(OnClickUserPanel);
        //  GetSetUserData();
        SetUserData(DataManager.instance.SignInResponseData.data);
    }
    public void OnClickUserPanel()
    {
        SoundManager.instance.PlaySound("ButtonClick");
    }
    public void SetUserData(Data data)
    {
        if (data != null)
        SetUiDetails(data.userName, data.level, ConvertNumberFormats(data.mainInventory.food) ,
           ConvertNumberFormats(data.mainInventory.gunPowder), ConvertNumberFormats(data.mainInventory.metal),
           ConvertNumberFormats(data.mainInventory.wood), ConvertNumberFormats(data.mainInventory.gold), ConvertNumberFormats(data.mainInventory.diamond) );
    }

    private string ConvertNumberFormats(int num)
    {
        if (num >= 100000000)
            return (num / 1000000).ToString("#,0M");

        if (num >= 10000000)
            return (num / 1000000).ToString("0.#") + "M";

        if (num >= 100000)
            return (num / 1000).ToString("#,0K");

        if (num >= 10000)
            return (num / 1000).ToString("0.#") + "K";

        return num.ToString("#,0");
    }

    private void SetUiDetails(string Name, int lvl, string food, string gunpowder, string metal, string wood, string gold, string diamond)
    {
        AvatarImage.sprite = AvatarDefaultSprite;
        userNameTxt.text = Name;
        userLvlTxt.text = "Level: " + lvl.ToString();
        foodTxt.text = food.ToString();
        gunPowderTxt.text = gunpowder.ToString();
        metalTxt.text = metal.ToString();
        woodTxt.text = wood.ToString();
        goldTxt.text = gold.ToString();
        diamondTxt.text = diamond.ToString();
    }
    //void GetSetUserData()
    //{
    //    for (int i = 0; i < DataManager.instance.userData.Count; i++)
    //    {
    //        if(PlayerPrefs.GetString("UserEmail") == DataManager.instance.userData[i].email)
    //        {
    //            PlayerPrefs.SetInt("CurrentLvl", DataManager.instance.userData[i].level);
    //            SetUserData(DataManager.instance.userData[i]);
    //        }
    //    }
    //}
}
