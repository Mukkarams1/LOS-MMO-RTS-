using System.Collections;
using System.Collections.Generic;
using TwitterSSO;
using TwitterSSO.DataModels.Oauth;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Net;

public class ApiController
{
    static string baseURL = "https://apilos.herokuapp.com/api/v1/";
    static string signInURL = "user/auth/signin";
    static string refreshTokenURL = "user/auth/get/accesstoken";
    static string logOutURL = "user/auth/logout";
    static string listAllTroopsURL = "user/troop/index?page=1&limit=40";
    static string recruitTroopURL = "user/troop/add/";
    static string listAllBuildingURL = "user/building/index?page=1&limit=40";
    static string constructBuildingURL = "user/building/create/";
    static string upgradeBuildingURL = "user/building/upgrade/";
    static string listAllMailURL = "user/mail/index?page=1&limit=40";
    static string updateMailStatusURL = "user/mail/update/";
    static string getServerTimeURL = "user/auth/get/servertime";
    static string resourceAndTimeUpdateURL = "user/update/all";
    static string getAllChaptersURL = "user/chapter/index?page=1&limit=40";
    static string addCompletedTaskURL = "user/chapter/task/complete/";
    static string addWareHouseInventoryURL = "user/collect/";
    static string consumeGiftURL = "user/gift/consume/";
    static string speedUpURL = "user/powerup/speedup/";
    static string boosterURL = "user/powerup/booster/";

    public delegate void userApiCallback(bool success, string response);
    public static Action<string, string> onRefreshTokenGenerationFailed;
    public static Action onRefreshTokenGenerationSuccessful;

    public static async void SignInApi(userApiCallback callback, string oauthToken, string oauthTokenSecret)
    {
        string url = baseURL + signInURL;
        WWWForm form = new WWWForm();
        // form.AddField("userName", TwitterRequest.screenName);
        form.AddField("userName", "あいか");
        // form.AddField("deviceToken", TwitterRequest.deviceIdentifier);
        // form.AddField("deviceToken", "");
        form.AddField("deviceToken", "fRw7tS6YTWeNpyNRRSFkT8:APA91bHucuP-w3KXNxDEsbnIW3puDxY2qbF7l8BjCX5JP3W11ArT0RKKFZqYsZkg_iJVTmghWe08vhq4iqchmz31gVelEEcHhF5--ZyiwiO0RoMkVLyRXKLu4DRXWn8PZ8hA7747vJg6");
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("X-Access-Token", "3377656685-LNYA7syyJqsrEfESdPzUZOCHRfJHE4QYefnxKta");
        reqParameters.Add("X-Access-Token-Secret", "xAUrrBkFJgu7g2rqHacGmv7sH0eLioB2hp4Xgl7uDBIlX");
        await POSTRequest(url, reqParameters, form, callback);
    }
    public static async void RefreshAccessTokenApi(userApiCallback callback, string refreshToken)
    {
        string url = baseURL + refreshTokenURL;
        WWWForm form = new WWWForm();
        form.AddField("refreshToken", refreshToken);
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        await POSTRequest(url, reqParameters, form, callback, true);
    }
    public static async void LogoutApi(userApiCallback callback, string accessToken, string refreshToken)
    {
        string url = baseURL + logOutURL;
        WWWForm form = new WWWForm();
        form.AddField("refreshToken", refreshToken);
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await POSTRequest(url, reqParameters, form, callback);
    }
    public static async void ListAllTroopsAPI(userApiCallback callback, string accessToken, int page = 1)
    {
        string url = baseURL + listAllTroopsURL + page.ToString();
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await GETRequest(url, reqParameters, callback);
    }
    public static async void RecruitTroopAPI(userApiCallback callback, string accessToken, string id, int quantity = 1)
    {
        string url = baseURL + recruitTroopURL + id + "/" + quantity;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await POSTRequest(url, reqParameters, form, callback);
    }
    public static async void ListAllBuildingsAPI(userApiCallback callback, string accessToken, int page = 1)
    {
        string url = baseURL + listAllBuildingURL + page.ToString();
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await GETRequest(url, reqParameters, callback);
    }
    public static async void ConstructBuildingAPI(userApiCallback callback, string accessToken, string id, int coordinateX, int coordinateY)
    {
        string url = baseURL + constructBuildingURL + id + "/" + coordinateX + "/" + coordinateY;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await POSTRequest(url, reqParameters, form, callback);
    }
    public static async void UpdateBuildingAPI(userApiCallback callback, string accessToken, string id)
    {
        string url = baseURL + upgradeBuildingURL + id;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await POSTRequest(url, reqParameters, form, callback);
    }
    public static async void SpeedUpBuildingAPI(userApiCallback callback, string accessToken, string buildingID, string inventoryID, int Quantity)
    {
        string url = baseURL + speedUpURL + buildingID + "/" + inventoryID + "/" + Quantity;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await POSTRequest(url, reqParameters, form, callback);
    }
    public static async void BuildingBoosterAPI(userApiCallback callback, string accessToken, string inventoryID)
    {
        string url = baseURL + boosterURL + inventoryID;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await POSTRequest(url, reqParameters, form, callback);
    }
    public static async void ListAllMailAPI(userApiCallback callback, string accessToken, int page = 1)
    {
        string url = baseURL + listAllMailURL + page.ToString();
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await GETRequest(url, reqParameters, callback);
    }
    public static async void UpdateMailStatusAPI(userApiCallback callback, string accessToken, string id)
    {
        string url = baseURL + updateMailStatusURL + id;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await PUTRequest(url, "", reqParameters, callback);
    }
    public static async void GetServerTimeAPI(userApiCallback callback, string accessToken)
    {
        string url = baseURL + getServerTimeURL;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await GETRequest(url, reqParameters, callback);
    }
    public static async void ResourceAndTimeUpdateAPI(userApiCallback callback, string accessToken)
    {
        string url = baseURL + resourceAndTimeUpdateURL;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await POSTRequest(url, reqParameters, form, callback);
    }

