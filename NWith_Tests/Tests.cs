using NUnit.Framework;
using NWith;

namespace NWith_Tests
{
    public class Tests
    {
      [Test]
        public void WhenCallingWithThenCloneGetsNewValueAndOriginalKeepsOldValue()
        {
            // ARRANGE
            var pig = new GuineaPig("Fluffy", 0);
            var originalPig = pig;

            // ACT 
            for (int i = 0; i < 100000; i++) // for profiling
            {
                pig = pig.With(nameof(pig.Age), pig.Age + 1);
            }

            pig = pig.With(nameof(pig.Name), "Shawn");

            // ASSERT
            // Check the new pig
            Assert.AreEqual("Shawn", pig.Name);
            Assert.AreEqual(100000, pig.Age);

            // Check that original pig has not mutated
            Assert.AreEqual(0, originalPig.Age);
            Assert.AreEqual("Fluffy", originalPig.Name);
        }

        [Test]
        public void GivenSourceIsNullWhenCallingWithThenShallCreateDefaultObject()
        {
            // ARRANGE
            GuineaPig noPig = null;

            // ACT
            var noClone = noPig.With(nameof(noPig.Age), 1);

            // ASSERT
            Assert.IsNull(noClone);
        }
    }
}