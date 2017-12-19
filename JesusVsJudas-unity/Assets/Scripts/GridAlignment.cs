using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GridAlignment : MonoBehaviour {

    [Range(0.0f, 1f / 3f)]
    public float cellPercentage = 0.24f;
    private float _spacingPercentage = 0.07f;

    private GridLayoutGroup _gridLayoutGroup;

    private void Awake() {
        
    }

    // Use this for initialization
    void Start () {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _spacingPercentage = (1f - cellPercentage * 3f) / 4f;
    }
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
        _spacingPercentage = (1f - cellPercentage * 3f) / 4f;
#endif

        RectTransform rt = (RectTransform)transform;
        int spacingPoints = (int)(_spacingPercentage * rt.rect.height);
        _gridLayoutGroup.padding = new RectOffset(spacingPoints, spacingPoints, spacingPoints, spacingPoints);
        _gridLayoutGroup.spacing = new Vector2(spacingPoints, spacingPoints);

        int cellPoints = (int)(cellPercentage * rt.rect.width);
        _gridLayoutGroup.cellSize = new Vector2(cellPoints, cellPoints);
    }
}
