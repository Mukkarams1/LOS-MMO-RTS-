using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class userTroopUI : MonoBehaviour
{
    public Transform TroopScrollTransform;
    public GameObject troopEntryPrefab;

    List<GameObject> troopEntries = new List<GameObject>();
    private void OnEnable()
    {
        DataManager.onUserDataUpdated+=UpdateTroopPanel;
    }
    private void OnDisable()
    {
        DataManager.onUserDataUpdated-=UpdateTroopPanel;

    }
    private void Start()
    {
        PopulateUI();
    }
    void UpdateTroopPanel()
    {
        ClearContent();
      PopulateUI();
    }
    void PopulateUI()
    {
        List<Troop> troops = DataManager.instance.GetUserTroopsData();
        foreach (var troop in troops)
        {
            var troopEntry = Instantiate(troopEntryPrefab, TroopScrollTransform);
            var name = DataManager.instance.GetAllTroopsData().Where(i => i._id.Equals(troop.troopId)).FirstOrDefault()?.title;
            troopEntry.GetComponent<userTroopEntry>().setText(name, troop.quantity.ToString(), troop.addedQuantity.ToString());
            troopEntries.Add(troopEntry);
        }
    }
    public void AddNewTroop(string name, string qty, string qty2)
    {
        var troopEntry = Instantiate(troopEntryPrefab, TroopScrollTransform);
        troopEntry.GetComponent<userTroopEntry>().setText(name, qty, qty2);
        troopEntries.Add(troopEntry);
    }
    public void ClearContent()
    {
        foreach (Transform child in TroopScrollTransform.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
