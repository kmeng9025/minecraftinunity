using Client;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSeed : MonoBehaviour
{
    [SerializeField] Button startGameButton;
    [SerializeField] TMP_InputField seedField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void StartGame()
    {
        if (seedField.text.Length > 8)
        {
            startGameButton.transform.Find("Text").GetComponent<Text>().text = "Seed too long";
        }
        else
        {
            Variables.seed = seedField.text;
            SceneManager.LoadScene(1);
        }
    }
}
