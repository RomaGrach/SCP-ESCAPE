using System.Collections.Generic;
using UnityEngine;
using TMPro; // Подключаем пространство имен TextMeshPro

public class QuestManager : MonoBehaviour
{
    // Ссылки на текстовые поля UI (используем TextMeshPro)
    public TMP_Text currentQuestsText; // Текущие активные задачи
    public TMP_Text completedQuestsText; // Выполненные задачи

    // Массивы для хранения квестов
    public List<Quest> activeQuests = new List<Quest>(); // Квесты, которые игрок видит
    public List<Quest> allQuests = new List<Quest>(); // Все доступные квесты

    private int completedQuestCount = 0; // Счетчик выполненных квестов

    void Start()
    {
        UpdateUI(); // Инициализация UI при старте
    }

    void Update()
    {
        // Проверка статуса всех активных квестов
        foreach (var quest in activeQuests)
        {
            if (quest.isCompleted && !quest.isMarkedAsCompleted)
            {
                quest.isMarkedAsCompleted = true;
                completedQuestCount++;
                UpdateUI();
            }
        }
    }

    // Метод для добавления нового квеста
    public void AddQuest(Quest quest)
    {
        allQuests.Add(quest);
        activeQuests.Add(quest);
        UpdateUI();
    }

    // Метод для завершения квеста
    public void CompleteQuest(string questName)
    {
        foreach (var quest in activeQuests)
        {
            if (quest.name == questName)
            {
                quest.isCompleted = true;
                break;
            }
        }
        UpdateUI();
    }

    // Обновление текстовых полей UI
    private void UpdateUI()
    {
        string activeQuestsString = "Текущие задачи:\n";
        foreach (var quest in activeQuests)
        {
            activeQuestsString += $"{quest.name}: {(quest.isCompleted ? "Выполнено" : "В процессе")}\n";
        }

        currentQuestsText.text = activeQuestsString;
        completedQuestsText.text = $"Выполнено заданий: {completedQuestCount}/{allQuests.Count}";
    }
}

// Класс, представляющий квест
[System.Serializable]
public class Quest
{
    public string name; // Название квеста
    public string description; // Описание квеста
    public bool isCompleted; // Статус выполнения
    public bool isMarkedAsCompleted; // Флаг для избежания повторного обновления
}