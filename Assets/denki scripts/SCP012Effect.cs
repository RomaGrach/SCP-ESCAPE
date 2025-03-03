using UnityEngine;
using UnityEngine.UI;

public class SCP012Effect : MonoBehaviour
{
    public float pullForce = 2f; // Сила притяжения
    public AudioSource scpMusic; // Музыка SCP-012
    public GameObject gameOverPanel; // Панель "Конец игры"

    private Transform player;
    private bool isPlayerNear = false;

    void Start()
    {
        gameOverPanel.SetActive(false); // Скрываем панель при старте
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isPlayerNear)
        {
            // Притягивание игрока к SCP-012
            Vector3 direction = (transform.position - player.position).normalized;
            player.position += direction * pullForce * Time.deltaTime;

            // Изменение громкости музыки в зависимости от расстояния
            float distance = Vector3.Distance(player.position, transform.position);
            scpMusic.volume = Mathf.Clamp01(1f - (distance / 10f)); // Чем ближе, тем громче
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            scpMusic.Play(); // Включаем музыку
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            scpMusic.Stop(); // Останавливаем музыку, если игрок вышел
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameOverPanel.SetActive(true); // Показываем панель "Конец игры"
            Time.timeScale = 0f; // Останавливаем время
        }
    }
}

