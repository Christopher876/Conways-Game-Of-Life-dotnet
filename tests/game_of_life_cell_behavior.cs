using NUnit.Framework;

namespace game_of_life.tests
{
    [TestFixture]
    public class test1
    {
        [Test]
        public void Test1(){
            int i = 0;
            Assert.Pass();
        }

        [Test]
        public void Test2(){
            Assert.Fail();
        }   
    }
}