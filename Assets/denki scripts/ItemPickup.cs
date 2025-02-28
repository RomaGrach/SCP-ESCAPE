using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem itemData; // Ссылка на данные предмета

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Объект {other.name} вошёл в триггер {gameObject.name}");

        if (other.CompareTag("Player")) // Проверяем, это ли игрок
        {
            Debug.Log("Игрок коснулся предмета!");

            bool added = InventoryManager.instance.AddItem(itemData);
            if (added)
            {
                Debug.Log("Предмет добавлен в инвентарь!");
                Destroy(gameObject); // Удаляем предмет после подбора
            }
            else
            {
                Debug.Log("Инвентарь полон, предмет не добавлен.");
            }
        }
    }
}
