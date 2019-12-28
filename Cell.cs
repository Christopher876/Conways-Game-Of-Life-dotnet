using SFML.System;

namespace game_of_life
{
    public enum CellState{
        Alive,
        Dead
    }
    public class Cell
    {
        public CellState cellState;
        public Vector2f location;

        public Cell(){
            this.cellState = CellState.Dead;
        }

        public void Update(){
            /*TODO
                -ADD UPDATE LOGIC TO CHECK THE 8 LOCATIONS AROUND THE CELL.
                -ADD CHECK FOR WHEN THE CELL RESIDES ON THE TOP ROW, LEFTMOST COLUMN, RIGHTMOST COLUMN OR BOTTOM ROW 
                SO THAT THERE WILL BE NO OUTOFBOUND ERROR 
            */
        }

        private void CheckSurroundingCells(){

        }
    }
}