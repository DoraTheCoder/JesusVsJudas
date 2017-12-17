using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    public Sprite EmptySprite;
    public Sprite JesusSprite;
    public Sprite JudasSprite;

    public CellState State;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void UpdateImage() {
        Debug.Log("Update image");
        if (State == CellState.jesus) {
            transform.GetChild(0).GetComponent<Image>().sprite = JesusSprite;
        } else if (State == CellState.judas) {
            transform.GetChild(0).GetComponent<Image>().sprite = JudasSprite;
        }
    }

    public void ChangeCellState() {
        State = GameManager.Instance.currentTurn;
        UpdateImage();
        GameManager.Instance.NextTurn();
    }



}
