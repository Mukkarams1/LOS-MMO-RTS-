using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Player
{
    
    internal void Initialize()
    {
        GameManager.instance.InitializeGrid(DataManager.instance.SignInResponseData?.data?.buildings);
    }
}
