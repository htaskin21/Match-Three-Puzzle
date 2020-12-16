using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MatchValue
{
    Yellow,
    Blue,
    Magenta,
    Indigo,
    Green,
    Teal,
    Red,
    Cyan,
    Wild,
    None
}

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    Board _board;

    bool _isMoving = false;

    public InterType interpolation = InterType.SmootherStep;
    
    public enum InterType
    {
        Linear,
        EaseOut,
        EaseIn,
        SmoothStep,
        SmootherStep
    }

    public MatchValue matchValue;
    public void SetCoord(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }
    public void Init(Board board)
    {
        _board = board;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move((int)transform.position.x + 1, (int)transform.position.y, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move((int)transform.position.x - 1, (int)transform.position.y, 0.5f);
        }
    }

    public void Move(int destX, int destY, float timeToMove)
    {
        if (!_isMoving)
        {
            StartCoroutine(MoveRoutine(new Vector3(destX, destY, 0), timeToMove));
        }
    }

    IEnumerator MoveRoutine(Vector3 destination, float timeToMove)
    {
        Vector3 startPosition = transform.position;

        bool reachedDestination = false;
        float elapsadTime = 0f;
        _isMoving = true;

        while (!reachedDestination)
        {
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                reachedDestination = true;
                if (_board != null)
                {
                    _board.PlaceGamePiece(this, (int)destination.x, (int)destination.y);
                }
                break;
            }
            elapsadTime += Time.deltaTime;
            float t = Mathf.Clamp(elapsadTime / timeToMove, 0f, 1f);

            switch (interpolation)
            {
                case InterType.Linear:
                    break;
                case InterType.EaseOut:
                    t = Mathf.Sin(t * Mathf.PI * 0.5f);
                    break;
                case InterType.EaseIn:
                    t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
                    break;
                case InterType.SmoothStep:
                    t = t * t * (3 - 2 * t);
                    break;
                case InterType.SmootherStep:
                    t = t * t * t * (t * (t * 6 - 15) + 10);
                    break;
            }

            transform.position = Vector3.Lerp(startPosition, destination, t);
            yield return null;
        }
        _isMoving = false;
    }

    public void ChangeColor(GamePiece pieceToMatch)
    {
        SpriteRenderer rendererToChange = GetComponent<SpriteRenderer>();
        if (pieceToMatch != null)
        {
            SpriteRenderer rendererToMatch = pieceToMatch.GetComponent<SpriteRenderer>();

            if (rendererToMatch != null && rendererToChange != null)
            {
                rendererToChange.color = rendererToMatch.color;
            }

            matchValue = pieceToMatch.matchValue;
        }
    }
}
