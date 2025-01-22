using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    public float loadingTime = 3f; // Время загрузки в секундах
    public AudioSource audioSource; // Ссылка на AudioSource

    public Image loadingImage; // Ссылка на Image (экран загрузки)
    public Material loadingImage2; // Ссылка на Image (экран загрузки)
    public float timer = 0f;
    public bool isLoading = true;

    private void Start()
    {
        loadingImage = GetComponent<Image>();
        audioSource.Play(); // Начать проигрывание звука
    }

    private void Update()
    {
        if (isLoading)
        {
            timer += Time.deltaTime;
            float albedoValue = Mathf.Lerp(1f, 0f, timer / loadingTime); // Плавное уменьшение значения Albedo
            loadingImage2.color = new Color(albedoValue, albedoValue, albedoValue);
            float alpha = Mathf.Lerp(1f, 0f, timer / loadingTime); // Плавное уменьшение прозрачности
            loadingImage.color = new Color(0.2f, 0.2f, 0.2f, alpha);
            loadingImage2.color = new Color(1f, 1f, 1f, alpha);

            if (timer >= loadingTime)
            {
                isLoading = false;
                loadingImage.gameObject.SetActive(false);
            }
        }
    }
}
