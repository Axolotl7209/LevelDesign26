using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneByIndex : MonoBehaviour
{
    [Header("Настройки сцены")]
    [Tooltip("Индекс сцены в Build Settings (0, 1, 2...)")]
    public int sceneIndex = 0;

    [Tooltip("Задержка перед загрузкой (секунды)")]
    public float delay = 0f;

    private void Start()
    {
        // Если скрипт висит на кнопке, автоматически добавляем обработчик
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadScene);
        }
    }

    // Метод для загрузки (можно вызывать через OnClick)
    public void LoadScene()
    {
        if (delay > 0f)
            Invoke(nameof(DoLoad), delay);
        else
            DoLoad();
    }

    private void DoLoad()
    {
        // Проверка: существует ли сцена с таким индексом
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError($"Сцена с индексом {sceneIndex} не добавлена в Build Settings! Проверьте File -> Build Settings.");
        }
    }

    // Опционально: для отображения в инспекторе предупреждения
    private void OnValidate()
    {
        if (sceneIndex < 0)
            sceneIndex = 0;
    }
}