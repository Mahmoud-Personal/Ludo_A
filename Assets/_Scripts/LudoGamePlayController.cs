using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LudoGamePlayController : MonoBehaviour
{
    public static LudoGamePlayController instance;
    public LudoPawnController[] PlayerPawns;
    public Sprite[] DiceFaces;
    public Button DiceButton;
    public GameObject YouWinPanel;
    
    [HideInInspector] public int Steps;
    [HideInInspector] public bool ReadytoMove;
    
    private RandomNumberGenerator randomNumberGenerator;
    private bool isGenerating = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        randomNumberGenerator = new RandomNumberGenerator();
        DiceButton.onClick.AddListener(OnDiceButtonClick);
    }

    // Handles the click event of the dice button
    private void OnDiceButtonClick()
    {
        if (!isGenerating && !ReadytoMove)
        {
            Steps = 0;
            // Call the FetchRandomNumber method when the button is clicked
            randomNumberGenerator.FetchRandomNumber(OnRandomNumberReceived, OnError);

            // Start the coroutine to visually rotate the dice
            StartCoroutine(RotateDiceFacesCoroutine());
        }
    }

    // Handles the received random number from the RandomNumberGenerator
    private void OnRandomNumberReceived(int randomValue)
    {
        // Handle the received random value
        Steps = randomValue;
        //Debug.Log("Random Number Received: " + randomValue);
    }

    // Handles errors during the FetchRandomNumber process
    private void OnError(string errorMessage)
    {
        // If an error occurs, generate a random number between 1 and 6
        Steps = UnityEngine.Random.Range(1,7);
        //Debug.LogError("Error: " + errorMessage);
    }

    // Coroutine to visually rotate the dice
    private IEnumerator RotateDiceFacesCoroutine()
    {
        isGenerating = true;

        // Cycle through images for a short duration
        float CyclingDuration = 2f; // Adjust the duration as needed
        float elapsedTime = 0f;
        int currentIndex = 0;

        while (elapsedTime < CyclingDuration)
        {
            // Cycle through images
            currentIndex = (currentIndex + 1) % DiceFaces.Length;
            DiceButton.image.sprite = DiceFaces[currentIndex];
            yield return new WaitForSeconds(0.15f);
            elapsedTime += 0.15f; // Increment the elapsed time by the delay            
        }

        // Set the final dice face based on the rolled Steps
        DiceButton.image.sprite = DiceFaces[Steps - 1];
        ReadytoMove = true;

        // Check if each pawn is ready to move based on the rolled Steps
        foreach (var pawn in PlayerPawns)
        {
            ReadytoMove = pawn.IsMovePossible(Steps);
        }

        isGenerating = false;
    }

    // Called when a player wins the game
    internal void PlayerWon()
    {
        YouWinPanel.SetActive(true);
    }

    // Resets the game by reloading the gameplay scene
    public void ResetGame()
    {
        SceneManager.LoadSceneAsync("GamePlay", LoadSceneMode.Single);
    }
}
