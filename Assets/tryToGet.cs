using UnityEngine;


public class tryToGet : MonoBehaviour
{
    // Тег, который мы будем проверять
    public string targetTag = "Enemy";

    // Ссылка на камеру
    public Camera mainCamera;

    private void Start()
    {
        // Если камера не назначена через инспектор, используем основную камеру сцены
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, имеет ли объект заданный тег
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Объект с тегом " + targetTag + " вошел в триггер.");

            // Выполняем проверку на наличие препятствий между камерой и объектом
            if (IsClearLineOfSight(mainCamera.transform.position, other.transform.position))
            {
                Debug.Log("Препятствий нет. Луч проходит до объекта.");
            }
            else
            {
                Debug.Log("Препятствие обнаружено. Луч не проходит до объекта.");
            }
        }
    }

    private bool IsClearLineOfSight(Vector3 origin, Vector3 target)
    {
        // Создаем луч от камеры до цели
        Ray ray = new Ray(origin, target - origin);
        RaycastHit hit;

        // Проверяем, что луч не сталкивается с другими объектами перед целью
        if (Physics.Raycast(ray, out hit))
        {
            // Если луч попал в цель, то путь свободен
            if (hit.collider.CompareTag(targetTag))
            {
                return true;
            }
        }

        // Если луч попал в другой объект или не достиг цели
        return false;
    }

    private void OnDrawGizmos()
    {
        // Визуализация луча в редакторе Unity
        if (mainCamera != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(mainCamera.transform.position, transform.position);
        }
    }
}
