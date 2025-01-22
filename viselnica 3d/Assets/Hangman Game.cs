using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class HangmanGame : MonoBehaviour
{
    public GameObject deadTop;
    public float fallSpeed = 1.0f; // Скорость падения объекта
    private bool isFalling = false; // Флаг, чтобы отслеживать начало падения
    public GameObject one;
    public GameObject two;
    public GameObject the;
    public GameObject tfo;
    public GameObject sez;
    public GameObject sem;
    public GameObject vos;
    public AudioSource wrongWord; // Ссылка на AudioSource
    public AudioSource yesWord; // Ссылка на AudioSource
    public TMP_Text wordDisplay;
    public TMP_Text lettersUsedDisplay;
    public TMP_InputField inputField; // Добавляем поле ввода
    public TMP_Text victoryMessageDisplay; // Добавьте эту строку в начало класса
     public TMP_Text hangmanDisplay; // Добавьте эту строку в начало класса
    public TMP_Text defeatMessageDisplay; // Для отображения сообщения о проигрыше
    public TMP_Text usedwords; // Для отображения сообщения о проигрыше
    private int mistakesAllowed = 7; // Количество допустимых ошибок
    private int currentMistakes = 0; // Текущее количество ошибок
    private string[] hangmanStates = new string[7]; // Состояния человечка
    public string[] wordList = { "рыба", "птица", "коралл", "волна", "акула", "дельфин", "планктон", "водоросли", "морской", "прилив", "отлив", "подводный", "глубина", "соленый", "песок", "пляж", "остров", "маяк", "краб", "медуза", "ракушка", "мореплаватель", "океанография", "субмарина", "титаник", "берег", "морской_звезда", "чайка", "пират", "сундук", "сокровище", "гарпун", "тропики", "экосистема", "биоразнообразие", "цунами", "мангровый", "лагуна", "атолл", "барьерный_риф", "морской_конек", "навигация", "морской_бриз" };
    private string chosenWord;
    private string hiddenWord;
    public GameObject deathScreen;
    public AudioSource audioSource; // Ссылка на компонент AudioSource
    
    private List<char> lettersUsed = new List<char>();

    public TMP_Dropdown themeDropdown; // Dropdown для выбора темы

    private Dictionary<string, string[]> themes = new Dictionary<string, string[]>
    {
        { "Ничего", new string[] { "None" } },
        { "Море", new string[] { "рыба", "птица", "коралл", "волна", "акула", "дельфин", "планктон", "водоросли", "морской", "прилив", "отлив", "подводный", "глубина", "соленый", "песок", "пляж", "остров", "маяк", "краб", "медуза", "ракушка", "мореплаватель", "океанография", "субмарина", "титаник", "берег", "морская_звезда", "чайка", "пират", "сундук", "сокровище", "гарпун", "тропики", "экосистема", "биоразнообразие", "цунами", "мангровый", "лагуна", "атолл", "барьерный_риф", "морской_конек", "навигация", "морской_бриз" } },
        { "Животные", new string[] { "тигр", "лев", "слон", "жираф", "зебра", "обезьяна", "кенгуру", "панда", "коала", "носорог", "бегемот", "волк", "лиса", "медведь", "олен", "заяц", "бобр", "барсук", "еж", "выдра", "дикобраз", "крыса", "мышь", "белка", "енот", "лось", "бизон", "антилопа", "кабан", "як", "буйвол", "тапир", "мангуст", "какаду", "страус", "фламинго", "пингвин", "чайка", "орел", "сокол", "ястреб" } },
        { "Техника", new string[] { "компьютер", "телефон", "телевизор", "радио", "автомобиль", "самолет", "вертолет", "поезд", "корабль", "подлодка", "ракета", "спутник", "робот", "дрон", "камера", "фотоаппарат", "проектор", "принтер", "сканер", "монитор", "мышь", "клавиатура", "джойстик", "консоль", "планшет", "ноутбук", "сервер", "маршрутизатор", "коммутатор", "микрофон", "наушники", "колонка", "усилитель", "синтезатор", "светильник", "пылесос", "стиральная_машина", "холодильник", "микроволновка", "плита", "духовка", "утюг" } }
    };


    void Start()
    {
        ChooseWord();
        DisplayWord();
        themeDropdown.onValueChanged.AddListener(delegate { ChooseTheme(); });

        one.gameObject.SetActive(false);
        two.gameObject.SetActive(false);
        the.gameObject.SetActive(false);
        tfo.gameObject.SetActive(false);
        sez.gameObject.SetActive(false);
        sem.gameObject.SetActive(false);
        vos.gameObject.SetActive(false);
    }

    void ChooseTheme()
    {
        string selectedTheme = themeDropdown.options[themeDropdown.value].text;
        wordList = themes[selectedTheme];
        themeDropdown.gameObject.SetActive(false); // Скрыть dropdown после выбора темы
        StartGame();
    }

    void UpdateHangmanState()
    {
        switch(currentMistakes)
        {
            case 1:
                one.gameObject.SetActive(true);
                break;
            case 2:
                two.gameObject.SetActive(true);
                break;
            case 3:
                the.gameObject.SetActive(true);
                break;
            case 4:
                tfo.gameObject.SetActive(true);
                break;
            case 5:
                sez.gameObject.SetActive(true);
                break;
            case 6:
                sem.gameObject.SetActive(true);
                break;
            case 7:
                vos.gameObject.SetActive(true);
                break;
        }
    }

    void StartGame()
    {
        ChooseWord();
        DisplayWord();
        inputField.onEndEdit.AddListener(delegate { GuessLetter(); });
    }


    void ChooseWord()
    {
        chosenWord = wordList[Random.Range(0, wordList.Length)];
        hiddenWord = new string('_', chosenWord.Length);
    }


    void DisplayWord()
    {
        wordDisplay.text = "";
        foreach (char letter in hiddenWord)
        {
            wordDisplay.text += letter + " ";
        }
    }

    public void GuessLetter()
    {
        string playerInput = inputField.text.ToUpper();
        if (playerInput.Length == 1)
        {
            char guessedLetter = playerInput[0];
            inputField.text = "";
    
            if (lettersUsed.Contains(guessedLetter))
            {
                return;
            }
    
            lettersUsed.Add(guessedLetter);
            lettersUsedDisplay.text = "Использованные буквы: " + string.Join(" ", lettersUsed);
            yesWord.Play();
    
            if (chosenWord.ToUpper().Contains(guessedLetter.ToString()))
            {
                // Обновляем hiddenWord
                char[] hiddenWordArray = hiddenWord.ToCharArray();
                for (int i = 0; i < chosenWord.Length; i++)
                {
                    if (chosenWord.ToUpper()[i] == guessedLetter)
                    {
                        hiddenWordArray[i] = guessedLetter;
                    }
                }
                hiddenWord = new string(hiddenWordArray);
                DisplayWord();
            }
            else
            {

            }

            if (!chosenWord.ToUpper().Contains(guessedLetter.ToString()))
            {
                wrongWord.Play(); // Начать проигрывание звука
                currentMistakes++;
                if (currentMistakes <= mistakesAllowed)
                {
                    UpdateHangmanState();
                }
            }
    
            CheckWinCondition();
        }
    }



    IEnumerator DefeatSequence()
    {
        defeatMessageDisplay.text = $"Проиграл. Загаданное слово: {chosenWord}";
        defeatMessageDisplay.gameObject.SetActive(true);
        usedwords.gameObject.SetActive(false);
        inputField.gameObject.SetActive(false);
        wordDisplay.gameObject.SetActive(false);

        // Ожидание 3 секунд
        yield return new WaitForSeconds(3);

        deathScreen.gameObject.SetActive(true);
        audioSource.Play(); // Проигрывание звука
        Cursor.lockState = CursorLockMode.Confined;
        StartCoroutine(RestartSceneAfterSound());
    }

    void CheckWinCondition()
    {
        if (hiddenWord.ToUpper() == chosenWord.ToUpper())
        {
            victoryMessageDisplay.text = "Победа!";
            victoryMessageDisplay.gameObject.SetActive(true);
        }
        else if (currentMistakes >= mistakesAllowed)
        {
            StartCoroutine(DefeatSequence());
        }
    }

    private IEnumerator RestartSceneAfterSound()
    {
        yield return new WaitWhile(() => audioSource.isPlaying); // Ожидание окончания звука
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагрузка сцены
    }

}
