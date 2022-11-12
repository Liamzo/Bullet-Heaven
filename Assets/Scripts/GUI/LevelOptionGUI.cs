using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelOptionGUI : MonoBehaviour
{
    public LevelUpPanel panel;

    public TextMeshProUGUI levelName;
    public TextMeshProUGUI description;
    public Image image;

    public LevelOption levelOption;

    public void OptionSelected() {
        levelOption.Select();

        panel.OptionSelected();
    }
}
