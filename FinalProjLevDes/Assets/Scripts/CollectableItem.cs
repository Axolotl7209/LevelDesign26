using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectableItem : MonoBehaviour
{
    [Tooltip("Диалог (UI-панель), который активируется при подборе этого предмета")]
    public GameObject dialogOnCollect;

    [HideInInspector]
    public bool isCollected = false;

    private void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected && gameObject.activeSelf)
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (isCollected) return;

        // Сообщаем менеджеру – он сам установит isCollected = true
        if (CollectionManager.Instance != null)
            CollectionManager.Instance.CollectItem(this);
        else
            Debug.LogError("CollectionManager.Instance не найден!");

        // Отключаем объект (флаг isCollected уже true)
        gameObject.SetActive(false);
    }
}