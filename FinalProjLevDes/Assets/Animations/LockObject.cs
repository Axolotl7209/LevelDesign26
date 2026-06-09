using UnityEngine;

public class LockObject : MonoBehaviour
{
    public GameObject finalDialog;
    public GameObject incompleteDialog;

    private bool finalShown = false;
    private bool allCollected = false;

    private void Start()
    {
        if (finalDialog != null) finalDialog.SetActive(false);
        if (incompleteDialog != null) incompleteDialog.SetActive(false);

        if (CollectionManager.Instance != null)
        {
            CollectionManager.Instance.OnAllCollected += OnAllItemsCollected;
            if (CollectionManager.Instance.IsFullCollection())
                OnAllItemsCollected();
        }
    }

    private void OnDestroy()
    {
        if (CollectionManager.Instance != null)
            CollectionManager.Instance.OnAllCollected -= OnAllItemsCollected;
    }

    private void OnAllItemsCollected()
    {
        allCollected = true;
        Debug.Log("LockObject: получен сигнал о полном сборе");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (CollectionManager.Instance != null)
        {
            bool full = CollectionManager.Instance.IsFullCollection() || allCollected;
            if (full)
            {
                if (!finalShown && finalDialog != null)
                {
                    finalDialog.SetActive(true);
                    finalShown = true;
                    Debug.Log("Показан финальный диалог");
                }
            }
            else
            {
                if (incompleteDialog != null)
                {
                    incompleteDialog.SetActive(true);
                    Debug.Log("Показан диалог о неполном сборе");
                }
            }
        }
    }
}