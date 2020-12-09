using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    bool _isMoving = false;

    public void SetCoord(int x, int y)
    {
        xIndex = x;
        yIndex = y;
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

    void Move(int destX, int destY, float timeToMove)
    {
        StartCoroutine(MoveRoutine(new Vector3(destX,destY,0), timeToMove));
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
                transform.position = destination;
                SetCoord((int)destination.x, (int)destination.y);
                break;
            }
            elapsadTime += Time.deltaTime;
            float t = Mathf.Clamp(elapsadTime / timeToMove, 0f, 1f);
            transform.position = Vector3.Lerp(startPosition, destination, t);
            yield return null;
        }
        _isMoving = false;
    }
}
