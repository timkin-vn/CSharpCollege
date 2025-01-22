using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSpot : MonoBehaviour
{
    public GameObject targetObject; // Объект, который нужно включать/выключать
    public AudioSource enableSpot; // Ссылка на AudioSource
    public KeyCode toggleKey = KeyCode.F; // Клавиша для переключения

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            // Инвертировать состояние объекта (включить, если выключен, и наоборот)
            targetObject.SetActive(!targetObject.activeSelf);
            enableSpot.Play(); // Начать проигрывание звука
        }
    }
}
