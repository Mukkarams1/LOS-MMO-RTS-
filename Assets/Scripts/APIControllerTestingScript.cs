using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TwitterSSO.DataModels.Oauth;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class APIControllerTestingScript : MonoBehaviour
{
    [SerializeField]
    Button TwitterLoginBtn;
    [SerializeField]
    WelcomePanelUI errorPanel;
    public string oAuthToken = "";
    public string oAuthTokenSecret = "";
    public string accessToken = "";
    public string refreshToken = "";
    public GameObject LoadingImage;
    private void OnEnable()
    {
        ApiController.onRefreshTokenGenerationFailed += SignInFaliure;
        ApiController.onRefreshTokenGenerationSuccessful += AccessTokenUpdated;
    }
    private void OnDisable()
    {
        ApiController.onRefreshTokenGenerationFailed -= SignInFaliure;
        ApiController.onRefreshTokenGenerationSuccessful -= AccessTokenUpdated;
    }
    private void Start()
    {
        LoadingImage.SetActive(false);
        SignInWithTwitter.losAcessToken = PlayerPrefs.GetString("losAcessToken");
        SignInWithTwitter.losRefreshToken = PlayerPrefs.GetString("losRefreshToken");
        if (SignInWithTwitter.losAcessToken != "" && SignInWithTwitter.losRefreshToken != "")
        {
            UpdateTimeAndResource();
            TwitterLoginBtn.gameObject.SetActive(false);
        }
        else
        {
            TwitterLoginBtn.gameObject.SetActive(true);
        }
    }
    void turnOnSignInBtn()
    {
        LoadingImage.SetActive(false);
        errorPanel.SetText("error 403", "");
        TwitterLoginBtn.gameObject.SetActive(true);
    }
    void AccessTokenUpdated()
    {
        PlayerPrefs.SetString("losAcessToken", SignInWithTwitter.losAcessToken);
        PlayerPrefs.SetString("losRefreshToken", SignInWithTwitter.losRefreshToken);
        UpdateTimeAndResource();
    }
    void UpdateTimeAndResource()
    {
        LoadingImage.SetActive(true);
        ApiController.ResourceAndTimeUpdateAPI(UpdatedTimeAndResourcesCallback, SignInWithTwitter.losAcessToken);
    }
    public void TestConversionFunction(int num)
    {
        string numFormat = HelperMethods.ConvertToKMBformat(num);
        print(numFormat);
    }
    public void OnButtonClick(int index)
    {

        switch (index)
        {
            case 0:
                LoadingImage.SetActive(true);
                ApiController.SignInApi(callback, oAuthToken, oAuthTokenSecret);
                break;
            case 1:
                ApiController.RefreshAccessTokenApi(callback, refreshToken);
                break;
            case 2:
                ApiController.LogoutApi(callback, accessToken, refreshToken);
                break;
            case 3:
                ApiController.ListAllTroopsAPI(callback, accessToken);
                break;
            case 4:
                ApiController.RecruitTroopAPI(callback, accessToken, "639856dffa0984231db6eb5d", 3);

                break;
            case 5:
                ApiController.ListAllBuildingsAPI(callback, accessToken);

                break;
            case 6:
                ApiController.ConstructBuildingAPI(callback, accessToken, "638ee7ca152315453301d6c0", 5, 5);

                break;
            case 7:
                ApiController.UpdateBuildingAPI(callback, accessToken, "63a5b3df83c923a80053a97a");

                break;
            case 8:
                ApiController.ListAllMailAPI(callback, accessToken);

                break;
            case 9:
                ApiController.UpdateMailStatusAPI(callback, accessToken, "6399a55b4ac79c86cc369414");
                break;
            case 10:
                LoadingImage.SetActive(true);
                ApiController.SignInApi(Signincallback, oAuthToken, oAuthTokenSecret);
                break;
            case 11:
                ApiController.GetServerTimeAPI(callback, oAuthToken);
                break;
            default:
                break;
        }

    }
    void callback(bool success, string response)
    {
        print(success);
        print(response);
    }
    void Signincallback(bool success, string response)
    {
        print(response);

        if (!success)
        {
            LoadingImage.SetActive(false);
            GenericErrorResopnse resp = JsonConvert.DeserializeObject<GenericErrorResopnse>(response);
            SignInFaliure(resp.code.ToString(), resp.msg);
            return;
        }
        print("Sign in successful");
        SignInResponse signInResponseData = JsonConvert.DeserializeObject<SignInResponse>(response);
        SignInWithTwitter.losAcessToken = signInResponseData.accessToken;
        SignInWithTwitter.losRefreshToken = signInResponseData.refreshToken;
        PlayerPrefs.SetString("losAcessToken", SignInWithTwitter.losAcessToken);
        PlayerPrefs.SetString("losRefreshToken", SignInWithTwitter.losRefreshToken);
        DataManager.instance.SetUserData(signInResponseData);
        TimeManager.instance.SetTime(signInResponseData.data.serverTime);

        DataManager.instance.FetchAllBuildingsData();
        DataManager.instance.FetchAllTroopsData();
        DataManager.instance.FetchAllChaptersData();
        StartCoroutine(WaitandLoadScene());

    }
    void SignInFaliure(string code, string msg)
    {
        errorPanel.gameObject.SetActive(true);
        errorPanel.SetText(code, msg);
        print("Sign in failed");
        turnOnSignInBtn();
    }
    public void UpdatedTimeAndResourcesCallback(bool success, string response)
    {
        print("Success Shoaib" + success);
        print("Response Shoaib" + response);
        if (!success) return;
        SignInResponse signInResponseData = JsonConvert.DeserializeObject<SignInResponse>(response);
        TimeManager.instance.SetTime(signInResponseData.data.serverTime);
        DataManager.instance.SetUserData(signInResponseData);
        TimeManager.instance.SetTime(signInResponseData.data.serverTime);
        DataManager.instance.FetchAllBuildingsData();
        DataManager.instance.FetchAllTroopsData();
        DataManager.instance.FetchAllChaptersData();
        StartCoroutine(WaitandLoadScene());
    }

    IEnumerator WaitandLoadScene()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(LoadYourAsyncScene());

    }
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        LoadingImage.SetActive(false);
    }
}
