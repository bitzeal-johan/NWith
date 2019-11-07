# NWith - For immutable types in C#
NWith for C# makes it easy to create modified clones of immutable objects.

You can for example create an immutable type by making all the fields readonly, and then 
clone it and just change one member using the following example.

To use immutable types with C# and allow for easy cloning using With, you can use this.

Please tell me what you think.

```
  public class GuineaPig
  {
      public readonly string Name;
      public readonly int Age;
      public GuineaPig(string name, int age)
      {
          Name = name;
          Age = age;
      }
  }

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
  ```
