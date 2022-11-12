using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelGUI : MonoBehaviour
{
    public GameObject weaponList;
    public GameObject itemList;

    public GameObject itemBlockPrefab;

    public PlayerController player;
    public EquipmentHandler playerEquipment;

    public List<GameObject> itemBlocks = new List<GameObject>();


    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        playerEquipment = player.GetComponent<EquipmentHandler>();

        GameManager.instance.OnPause += UpdateItems;
    }

    public void UpdateItems() {
        foreach (GameObject go in itemBlocks) {
            Destroy(go);
        }

        for (int i = 0; i < playerEquipment.weapons.Count; i++) {
            GameObject itemBlock = Instantiate(itemBlockPrefab, weaponList.transform);
            ItemBlock block = itemBlock.GetComponent<ItemBlock>();
            block.sprite.sprite = playerEquipment.weapons[i].GetComponent<Weapon>().sprite;
            block.itemName.text = playerEquipment.weapons[i].GetComponent<Weapon>().weaponName;
            block.itemLevel.text = "Lv. " + playerEquipment.weapons[i].GetComponent<Weapon>().level;

            itemBlocks.Add(itemBlock);
        }

        for (int i = 0; i < playerEquipment.items.Count; i++) {
            GameObject itemBlock = Instantiate(itemBlockPrefab, itemList.transform);
            ItemBlock block = itemBlock.GetComponent<ItemBlock>();
            block.sprite.sprite = playerEquipment.items[i].sprite;
            block.itemName.text = playerEquipment.items[i].itemName;
            block.itemLevel.text = "Lv. " + playerEquipment.items[i].level;

            itemBlocks.Add(itemBlock);
        }
    }
}
