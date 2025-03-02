using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
<<<<<<< Updated upstream
    public GameObject inventoryPanel; // Панель инвентаря
    public Image[] itemSlots; // Ячейки для предметов
    private bool isInventoryOpen = false;
    private int selectedItemIndex = -1;
    void Start()
    {
        inventoryPanel.SetActive(false); // Инвентарь скрыт при старте
    }
=======
    public static InventoryManager instance; // Singleton

    public GameObject inventoryPanel; // РџР°РЅРµР»СЊ РёРЅРІРµРЅС‚Р°СЂСЏ
    public Image[] itemSlots; // РЇС‡РµР№РєРё-РёРЅРІРµРЅС‚Р°СЂСЏ
    private InventoryItem[] inventory = new InventoryItem[10]; // 10 РїСЂРµРґРјРµС‚РѕРІ
    private bool isInventoryOpen = false;
    private int selectedItemIndex = -1;

    public Transform dropPoint; // РўРѕС‡РєР° РІС‹Р±СЂРѕСЃР° РїСЂРµРґРјРµС‚Р° (РЅР°Р·РЅР°С‡СЊ РІ Unity)
    public GameObject itemPrefab; // РџСЂРµС„Р°Р± РІС‹Р±СЂР°СЃС‹РІР°РµРјРѕРіРѕ РїСЂРµРґРјРµС‚Р°

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

>>>>>>> Stashed changes
    void Update()
    {
        // Открытие/закрытие инвентаря на клавишу B
        if (Input.GetKeyDown(KeyCode.B))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.SetActive(isInventoryOpen);
        }

        // Переключение предметов клавишами 1-5
        for (int i = 0; i < itemSlots.Length; i++)
        {
<<<<<<< Updated upstream
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
=======
            if ((i < 9 && Input.GetKeyDown(KeyCode.Alpha1 + i)) || (i == 9 && Input.GetKeyDown(KeyCode.Alpha0)))
>>>>>>> Stashed changes
            {
                if (inventory[i] != null)
                {
                    SelectItem(i);
                }
            }
        }
<<<<<<< Updated upstream
    }
=======

        // Р’С‹Р±СЂРѕСЃРёС‚СЊ РїСЂРµРґРјРµС‚ РїСЂРё РЅР°Р¶Р°С‚РёРё Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
    }

    public bool AddItem(InventoryItem newItem)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = newItem;
                itemSlots[i].sprite = newItem.icon;
                itemSlots[i].enabled = true;
                return true;
            }
        }
        Debug.Log("РРЅРІРµРЅС‚Р°СЂСЊ РїРѕР»РѕРЅ!");
        return false;
    }

>>>>>>> Stashed changes
    void SelectItem(int index)
    {
        selectedItemIndex = index;
        Debug.Log("Выбран предмет в слоте " + (index + 1));

        // Можно добавить визуальное выделение активного предмета
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].color = (i == index) ? Color.yellow : Color.white;
        }
    }
<<<<<<< Updated upstream
}

=======

    public void UseItem()
    {
        if (selectedItemIndex != -1 && inventory[selectedItemIndex] != null)
        {
            Debug.Log("РСЃРїРѕР»СЊР·РѕРІР°РЅ РїСЂРµРґРјРµС‚: " + inventory[selectedItemIndex].itemName);
            inventory[selectedItemIndex] = null;
            itemSlots[selectedItemIndex].sprite = null;
            itemSlots[selectedItemIndex].enabled = false;
            selectedItemIndex = -1;
        }
    }

    public void DropItem()
    {
        if (selectedItemIndex == -1 || inventory[selectedItemIndex] == null)
        {
            Debug.Log("РќРµС‚ РІС‹Р±СЂР°РЅРЅРѕРіРѕ РїСЂРµРґРјРµС‚Р° РґР»СЏ РІС‹Р±СЂРѕСЃР°!");
            return;
        }

        Debug.Log("Р’С‹Р±СЂРѕС€РµРЅ РїСЂРµРґРјРµС‚: " + inventory[selectedItemIndex].itemName);

        // РћРїСЂРµРґРµР»СЏРµРј РїРѕР·РёС†РёСЋ РІС‹Р±СЂРѕСЃР° РїРµСЂРµРґ РёРіСЂРѕРєРѕРј
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 dropPosition = playerTransform.position + playerTransform.forward * 1.5f; // 1.5 РјРµС‚СЂР° РІРїРµСЂРµРґРё

        // РЎРѕР·РґР°С‘Рј РІС‹Р±СЂРѕС€РµРЅРЅС‹Р№ РїСЂРµРґРјРµС‚ РїРµСЂРµРґ РёРіСЂРѕРєРѕРј
        GameObject droppedItem = Instantiate(itemPrefab, dropPosition, Quaternion.identity);
        droppedItem.GetComponent<ItemPickup>().itemData = inventory[selectedItemIndex];

        // РћС‡РёС‰Р°РµРј СЃР»РѕС‚ РІ РёРЅРІРµРЅС‚Р°СЂРµ
        inventory[selectedItemIndex] = null;
        itemSlots[selectedItemIndex].sprite = null;
        itemSlots[selectedItemIndex].enabled = false;

        selectedItemIndex = -1;
    }

}
>>>>>>> Stashed changes
