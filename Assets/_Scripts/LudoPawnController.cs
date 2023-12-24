using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LudoPawnController : MonoBehaviour
{
    private bool isOnBoard;
    private int currentPosition;
    private RectTransform rect;
    private Vector2 initPosition;

    public float pathMovementSpeed;
    public RectTransform[] path;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        initPosition = rect.anchoredPosition;
    }

    // Initiates the pawn movement based on the dice roll.
    public void MakeMove()
    {
        if (!LudoGamePlayController.instance.ReadytoMove)
            return;

        // If the pawn is not on the board and the dice roll is 6, move to the starting position.
        if (!isOnBoard && LudoGamePlayController.instance.Steps==6)
        {
            GoToStartPosition();
            isOnBoard = true;
        }
        else if (isOnBoard)
        {
            // Check if the pawn can move by the number of Steps rolled.
            if (currentPosition + LudoGamePlayController.instance.Steps >= path.Length)
                return;

            // Move the pawn by the rolled Steps.
            MoveBySteps(LudoGamePlayController.instance.Steps);
        }

        // Prevent multiple moves in a single turn.
        LudoGamePlayController.instance.ReadytoMove = false;
    }

    // Moves the pawn to the starting position on the board.
    public void GoToStartPosition()
    {
        currentPosition = 0;
        StartCoroutine(MoveDelayed(0, path[currentPosition].anchoredPosition, pathMovementSpeed));       
    }

    // Moves the pawn by the specified number of Steps on the board path.
    public void MoveBySteps(int Steps)
    {
        for (int i = 0; i < Steps; i++)
        {
            currentPosition++;
            StartCoroutine(MoveDelayed(i,path[currentPosition].anchoredPosition, pathMovementSpeed));
        }        
    }

    // Moves the pawn to the specified destination with a delay.
    private IEnumerator MoveDelayed(int delay,Vector2 destination, float time)
    {
        // Introduce a delay based on the step index multiplied by the speed.
        yield return new WaitForSeconds(delay * pathMovementSpeed);

        // Use DOTween for smooth pawn movement.
        rect.DOAnchorPos(destination, time);

        // If the pawn reaches the last position, the player has won.
        if (currentPosition == path.Length - 1)
            LudoGamePlayController.instance.PlayerWon();
    }

    // Checks if the specified move is possible based on the current state of the pawn.
    public bool IsMovePossible(int Steps)
    {
        if (!isOnBoard && Steps != 6) 
            return false;
        else if (currentPosition + LudoGamePlayController.instance.Steps >= path.Length) 
            return false;
       
        return true;
    }
}
