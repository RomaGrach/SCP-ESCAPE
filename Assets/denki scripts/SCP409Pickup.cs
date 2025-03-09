using UnityEngine;
using UnityEngine.SceneManagement; // Для перезапуска сцены или показа панели конца игры

public class SCP409Pickup : MonoBehaviour
{
    public InventoryItem scp409Item; // Данные о предмете SCP-409
    public GameObject gameOverPanel; // Панель конца игры (назначить в инспекторе)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventoryManager.instance.HasItem("Перчатки")) // Проверяем наличие перчаток
            {
                bool added = InventoryManager.instance.AddItem(scp409Item);
                if (added)
                {
                    Debug.Log("Игрок подобрал SCP-409 безопасно.");
                    Destroy(gameObject); // Удаляем SCP-409 после подбора
                }
                else
                {
                    Debug.Log("Инвентарь полон, SCP-409 не добавлен.");
                }
            }
            else
            {
                Debug.Log("Игрок коснулся SCP-409 без защиты! Конец игры.");
                gameOverPanel.SetActive(true); // Включаем панель конца игры
                Time.timeScale = 0; // Останавливаем игру
            }
        }
    }
}
