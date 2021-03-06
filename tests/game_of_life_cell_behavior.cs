using NUnit.Framework;

namespace game_of_life.tests
{
    [TestFixture]
    public class Test1
    {
        [Test]
        public void game_of_life_CellIsDead_ShouldComeBackToLife([Values(3)] int i){
            Cell cell = new Cell {cellState = CellState.Dead};
            cell.Update(i);
            Assert.AreEqual(CellState.Alive,cell.cellState);
        }   

        [Test]
        public void game_of_life_CellIsDead_ShouldStayDead([Values(1,2,4,5,6,7,8)] int i){
            Cell cell = new Cell {cellState = CellState.Dead};
            cell.Update(i);
            Assert.AreEqual(CellState.Dead,cell.cellState);
        } 

        [Test]
        public void game_of_life_CellIsAlive_ShouldDie([Values(0,1,4,5,6,7,8)] int i){
            Cell cell = new Cell {cellState = CellState.Alive};
            cell.Update(i);
            Assert.AreEqual(CellState.Dead,cell.cellState);
        }   

    }
}