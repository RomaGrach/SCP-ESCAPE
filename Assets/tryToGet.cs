using System.Collections.Generic;
using UnityEngine;

public class tryToGet : MonoBehaviour
{
    // Тег, который мы будем проверять
    public string targetTag = "Enemy";
    public bool ableGet = false;
    public GameObject thing;
    public float MaxDist = 5f;
    [SerializeField] private List<GameObject> HaveItem = new List<GameObject>();
    public Transform BackPack;
    public Transform LeftHand;
    public Transform RightHand;

    // Ссылка на камеру
    public Camera mainCamera;

    // Индекс текущего объекта для левой и правой руки
    private int leftHandIndex = -1; // -1 означает, что рука пуста
    private int rightHandIndex = -1; // -1 означает, что рука пуста

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
                ableGet = true;
                thing = other.gameObject;
            }
            else
            {
                Debug.Log("Препятствие обнаружено. Луч не проходит до объекта.");
                ableGet = false;
                thing = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == thing)
        {
            ableGet = false;
            thing = null;
        }
    }

    private void Update()
    {
        // Добавление объекта в список при нажатии E
        if (Input.GetKeyDown(KeyCode.E) && ableGet)
        {
            HaveItem.Add(thing);
            ableGet = false;
            thing.transform.position = BackPack.transform.position;
            thing.SetActive(false);
            thing = null;
        }

        // Управление левой рукой (клавиша R)
        if (Input.GetKeyDown(KeyCode.R))
        {
            HandleHand(LeftHand, ref leftHandIndex, rightHandIndex);
        }

        // Управление правой рукой (клавиша T)
        if (Input.GetKeyDown(KeyCode.T))
        {
            HandleHand(RightHand, ref rightHandIndex, leftHandIndex);
        }

        // Выброс предмета из левой руки (клавиша G)
        if (Input.GetKeyDown(KeyCode.G))
        {
            DropItemFromHand(LeftHand, ref leftHandIndex);
        }
    }

    private void HandleHand(Transform hand, ref int handIndex, int otherHandIndex)
    {
        // Если в списке нет объектов, ничего не делаем
        if (HaveItem.Count == 0)
        {
            Debug.Log("Список объектов пуст.");
            return;
        }

        // Освобождаем текущий объект в руке, если он есть
        if (handIndex >= 0 && handIndex < HaveItem.Count)
        {
            GameObject currentItem = HaveItem[handIndex];
            currentItem.transform.position = BackPack.transform.position;
            currentItem.SetActive(false);

            // Включаем Collider и Rigidbody, если они были выключены
            EnablePhysicsComponents(currentItem);
        }

        // Переходим к следующему объекту в списке (циклически)
        do
        {
            handIndex++;
            if (handIndex >= HaveItem.Count)
            {
                handIndex = -1; // Рука становится пустой
            }
        } while (handIndex >= 0 && handIndex == otherHandIndex); // Пропускаем объект, который уже в другой руке

        // Если рука не пуста, берем следующий объект
        if (handIndex >= 0)
        {
            GameObject nextItem = HaveItem[handIndex];
            nextItem.transform.position = hand.position;
            nextItem.transform.rotation = hand.rotation; // Поворот объекта совпадает с поворотом руки
            nextItem.SetActive(true);

            // Проверяем, чтобы объект не был в другой руке
            if (nextItem.transform.parent != hand)
            {
                nextItem.transform.SetParent(hand);
            }

            // Выключаем Collider и Rigidbody у объекта
            DisablePhysicsComponents(nextItem);
        }
    }

    private void DropItemFromHand(Transform hand, ref int handIndex)
    {
        // Если рука пуста, ничего не делаем
        if (handIndex < 0 || handIndex >= HaveItem.Count)
        {
            Debug.Log("Рука пуста. Нечего выбрасывать.");
            return;
        }

        // Получаем текущий объект в руке
        GameObject currentItem = HaveItem[handIndex];

        // Возвращаем объект в мир
        currentItem.transform.position = hand.position;
        currentItem.transform.rotation = hand.rotation; // Поворот объекта совпадает с поворотом руки
        currentItem.transform.SetParent(null); // Убираем родителя
        currentItem.SetActive(true);

        // Включаем Collider и Rigidbody
        EnablePhysicsComponents(currentItem);

        // Удаляем объект из списка
        HaveItem.RemoveAt(handIndex);
        handIndex = -1; // Рука становится пустой

        Debug.Log($"Объект {currentItem.name} выброшен из руки.");
    }

    private void DisablePhysicsComponents(GameObject obj)
    {
        // Проверяем и выключаем Collider
        Collider collider = obj.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
            Debug.Log($"Collider у объекта {obj.name} выключен.");
        }

        // Проверяем и выключаем Rigidbody
        Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true; // Делаем объект кинематическим
            Debug.Log($"Rigidbody у объекта {obj.name} выключен.");
        }
    }

    private void EnablePhysicsComponents(GameObject obj)
    {
        // Проверяем и включаем Collider
        Collider collider = obj.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
            Debug.Log($"Collider у объекта {obj.name} включен.");
        }

        // Проверяем и включаем Rigidbody
        Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false; // Делаем объект динамическим
            Debug.Log($"Rigidbody у объекта {obj.name} включен.");
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