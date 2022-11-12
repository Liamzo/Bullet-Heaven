using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    public GameObject levelUpParent;
    public GameObject optionList;

    public GameObject levelOptionPrefab;

    public PlayerStatsHandler playerStats;
    public ExpHandler expHandler;

    public List<LevelOptionGUI> levelOptions = new List<LevelOptionGUI>();

    public event System.Action LevelCompleteAction;

    // Start is called before the first frame update
    void Start()
    {
        playerStats.GetComponent<ExpHandler>().OnLevelUp += OnLevelUp;
    }


    public void OnLevelUp() {
        levelUpParent.SetActive(true);
        GameManager.instance.PauseGame(true);
        
        int count = 0;
        while (count < 4 && expHandler.levelOptions.Count > 0) {
            LevelOption option = expHandler.levelOptions[Random.Range(0, expHandler.levelOptions.Count)];
            expHandler.levelOptions.Remove(option);

            GameObject obj = Instantiate(levelOptionPrefab, optionList.transform);
            LevelOptionGUI optionGUI = obj.GetComponent<LevelOptionGUI>();
            levelOptions.Add(optionGUI);

            optionGUI.panel = this;
            optionGUI.levelOption = option;
            optionGUI.image.sprite = option.icon;
            optionGUI.levelName.text = option.levelName;
            optionGUI.description.text = option.description;

            count++;
        }

        if (count == 0) {
            OptionSelected();
        }
    }

    public void OptionSelected() {
        foreach(LevelOptionGUI tempOption in levelOptions) {
            Destroy(tempOption.gameObject);
        }
        
        levelOptions.Clear();

        levelUpParent.SetActive(false);

        GameManager.instance.PauseGame(false);

        playerStats.FullHeal();

        if (LevelCompleteAction != null) {
            LevelCompleteAction();
        }
    }
}