    public static async void GetAllChaptersAPI(userApiCallback callback, string accessToken)
    {
        string url = baseURL + getAllChaptersURL;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await GETRequest(url, reqParameters, callback);
    }
    public static async void AddCompletedTaskAPI(userApiCallback callback, string accessToken, string chapterID, string taskID)
    {
        string url = baseURL + addCompletedTaskURL + chapterID + "/" + taskID;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await POSTRequest(url, reqParameters, form, callback);
    }
    public static async void AddWarehouseInventoryAPI(userApiCallback callback, string accessToken, string itemID, int quantity)
    {
        string url = baseURL + addWareHouseInventoryURL + itemID + "/" + quantity;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await PUTRequest(url, "", reqParameters, callback);
    }
    public static async void ConsumeGiftAPI(userApiCallback callback, string accessToken, string giftID)
    {
        string url = baseURL + consumeGiftURL + giftID;
        Dictionary<string, string> reqParameters = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        reqParameters.Add("Content-Type", "application/x-www-form-urlencoded");
        reqParameters.Add("Authorization", "Bearer " + accessToken);
        await PUTRequest(url, "", reqParameters, callback);
    }
    static async Task GETRequest(string url, Dictionary<string, string> reqHeaders, userApiCallback callback)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        foreach (var item in reqHeaders)
        {
            req.SetRequestHeader(item.Key, item.Value);
        }
        req.SendWebRequest();
        while (!req.isDone)
        {
            await Task.Yield();
        }

