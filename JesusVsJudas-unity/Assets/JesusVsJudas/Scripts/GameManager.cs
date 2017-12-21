using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public GameObject gridUI;
    public GameObject endGamePanel;
    public CellState currentTurn = CellState.Judas;

    private List<List<CellState>> _grid = new List<List<CellState>>();

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start () {
        for (int i = 0; i < 3; i++) {
            _grid.Add(new List<CellState>());
            for (int j = 0; j < 3; j++) {
                _grid[i].Add(CellState.None);
            }
        }
	}

    private CellState _CalculateWinner() {
        CellState winner = CellState.None;
        // row check
        for (int i = 0; i < 3; i++) {
            CellState rowWinner = _grid[i][0];
            winner = rowWinner;
            for (int j = 1; j < 3; j++) {
                if(_grid[i][j] != rowWinner) {
                    winner = CellState.None;
                    break;
                }
            }
            if(winner != CellState.None){
                return winner;
            }
        }
        // column check
        for (int i = 0; i < 3; i++) {
            CellState columnWinner = _grid[0][i];
            winner = columnWinner;
            for (int j = 1; j < 3; j++) {
                if (_grid[j][i] != columnWinner) {
                    winner = CellState.None;
                    break;
                }
            }
            if (winner != CellState.None) {
                return winner;
            }
        }
        // diagonal check
        CellState diagonalWinner = _grid[0][0];
        winner = diagonalWinner;
        for (int i = 1; i < 3; i++) {
            if(_grid[i][i] != diagonalWinner) {
                winner = CellState.None;
                break;
            }
        }
        if (winner != CellState.None) {
            return winner;
        }
        // reveresed diagonal check
        CellState reversedDiagonalWinner = _grid[0][2];
        winner = reversedDiagonalWinner;
        for (int i = 1; i < 3; i++) {
            if (_grid[i][2 - i] != reversedDiagonalWinner) {
                winner = CellState.None;
                break;
            }
        }
        if (winner != CellState.None) {
            return winner;
        }
        return CellState.None;
    }

    private bool _MovesAvailable() {
        for (int i = 0; i < _grid.Count; i++) {
            for (int j = 0; j < _grid[i].Count; j++) {
                if (_grid[i][j] == CellState.None) {
                    return true;
                }
            }
        }
        return false;
    }

    private void _NextTurn() {
        if (currentTurn == CellState.Jesus) {
            currentTurn = CellState.Judas;
        } else {
            currentTurn = CellState.Jesus;
        }
        CellState state = _CalculateWinner();
        if (state != CellState.None) {
            _ShowEndGameScreen(state);
        } else if (!_MovesAvailable()) {
            _ShowEndGameScreen(CellState.None);
        }
    }

    private void _UpdateGrid() {
        Cell[] cells = gridUI.transform.GetComponentsInChildren<Cell>();
        for (int i = 0; i < cells.Length; i++) {
            cells[i].state = _grid[i % 3][i / 3];
        }
    }

    public void OnClickCellAtIndex(int index) {
        if(_grid[index / 3][index % 3] != CellState.None) {
            Debug.Log("Move is illegal, try again");
        } else {
            _grid[index / 3][index % 3] = currentTurn;
            Cell cell = gridUI.transform.GetChild(index).GetComponent<Cell>();
            cell.state = _grid[index / 3][index % 3];
            _NextTurn();
        }
    }

    private void _ShowEndGameScreen(CellState state) {
        endGamePanel.SetActive(true);
        Text winnerTextField = endGamePanel.transform.GetComponentInChildren<Text>();
        string winnerString;
        if(state == CellState.Jesus) {
            winnerString = "Jesus won";
        }else if(state == CellState.Judas) {
            winnerString = "Judas won";
        } else {
            winnerString = "Draw";
        }
        winnerTextField.text = winnerString;
    }

    public void RestartGame() {
        SceneManager.LoadScene("JesusVsJudas");
    }
}
