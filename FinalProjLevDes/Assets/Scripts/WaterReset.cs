using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterReset : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Тэг игрока (должен совпадать с тегом на объекте игрока)")]
    public string playerTag = "Player";

    [Tooltip("Задержка перед перезагрузкой сцены (в секундах)")]
    public float delayBeforeRestart = 0f;

    [Tooltip("Эффект/звук при касании воды (опционально)")]
    public GameObject onTouchEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (onTouchEffect != null)
                Instantiate(onTouchEffect, transform.position, Quaternion.identity);

            // Запускаем перезагрузку (можно с задержкой для эффекта)
            if (delayBeforeRestart > 0f)
                Invoke(nameof(RestartScene), delayBeforeRestart);
            else
                RestartScene();
        }
    }

    private void RestartScene()
    {
        // Получаем имя текущей активной сцены
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}