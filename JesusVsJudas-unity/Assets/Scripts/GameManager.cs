using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    public GameObject GridUI;
    public CellState currentTurn = CellState.judas;

    private List<List<CellState>> grid = new List<List<CellState>>();
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {

        for (int i = 0; i < 3; i++) {
            grid.Add(new List<CellState>());
            for (int j = 0; j < 3; j++) {
                grid[i].Add(CellState.none);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public CellState CheckWinner() {
        CellState winner = CellState.none;
        // row check
        for (int i = 0; i < 3; i++) {
            CellState rowWinner = grid[i][0];
            winner = rowWinner;
            for (int j = 1; j < 3; j++) {
                if(grid[i][j] != rowWinner) {
                    winner = CellState.none;
                    break;
                }
            }
            if(winner != CellState.none){
                return winner;
            }
        }
        // column check
        for (int i = 0; i < 3; i++) {
            CellState columnWinner = grid[0][i];
            winner = columnWinner;
            for (int j = 1; j < 3; j++) {
                if (grid[j][i] != columnWinner) {
                    winner = CellState.none;
                    break;
                }
            }
            if (winner != CellState.none) {
                return winner;
            }
        }
        // diagonal check
        CellState diagonalWinner = grid[0][0];
        winner = diagonalWinner;
        for (int i = 1; i < 3; i++) {
            if(grid[i][i] != diagonalWinner) {
                winner = CellState.none;
                break;
            }
            if (winner != CellState.none) {
                return winner;
            }
        }
        // reveresed diagonal check
        CellState reversedDiagonalWinner = grid[0][2];
        winner = reversedDiagonalWinner;
        for (int i = 1; i < 3; i++) {
            if (grid[i][2 - i] != reversedDiagonalWinner) {
                winner = CellState.none;
                break;
            }
            if (winner != CellState.none) {
                return winner;
            }
        }
        return CellState.none;
    }

    public void NextTurn() {
        if (currentTurn == CellState.jesus) {
            currentTurn = CellState.judas;
        } else {
            currentTurn = CellState.jesus;
        }
        UpdateGrid();
    }

    private void UpdateGrid() {
        Cell[] cells = GridUI.transform.GetComponentsInChildren<Cell>();
        for (int i = 0; i < cells.Length; i++) {
            grid[i % 3][i / 3] = cells[i].State;
        }

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                Debug.Log(grid[i][j]);
            }
        }
    }
}
