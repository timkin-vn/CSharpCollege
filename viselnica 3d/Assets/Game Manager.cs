using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainObject; // Переменная для основного объекта
    public GameObject backgroundObject; // Переменная для фонового объекта
    private bool gameStarted = false; // Флаг, чтобы отслеживать, была ли нажата кнопка "играть"

    void Start()
    {
        // Замораживаем время и блокируем объекты при старте игры
        if (gameStarted) return;
        Time.timeScale = 0f;
        mainObject.SetActive(false); // Блокируем основной объект
        backgroundObject.SetActive(true); // Активируем фоновый объект
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        // Проверяем, не была ли уже нажата кнопка "играть"
        if (gameStarted) return;

        // Устанавливаем флаг в true, чтобы предотвратить повторное нажатие
        gameStarted = true;

        // Размораживаем время и разблокируем объекты после нажатия на кнопку "играть"
        Time.timeScale = 1f;
        mainObject.SetActive(true); // Разблокируем основной объект
        backgroundObject.SetActive(false); // Блокируем фоновый объект
    }
}
