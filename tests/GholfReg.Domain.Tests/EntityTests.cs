using Xunit;
using Gholfreg.Domain;
using System;

namespace GholfReg.Domain.Tests
{
    public class EntityTests
    {
        [Fact]
        public void Equals_SameId_ShouldBeTrue()
        {
            var test1 = new TestDummy();
            var test2 = new TestDummy() { Id = test1.Id };

            Assert.True(test1.Equals(test2));
            Assert.True(test1 == test2);
        }

        [Fact]
        public void Equals_DifferentId_ShouldBeFalse()
        {
            var test1 = new TestDummy();
            var test2 = new TestDummy();

            Assert.False(test1.Equals(test2));
            Assert.True(test1 != test2);
        }

        [Fact]
        public void Equals_Objects_ShouldBeTrue()
        {
            var test1 = new TestDummy();
            var test2 = new TestDummy() { Id = test1.Id };

            Assert.True(test1.Equals((object)test2));
        }

        [Fact]
        public void Equal_OtherNull_ShouldBeFalse()
        {
            var test1 = new TestDummy();
            TestDummy test2 = null;

            Assert.False(test1.Equals(test2));
            Assert.False(test1 == test2);
        }
    }

    public class TestDummy: Entity
    {

    }
}