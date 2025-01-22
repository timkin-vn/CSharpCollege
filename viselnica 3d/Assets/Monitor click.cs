using UnityEngine;

public class MonitorClick : MonoBehaviour
{
    public GameObject monitorUI; // Ссылка на ваш UI

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверка нажатия левой кнопки мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // Проверка, что нажатие было на этот объект
                {
                    monitorUI.SetActive(true); // Активация UI
                }
            }
        }
    }
}
