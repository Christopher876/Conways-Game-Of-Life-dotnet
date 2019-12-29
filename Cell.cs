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

        public void Update(int neighbors){
            /*TODO
                -ADD UPDATE LOGIC TO CHECK THE 8 LOCATIONS AROUND THE CELL.
                -ADD CHECK FOR WHEN THE CELL RESIDES ON THE TOP ROW, LEFTMOST COLUMN, RIGHTMOST COLUMN OR BOTTOM ROW 
                SO THAT THERE WILL BE NO OUTOFBOUND ERROR 
            */
            switch(neighbors){
                //Cell comes back to life if there are 3 neighbors
                case int n when (neighbors == 3 && cellState == CellState.Dead):
                    cellState = CellState.Alive;
                    break;
                //Dies from loneliness
                case int n when (neighbors < 2):
                    cellState = CellState.Dead;
                    break;
                //Cell lives to next generation
                case int n when (neighbors == 2 || neighbors == 3 && cellState == CellState.Alive):
                    break;
                //Dies from overpopulation
                case int n when (neighbors > 3):
                    cellState = CellState.Dead;
                    break;
            }
        }
    }
}