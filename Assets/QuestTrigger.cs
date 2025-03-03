using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public string questName; // Название квеста, который будет выполнен
    public QuestManager questManager; // Ссылка на менеджер квестов

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что объект - это игрок
        {
            if (questManager != null)
            {
                questManager.CompleteQuest(questName); // Завершаем квест
                Debug.Log($"Квест '{questName}' выполнен!");
            }
        }
    }
}