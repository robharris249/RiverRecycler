using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridBuildingSystem : MonoBehaviour {

    public static GridBuildingSystem Instance { get; private set; }

    public event EventHandler OnSelectedChanged;
    public event EventHandler OnObjectPlaced;

    public int gridWidth;
    public int gridHeight;
    public float cellSize;

    public bool isRiverGrid;

    public Grid<GridObject> grid;
    [SerializeField] private List<GameObject> buildingList = null;
    private GameObject selectedBuilding;
    public GameObject horizontalLines;
    public GameObject verticalLines;

    private void Awake() {
        Instance = this;

        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, 
            this.gameObject.transform.position, 
            (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
        
        horizontalLines.GetComponent<GridLines>().DrawHorizontalLines(gridWidth, gridHeight, cellSize);//Draw Horizontal grid lines
        verticalLines.GetComponent<GridLines>().DrawVerticalLines(gridWidth, gridHeight, cellSize);    //Now Draw Vertical grid lines

        if (isRiverGrid) {
            Vector2[] riverTiles = new Vector2[] {
            new Vector2(0, 6),
            new Vector2(0, 7),
            new Vector2(0, 8),
            new Vector2(1, 6),
            new Vector2(1, 7),
            new Vector2(1, 8),
            new Vector2(2, 6),
            new Vector2(2, 7),
            new Vector2(2, 8),
            new Vector2(3, 6),
            new Vector2(3, 7),
            new Vector2(3, 8),
            new Vector2(4, 6),
            new Vector2(4, 7),
            new Vector2(4, 8),
            new Vector2(5, 6),
            new Vector2(5, 7),
            new Vector2(5, 8),
            new Vector2(6, 5),
            new Vector2(6, 6),
            new Vector2(6, 7),
            new Vector2(7, 3),
            new Vector2(7, 4),
            new Vector2(7, 5),
            new Vector2(7, 6),
            new Vector2(7, 7),
            new Vector2(8, 2),
            new Vector2(8, 3),
            new Vector2(8, 4),
            new Vector2(8, 5),
            new Vector2(8, 6),
            new Vector2(8, 7),
            new Vector2(9, 2),
            new Vector2(9, 3),
            new Vector2(9, 4),
            new Vector2(9, 5),
            new Vector2(9, 6),
            new Vector2(10, 2),
            new Vector2(10, 3),
            new Vector2(10, 4),
            new Vector2(10, 5),
            new Vector2(11, 1),
            new Vector2(11, 2),
            new Vector2(11, 3),
            new Vector2(11, 4),
            new Vector2(12, 1),
            new Vector2(12, 2),
            new Vector2(12, 3),
            new Vector2(12, 4),
            new Vector2(13, 1),
            new Vector2(13, 2),
            new Vector2(13, 3),
            new Vector2(14, 1),
            new Vector2(14, 2),
            new Vector2(14, 3),
            new Vector2(15, 1),
            new Vector2(15, 2),
            new Vector2(15, 3),
            new Vector2(16, 1),
            new Vector2(16, 2),
            new Vector2(16, 3),
            new Vector2(17, 1),
            new Vector2(17, 2),
            new Vector2(17, 3),
            new Vector2(18, 2),
            new Vector2(18, 3),
            new Vector2(19, 2),
            new Vector2(19, 3),
            new Vector2(19, 4),
            new Vector2(19, 5),
            new Vector2(19, 6),
            new Vector2(19, 7),
            new Vector2(20, 3),
            new Vector2(20, 4),
            new Vector2(20, 5),
            new Vector2(20, 6),
            new Vector2(20, 7),
            new Vector2(20, 8),
            new Vector2(21, 4),
            new Vector2(21, 7),
            new Vector2(21, 8),
            new Vector2(22, 7),
            new Vector2(22, 8),
            new Vector2(22, 9),
            new Vector2(23, 8),
            new Vector2(23, 9),
            new Vector2(24, 8),
            new Vector2(24, 9),
        }; //Manually entered river tile co-ords, there is a more elegant way of doing this but would be a bit of over engineering 

            for (int i = 0; i < riverTiles.Length; i++) {                                          //loop through the river tiles
                grid.GetGridObject((int)riverTiles[i].x, (int)riverTiles[i].y).isRiverTile = true;//set those grid squares as river tiles
            }

            Vector2[] UITiles = new Vector2[] {
                new Vector2(12, 8),
                new Vector2(12, 9),
                new Vector2(12, 10),
                new Vector2(13, 8),
                new Vector2(13, 9),
                new Vector2(13, 10),
                new Vector2(14, 8),
                new Vector2(14, 9),
                new Vector2(14, 10),
                new Vector2(15, 8),
                new Vector2(15, 9),
                new Vector2(15, 10),
                new Vector2(16, 8),
                new Vector2(16, 9),
                new Vector2(16, 10),

            }; //Manually entered UI tile co-ords, there is a more elegant way of doing this but would be a bit of over engineering 

            for (int i = 0; i < UITiles.Length; i++) {                                   //loop through the UITiles
                grid.GetGridObject((int)UITiles[i].x, (int)UITiles[i].y).isUITile = true;//set those grid squares as UI tiles
            }

        }
    }

    public class GridObject {

        private Grid<GridObject> grid;
        private int x;
        private int y;
        public GameObject placedObject;
        public bool isRiverTile = false;
        public bool isUITile = false;
        public bool occupied = false;

        public GridObject(Grid<GridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            placedObject = null;
        }

        public override string ToString() {
            return x + ", " + y + "\n" + placedObject;
        }

        public void SetPlacedObject(GameObject placedObject) {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x, y);
        }

        public void ClearPlacedObject() {
            placedObject = null;
            grid.TriggerGridObjectChanged(x, y);
        }

        public GameObject GetPlacedObject() {
            return placedObject;
        }

        public bool CanBuild() {
            return placedObject == null;
        }
    }

    public int GetSelectedBuildingCost() {
        if (selectedBuilding.tag == "Grabbapult") {
            return selectedBuilding.GetComponent<Grabbapult>().GetCost();
        } else if (selectedBuilding.tag == "AlgaeChucker") {
            return selectedBuilding.GetComponent<AlgaeChucker>().GetCost();
        } else if (selectedBuilding.tag == "MagicMagnet") {
            return selectedBuilding.GetComponent<MagicMagnet>().GetCost();
        } else {
            Debug.Log("Error Has Occured");
            return 0;
        }
    }

    public GameObject GetSelectedBuilding() {
        return selectedBuilding;
    }

    public bool Build(int money, float rotation) {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();  //Get the mouse position
        grid.GetXY(mousePosition, out int x, out int y);             //Translate that into which grid square co-ords 

        if(isOutOfBounds(x,y)) {
            return false;
        }
        else if (grid.GetGridObject(x, y).isRiverTile) {
            UtilsClass.CreateWorldTextPopup("Cannot Build in the River!", mousePosition);
            FindObjectOfType<AudioManager>().Play("Cant Build");
            return false;
        } else if (grid.GetGridObject(x, y).occupied) {
            UtilsClass.CreateWorldTextPopup("Cannot Build Here! Building In the way", mousePosition);
            FindObjectOfType<AudioManager>().Play("Cant Build");
            return false;
        } else if (grid.GetGridObject(x, y).isUITile) {
            return false;
        } else {
            Vector3 gridOffset = new Vector3(cellSize/2, cellSize/2);
            GameObject placedObject = Instantiate(selectedBuilding, grid.GetWorldPosition(x, y) + gridOffset, Quaternion.Euler(0f, 0f, rotation));
            grid.GetGridObject(x, y).SetPlacedObject(placedObject);
            grid.GetGridObject(x, y).occupied = true;
            OnObjectPlaced?.Invoke(this, EventArgs.Empty);
            return true;
        }
    }

    public bool Build(float rotation) {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();  //Get the mouse position
        grid.GetXY(mousePosition, out int x, out int y);             //Translate that into which grid square co-ords 

        if (isOutOfBounds(x, y)) {
            return false;
        }
        else if (grid.GetGridObject(x, y).occupied) {
            UtilsClass.CreateWorldTextPopup("Cannot Build Here! Building In the way", mousePosition);
            FindObjectOfType<AudioManager>().Play("Cant Build");
            return false;
        } else {
            Vector3 gridOffset = new Vector3(4, 4);
            GameObject placedObject = Instantiate(selectedBuilding, grid.GetWorldPosition(x, y) + gridOffset, Quaternion.Euler(0f, 0f, rotation));
            grid.GetGridObject(x, y).SetPlacedObject(placedObject);
            grid.GetGridObject(x, y).occupied = true;
            OnObjectPlaced?.Invoke(this, EventArgs.Empty);
            return true;
        }
    }

    public bool isOutOfBounds(int x, int y) {

        if (x < 0 || x > gridWidth) {
            return true;
        } else if (y < 0 || y > gridHeight) {
            return true;
        }

        return false;
    }

    public void DeselectObjectType() {
        selectedBuilding = null; 
        RefreshSelectedObjectType();
    }

    private void RefreshSelectedObjectType() {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }


    public Vector2Int GetGridPosition(Vector3 worldPosition) {
        grid.GetXY(worldPosition, out int x, out int z);
        return new Vector2Int(x, z);
    }

    /// <summary>
    /// Method for selecting the contraptions to be placed, 0 to N in the list is passed through
    /// </summary>
    public void SetSelectedBuilding(int selection) {
        selectedBuilding = buildingList[selection];
        RefreshSelectedObjectType();
    }

}
