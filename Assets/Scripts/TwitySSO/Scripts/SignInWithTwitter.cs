using TwitterSSO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections.Generic;

public class SignInWithTwitter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Button TwitterLoginBtn;
    public Text verifierText;
    [SerializeField]
    GameObject LoadingImage;
    [SerializeField]
    WelcomePanelUI errorPanel;
    public static string losAcessToken = string.Empty;
    public static string losRefreshToken = string.Empty;
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
    void AccessTokenUpdated()
    {
        PlayerPrefs.SetString("losAcessToken", SignInWithTwitter.losAcessToken);
        PlayerPrefs.SetString("losRefreshToken", SignInWithTwitter.losRefreshToken);
        UpdateTimeAndResource();
    }
    [System.Obsolete]
    void Start()
    {
        LoadingImage.SetActive(false);
        losAcessToken = PlayerPrefs.GetString("losAcessToken");
        losRefreshToken = PlayerPrefs.GetString("losRefreshToken");
        if (losAcessToken != "" && losRefreshToken != "")
        {
            UpdateTimeAndResource();
            TwitterLoginBtn.gameObject.SetActive(false);
        }
        else
        {
            TwitterSSO.Oauth.consumerKey = "yvZYvCGvmFkrGD2MEBumkUgp2";
            TwitterSSO.Oauth.consumerSecret = "dkV62prCinZMvHDGcLzsvqbBbORbAlIOAgygvC22SlBNkIlakZ";
            TwitterSSO.Oauth.accessToken = "3377656685-LNYA7syyJqsrEfESdPzUZOCHRfJHE4QYefnxKta";
            TwitterSSO.Oauth.accessTokenSecret = "xAUrrBkFJgu7g2rqHacGmv7sH0eLioB2hp4Xgl7uDBIlX";
            //TwitterSSO.Oauth.callbackURL = "https://dawahsoft.com/";                                       // for testing only
            TwitterSSO.Oauth.callbackURL = "unitydl://testlink";                                         /// orignal
            TwitterLoginBtn.gameObject.SetActive(true);
            TwitterLoginBtn.onClick.AddListener(OnTwitterLoginBtnClicked);
        }
    }
    void UpdateTimeAndResource()
    {
        LoadingImage.SetActive(true);
        ApiController.ResourceAndTimeUpdateAPI(UpdatedTimeAndResourcesCallback, losAcessToken);
    }
    void OnTwitterLoginBtnClicked()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        LoadingImage.SetActive(true);
        StartCoroutine(TwitterSSO.TwitterRequest.GenerateRequestToken(CallbackGenerateRequestToken, TwitterSSO.Oauth.callbackURL));

    }
    void CallbackGenerateRequestToken(bool success)
    {
        if (!success) return;

        StartCoroutine(TwitterSSO.TwitterRequest.AuthenticateRequestToken(CallbackAuthenticateRequestToken, TwitterSSO.Oauth.authenticateURL));

        print(" Token Generation Successful ");
    }
    void CallbackAuthenticateRequestToken(bool success, string response)
    {
        if (!success) return;

        print(" Token Authenticated Successfully ");
        print(response);


    }



    public void AccessTokenGenerator(string pin)
    {
        Debug.Log("access token generator");
        StartCoroutine(TwitterSSO.TwitterRequest.GenerateAccessToken(pin, CallbackAccessoken));
    }
    void CallbackAccessoken(bool success, string name)
    {
        if (!success) return;
        verifierText.text = name;
       
        Debug.Log("access token generated");
        ApiController.SignInApi(CallbackSigninApi, TwitterSSO.Oauth.accessToken, TwitterSSO.Oauth.accessTokenSecret);
    }
    void CallbackSigninApi(bool success, string response)
    {
        LoadingImage.SetActive(false);
        if (!success)
        {
            LoadingImage.SetActive(false);
            GenericErrorResopnse resp = JsonConvert.DeserializeObject<GenericErrorResopnse>(response);
            SignInFaliure(resp.code.ToString(),resp.msg);
            print("Sign in failed");
            return;
        }
        print("signin successful");
        print(response);
        SignInResponse signInResponseData = JsonConvert.DeserializeObject<SignInResponse>(response);
        verifierText.text = signInResponseData.data._id;
        losAcessToken = signInResponseData.accessToken;
        losRefreshToken = signInResponseData.refreshToken;
        PlayerPrefs.SetString("losAcessToken", losAcessToken);
        PlayerPrefs.SetString("losRefreshToken", losRefreshToken);
        DataManager.instance.SetUserData(signInResponseData);
        TimeManager.instance.SetTime(signInResponseData.data.serverTime);
        DataManager.instance.FetchAllBuildingsData();
        DataManager.instance.FetchAllTroopsData();
        DataManager.instance.FetchAllChaptersData();
        PlayerPrefs.SetString("UserEmail", "himari@gmail.com");
        SceneManager.LoadScene(1);
    }
    public void UpdatedTimeAndResourcesCallback(bool success, string response)
    {
        print(success);
        print(response);
        if (!success)
            return;
        SignInResponse signInResponseData = JsonConvert.DeserializeObject<SignInResponse>(response);
        TimeManager.instance.SetTime(signInResponseData.data.serverTime);
        DataManager.instance.SetUserData(signInResponseData);
        TimeManager.instance.SetTime(signInResponseData.data.serverTime);
        DataManager.instance.FetchAllBuildingsData();
        DataManager.instance.FetchAllTroopsData();
        DataManager.instance.FetchAllChaptersData();
    }
    void SignInFaliure(string code, string msg)
    {
        errorPanel.gameObject.SetActive(true);
        errorPanel.SetText(code, msg);
        print("Sign in failed");
        turnOnSignInBtn();
    }

    void turnOnSignInBtn()
    {
        LoadingImage.SetActive(false);
        TwitterLoginBtn.gameObject.SetActive(true);
    }

}