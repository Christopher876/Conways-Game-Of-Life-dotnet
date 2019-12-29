using NUnit.Framework;

namespace game_of_life.tests
{
    [TestFixture]
    public class test1
    {
        [Test]
        public void game_of_life_CellIsDead_ShouldComeBackToLife(){
            Cell cell = new Cell();
            cell.cellState = CellState.Dead;
            cell.Update(3);
            Assert.AreEqual(CellState.Alive,cell.cellState);
        }   

    }
}