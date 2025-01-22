using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    public Button lowQualityButton;
    public Button mediumQualityButton;
    public Button highQualityButton;

    void Start()
    {
        // Подписываемся на события нажатия кнопок
        lowQualityButton.onClick.AddListener(delegate { SetQuality(0); });
        mediumQualityButton.onClick.AddListener(delegate { SetQuality(1); });
        highQualityButton.onClick.AddListener(delegate { SetQuality(2); });

        // Устанавливаем начальное качество графики в зависимости от сохраненных настроек
        int savedQuality = PlayerPrefs.GetInt("QualitySetting", 1); // По умолчанию Medium
        SetQualityLevel(savedQuality);
    }

    void SetQuality(int qualityIndex)
    {
        SetQualityLevel(qualityIndex);
    }

    void SetQualityLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
        PlayerPrefs.SetInt("QualitySetting", qualityIndex);
        PlayerPrefs.Save();
    }
}
