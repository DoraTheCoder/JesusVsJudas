using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    public Sprite emptySprite;
    public Sprite jesusSprite;
    public Sprite judasSprite;

    private CellState _state = CellState.None;
    public CellState state{
        set {
            _state = value;
            _UpdateImage();
        }
        get {
            return _state;
        }
    }

    private void _UpdateImage() {
        Debug.Log("Update image");
        if (state == CellState.Jesus) {
            transform.GetChild(0).GetComponent<Image>().sprite = jesusSprite;
        } else if (state == CellState.Judas) {
            transform.GetChild(0).GetComponent<Image>().sprite = judasSprite;
        }
    }

    public void ChangeCellState() {
        GameManager.instance.OnClickCellAtIndex(transform.GetSiblingIndex());
    }
}
