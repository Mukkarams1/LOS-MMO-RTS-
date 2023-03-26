using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class UserGiftPrefab : MonoBehaviour
{
    Gift gift;
    [SerializeField]
    Button ClaimButton;
    [SerializeField]
    Button CloseBtn;
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    TextMeshProUGUI description;
    [SerializeField]
    GameObject contentForGifts;
    [SerializeField]
    GameObject giftInventoryPrefab;
    [SerializeField]
    GameObject animPrefab;
    [SerializeField]
    Transform animStartPoint;
    public void SetUI(Gift gift)
    {
        this.gift = gift;
        title.text = gift.title;
        description.text = gift.description;
        PopulateInventory();
    }
    void PopulateInventory()
    {
        foreach (var giftInventory in gift.inventory)
        {
           GameObject giftInventoryObject =  Instantiate(giftInventoryPrefab,contentForGifts.transform);
            giftInventoryObject.GetComponent<GiftInventoryUIPrefab>().SetUI(giftInventory);
        }
    }

    private void Start()
    {
        ClaimButton.onClick.AddListener(ClaimBtnPressed);
        CloseBtn.onClick.AddListener(CloseWindow);
    }
    void ClaimBtnPressed()
    {
        GameManager.instance.creatGiftRedeemAmin( ClaimButton.transform);
        StartCoroutine(SetNotIntrectable());
        ApiController.ConsumeGiftAPI(DataManager.instance.UpdatedTimeAndResourcesCallback, SignInWithTwitter.losAcessToken, gift._id);
        CloseWindow();
    }
    void CloseWindow()
    {
        Destroy(this.gameObject);
    }
    IEnumerator SetNotIntrectable()
    {
        ClaimButton.interactable = false;
        yield return new WaitForSeconds(1f);
        ClaimButton.interactable = true;
    }
    //void createAnimation()
    //{
    //    foreach (var inventroyGift in gift.inventory)
    //    {
            
    //       var prefab = Instantiate(animPrefab, new Vector3(784.849976f, -534, 0),Quaternion.identity);
    //        prefab.GetComponent<giftAnimPrefab>().SetUI(inventroyGift); 
    //    }
    //}
}
