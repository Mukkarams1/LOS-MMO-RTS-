using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class navigation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fromInputPlayerToCity()
    {
        SceneManager.LoadScene("02choosehero");
    }

    public void fromHeroToCity()
    {
        SceneManager.LoadScene("03chooseland");
    }

    public void fromCityToMainGame()
    {
        SceneManager.LoadScene("04singlemode");
    }
}
