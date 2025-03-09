using UnityEngine;
using UnityEngine.UI;

public class SCP012Effect : MonoBehaviour
{
    public float pullForce = 2f; // Сила притяжения
    public float pullRadius = 100f; // Радиус притяжения
    public AudioSource scpMusic; // Музыка SCP-012
    public GameObject gameOverPanel; // Панель "Конец игры"

    private Transform player;

    void Start()
    {
        gameOverPanel.SetActive(false); // Скрываем панель при старте
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // Если игрок в радиусе 100, включаем музыку и притягиваем
        if (distance < pullRadius)
        {
            if (!scpMusic.isPlaying) scpMusic.Play(); // Включаем музыку (если не играет)

            // Притягивание игрока к SCP-012
            Vector3 direction = (transform.position - player.position).normalized;
            player.position += direction * pullForce * Time.deltaTime;

            // Громкость музыки в зависимости от расстояния
            scpMusic.volume = Mathf.Clamp01(1f - (distance / pullRadius));
        }
        else
        {
            scpMusic.Stop(); // Если вышел за радиус - выключаем музыку
        }
    }

    // 💀 Если игрок касается SCP-012, игра заканчивается
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true); // Показываем панель "Конец игры"
        Time.timeScale = 0f; // Останавливаем время
    }
}
