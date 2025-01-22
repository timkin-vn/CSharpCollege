using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class RestartSceneOnTrigger : MonoBehaviour
{
    public string playerTag = "Player"; // Тег объекта игрока
    public GameObject deathScreen;
    public AudioSource audioSource; // Ссылка на компонент AudioSource

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            deathScreen.gameObject.SetActive(true);
            audioSource.Play(); // Проигрывание звука
            Cursor.lockState = CursorLockMode.Confined;
            StartCoroutine(RestartSceneAfterSound());
        }
    }

    private IEnumerator RestartSceneAfterSound()
    {
        yield return new WaitWhile(() => audioSource.isPlaying); // Ожидание окончания звука
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагрузка сцены
    }
}
