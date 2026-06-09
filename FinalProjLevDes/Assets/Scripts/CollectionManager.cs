using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager Instance { get; private set; }

    [Header("UI Шкала")]
    public Slider progressSlider;

    [Header("Список предметов (можно оставить пустым)")]
    public List<CollectableItem> collectableItems;

    private int totalCount = 0;
    private int collectedCount = 0;

    public event Action OnAllCollected;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Если список не назначен или пуст, находим все объекты с CollectableItem на сцене
        if (collectableItems == null || collectableItems.Count == 0)
        {
            CollectableItem[] found = FindObjectsOfType<CollectableItem>(true);
            collectableItems = new List<CollectableItem>(found);
        }
        else
        {
            // Очищаем список от null
            collectableItems.RemoveAll(item => item == null);
        }

        // Сбрасываем флаги сбора у всех предметов (на случай перезапуска сцены)
        foreach (var item in collectableItems)
        {
            if (item != null)
                item.isCollected = false;
        }

        totalCount = collectableItems.Count;
        collectedCount = 0;
        UpdateProgressUI();

        Debug.Log($"CollectionManager: найдено предметов = {totalCount}");
    }

    public void CollectItem(CollectableItem item)
    {
        if (item == null || item.isCollected) return;

        item.isCollected = true;
        collectedCount++;
        UpdateProgressUI();

        Debug.Log($"Собран предмет: {item.name}. Прогресс: {collectedCount}/{totalCount}");

        // Активируем диалог, связанный с предметом
        if (item.dialogOnCollect != null)
        {
            item.dialogOnCollect.SetActive(true);
        }

        // Проверяем, все ли собраны
        if (collectedCount >= totalCount && totalCount > 0)
        {
            Debug.Log("Все предметы собраны! Вызываем событие OnAllCollected");
            OnAllCollected?.Invoke();
        }
    }

    private void UpdateProgressUI()
    {
        if (progressSlider != null && totalCount > 0)
        {
            float value = (float)collectedCount / totalCount;
            progressSlider.value = value;
            Debug.Log($"Слайдер обновлён: {value}");
        }
        else if (progressSlider == null)
        {
            Debug.LogWarning("Progress Slider не назначен в CollectionManager!");
        }
    }

    public bool IsFullCollection()
    {
        return collectedCount >= totalCount && totalCount > 0;
    }
}