        if (!string.IsNullOrEmpty(req.error))
        {
            // call Refresh API, if error is forbidden or unauthorization else Show error message
            if ( req.responseCode == (int)ResponseCodes.forbiddenAccess || req.responseCode == (int)ResponseCodes.unAuthorizedAccess)
            {
                // it means forbidden access and need to refresh token.
                RefreshAccessTokenApi(RefreshTokenCallback, SignInWithTwitter.losRefreshToken);
                callback(false, req.downloadHandler.text);
            }      
            else 
            {
                callback(false, req.downloadHandler.text);
            }
        }
        else
        {
            if (req.responseCode == (int)ResponseCodes.successOne || req.responseCode == (int)ResponseCodes.successTwo)
            {
                callback(true, req.downloadHandler.text);
            }
            else
            {
                callback(false, req.downloadHandler.text);
                Debug.Log("yaha ara a");
            }
        }
    }
    static async Task POSTRequest(string url, Dictionary<string, string> reqHeaders, WWWForm form, userApiCallback callback, bool isRefreshAPI = false)
    {
        UnityWebRequest req = UnityWebRequest.Post(url, form);
        foreach (var item in reqHeaders)
        {
            req.SetRequestHeader(item.Key, item.Value);
        }
        req.SendWebRequest();

        while (!req.isDone)
        {
            await Task.Yield();
        }

        if (!string.IsNullOrEmpty(req.error))
        {
            //if the error is not from refresh API then call Refresh API, else if error is on Refresh API then you need real help bro, Show error message
            if (!isRefreshAPI && req.responseCode == (int)ResponseCodes.forbiddenAccess || req.responseCode == (int)ResponseCodes.unAuthorizedAccess)
            {
                // it means forbidden access and need to refresh token.
                RefreshAccessTokenApi(RefreshTokenCallback, SignInWithTwitter.losRefreshToken);
                callback(false, req.downloadHandler.text);
            }
            else if (!isRefreshAPI)
            {
                callback(false, req.downloadHandler.text);
            }
            else if (isRefreshAPI)
            {
                callback(false, req.downloadHandler.text);
            }
        }
        else
        {
            if (req.responseCode == (int)ResponseCodes.successOne || req.responseCode == (int)ResponseCodes.successTwo)
            {
                callback(true, req.downloadHandler.text);
            }
            else
            {
                callback(false, req.downloadHandler.text);
                Debug.Log("yaha arha hauu");
            }
        }
    }
    static async Task PUTRequest(string url, string bodyData, Dictionary<string, string> reqHeaders, userApiCallback callback)
    {
        UnityWebRequest req = UnityWebRequest.Put(url, bodyData);
        foreach (var item in reqHeaders)
        {
            req.SetRequestHeader(item.Key, item.Value);
        }
        req.SendWebRequest();
        while (!req.isDone)
        {
            await Task.Yield();
        }
        if (!string.IsNullOrEmpty(req.error))
        {
            // call Refresh API, if error is forbidden or unauthorization else Show error message
            if (req.responseCode == (int)ResponseCodes.forbiddenAccess || req.responseCode == (int)ResponseCodes.unAuthorizedAccess)
            {
                // it means forbidden access and need to refresh token.
                RefreshAccessTokenApi(RefreshTokenCallback, SignInWithTwitter.losRefreshToken);
                callback(false, req.downloadHandler.text);
            }
            else
            {
                callback(false, req.downloadHandler.text);
            }
        }
        else
        {
            if (req.responseCode == (int)ResponseCodes.successOne || req.responseCode == (int)ResponseCodes.successTwo)
            {
                callback(true, req.downloadHandler.text);
            }
            else
            {
                callback(false, req.downloadHandler.text);
                Debug.Log("yaha pe ara a");

            }
        }
    }
    private static void RefreshTokenCallback(bool success, string response)
    {
        RefreshTokenResponse refreshTokenResponse = JsonConvert.DeserializeObject<RefreshTokenResponse>(response);
        if (!refreshTokenResponse.status && (refreshTokenResponse.code == (int)ResponseCodes.unAuthorizedAccess || refreshTokenResponse.code == (int)ResponseCodes.forbiddenAccess))
        {
            // need to show sign in button 
            onRefreshTokenGenerationFailed?.Invoke(refreshTokenResponse.code.ToString(), refreshTokenResponse.msg);
        }
        else if (refreshTokenResponse.status)
        {
            SignInWithTwitter.losAcessToken = refreshTokenResponse.accessToken;
            SignInWithTwitter.losRefreshToken = refreshTokenResponse.refreshToken;
            onRefreshTokenGenerationSuccessful?.Invoke();
        }
    }

        public static bool IsConnectedToInternet(bool quick = true)
        {
            if (quick)
                return Application.internetReachability != NetworkReachability.NotReachable;

            try
            {
                using (WebClient client = new WebClient())
                using (client.OpenRead("http://www.google.com"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    
}

public enum ResponseCodes
{
    successOne = 200,
    successTwo = 201,
    unAuthorizedAccess = 401,
    forbiddenAccess = 403,
    notFound = 404
}
