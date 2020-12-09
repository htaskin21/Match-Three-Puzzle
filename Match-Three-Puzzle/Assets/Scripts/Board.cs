using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject[] gamePiecePrefabs;
    public int height;
    public int width;

    public int borderSize;

    Tile[,] _allTiles;
    GamePiece[,] _allGamePieces;

    void Start()
    {
        _allTiles = new Tile[width, height];
        _allGamePieces = new GamePiece[width, height];
        SetupTiles();
        SetupCamera();
        FillRandom();
    }

    void SetupTiles()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                tile.name = $"Tile({i},{j})";
                _allTiles[i, j] = tile.GetComponent<Tile>();
                _allTiles[i, j].Init(i, j, this);
                tile.transform.parent = transform;
            }
        }
    }

    void SetupCamera()
    {
        Camera.main.transform.position = new Vector3((((float)width - 1f) / 2f), (((float)height - 1f) / 2f), -10f);
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float vertical = (float)height / 2 + borderSize;
        float horizontal = ((float)width / 2 + borderSize) / aspectRatio;
        Camera.main.orthographicSize = (horizontal > vertical) ? horizontal : vertical;
    }

    GameObject GetRandomGamePiece()
    {
        int randomIndex = Random.Range(0, gamePiecePrefabs.Length);
        if (gamePiecePrefabs[randomIndex] == null)
        {
            Debug.LogWarning("Board: " + randomIndex + "does not contain valid prefab");
        }

        return gamePiecePrefabs[randomIndex];
    }

    void PlaceGamePiece(GamePiece gamePiece, int x, int y)
    {
        if (gamePiece == null)
        {
            Debug.LogWarning("Board: Invalid GamePiece");
            return;
        }
        gamePiece.transform.position = new Vector3(x, y, 0);
        gamePiece.transform.rotation = Quaternion.identity;
        gamePiece.SetCoord(x, y);
    }

    void FillRandom()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject randomPiece = Instantiate(GetRandomGamePiece(), Vector3.zero, Quaternion.identity) as GameObject;
                if (randomPiece != null)
                {
                    PlaceGamePiece(randomPiece.GetComponent<GamePiece>(), i, j);
                }
            }
        }
    }
}
