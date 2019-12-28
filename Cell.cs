using SFML.System;

namespace game_of_life
{
    public enum CellState{
        Alive,
        Dead
    }
    public class Cell
    {
        private CellState cellState;
        public Vector2f location;
        public Cell(Vector2f location){
            this.location = location;
        }

        public void Update(){

        }

        private void CheckSurroundingCells(){

        }
    }
}