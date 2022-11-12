using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanelGUI : MonoBehaviour
{
    public GameObject statList;

    public GameObject statBlockPrefab;

    public PlayerStatsHandler playerStats;

    public List<StatBlock> statBlocks = new List<StatBlock>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < playerStats.stats.Length; i++) {
            GameObject statBlock = Instantiate(statBlockPrefab, statList.transform);
            StatBlock block = statBlock.GetComponent<StatBlock>();
            block.statNameText.text = playerStats.stats[i].name;
            block.statValueText.text = playerStats.stats[i].GetValue().ToString();

            statBlocks.Add(block);
        }

        GameManager.instance.OnPause += UpdateStats;
    }

    public void UpdateStats() {
        for (int i = 0; i < statBlocks.Count; i++) {
            statBlocks[i].statValueText.text = playerStats.stats[i].GetValue().ToString();
        }
    }
}
