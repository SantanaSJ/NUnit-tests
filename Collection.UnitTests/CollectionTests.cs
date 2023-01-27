using Collections;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Colelction.UnitTests
{
    public class CollectionTests
    {
        [Test]
        public void Test_Collection_Empty_Constructor()
        {
            //arrange and act
            var collection = new Collections<int>();

            //assert
            Assert.That(collection.ToString(), Is.EqualTo("[]"));
            Assert.That(collection.Capacity, Is.EqualTo(16));
        }

        [Test]
        public void Test_Collection_Constructor_Single_Item()
        {
            //arrange and act
            var collection = new Collections<int>(5);

            //assert
            Assert.That(collection.ToString(), Is.EqualTo("[5]"));
        }

        [Test]
        public void Test_Collection_Constructor_Mutiple_Items()
        {
            //arrange and act
            var collection = new Collections<int>(5, 6);

            //assert
            Assert.That(collection.ToString(), Is.EqualTo("[5, 6]"));
        }

        [Test]
        public void Test_Collection_Count_And_Capacity()
        {
            //arrange and act
            var collection = new Collections<int>(5, 6);

            //assert
            Assert.That(collection.Count, Is.EqualTo(2), "Check for count");
            Assert.That(collection.Capacity, Is.GreaterThan(collection.Count));

        }

        [Test]
        public void Test_Collection_Add()
        {
            var collection = new Collections<string>("Ivan", "Pesho");

            collection.Add("Gosho");

            Assert.That(collection.ToString(), Is.EqualTo("[Ivan, Pesho, Gosho]"));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            //arrange
            var collection = new Collections<int>(5, 6, 7);
            var item = collection[1];

            Assert.That(item.ToString(), Is.EqualTo("6"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            //arrange
            var collection = new Collections<int>(5, 6, 7);
            collection[1] = 666;

            Assert.That(collection.ToString(), Is.EqualTo("[5, 666, 7]"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            //arrange
            var collection = new Collections<string>("Ivan", "Pesho");

            Assert.That(() => { var item = collection[2]; }, 
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_Add_With_Grow()
        {
            //arrange
            var collection = new Collections<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            //act
            collection.Add(17);

            Assert.That(collection.ToString(), Is.EqualTo("[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17]"));
            Assert.That(collection.Count, Is.EqualTo(17));
            Assert.That(collection.Capacity, Is.GreaterThan(collection.Count));
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            //arrange
            var collection = new Collections<int>(1, 2);

            //act
            collection.AddRange(3, 4, 5, 6, 7);

            Assert.That(collection.ToString(), Is.EqualTo("[1, 2, 3, 4, 5, 6, 7]"));
            Assert.That(collection.Count, Is.EqualTo(7));
            Assert.That(collection.Capacity, Is.GreaterThan(collection.Count));
        }

        [Test]
        public void Test_Collection_Set_By_Invalid_Index()
        {
            //act
            Assert.That(() => { var collection = new Collections<int>(5, 6, 7); collection[3] = 666; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_AddRange_With_Grow()
        {
            var nums = new Collections<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }


        [Test]
        public void Test_Collection_Insert_At_Start()
        {
            var collection = new Collections<int>(1, 2, 3);

            collection.InsertAt(0, 4);

            Assert.That(collection.ToString(), Is.EqualTo("[4, 1, 2, 3]"));
            Assert.That(collection.Count, Is.EqualTo(4));
        }

        [Test]
        public void Test_Collection_Insert_At_End()
        {
            var collection = new Collections<int>(1, 2, 3);

            collection.InsertAt(3, 4);

            Assert.That(collection.ToString(), Is.EqualTo("[1, 2, 3, 4]"));
            Assert.That(collection.Count, Is.EqualTo(4));
        }

        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            var collection = new Collections<int>(1, 2, 3);

            collection.InsertAt(1, 4);

            Assert.That(collection.ToString(), Is.EqualTo("[1, 4, 2, 3]"));
            Assert.That(collection.Count, Is.EqualTo(4));
        }

        [Test]
        public void Test_Collection_InsertAtWithGrow()
        {
            var collection = new Collections<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            collection.InsertAt(0, 17);

            Assert.That(collection.ToString(), Is.EqualTo("[17, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]"));
            Assert.That(collection.Count, Is.EqualTo(17));
            Assert.That(collection.Capacity, Is.GreaterThan(collection.Count));
        }

        [Test]
        public void Test_Collection_InsertAtInvalidIndex()
        {
        var collection = new Collections<int>(1, 2, 3);
            Assert.That(() => collection.InsertAt(5, 4),
                Throws.TypeOf<ArgumentOutOfRangeException>());
            //Assert.That(() => { var collection = new Collections<int>(1, 2, 3); collection.InsertAt(5, 4); },
            //    Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            var collection = new Collections<int>(1, 2, 3);

            collection.Exchange(1, 2);

            Assert.That(collection.ToString(), Is.EqualTo("[1, 3, 2]"));
        }

        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            var collection = new Collections<int>(1, 2, 3);

            collection.Exchange(0, 2);

            Assert.That(collection.ToString(), Is.EqualTo("[3, 2, 1]"));
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndexes()
        {
            Assert.That(() => { var collection = new Collections<int>(1, 2, 3); ; collection.Exchange(3, 4); },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            var collection = new Collections<int>(1, 2, 3);
            collection.RemoveAt(0);

            Assert.That(collection.ToString(), Is.EqualTo("[2, 3]"));
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            var collection = new Collections<int>(1, 2, 3);
            collection.RemoveAt(2);

            Assert.That(collection.ToString(), Is.EqualTo("[1, 2]"));
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            var collection = new Collections<int>(1, 2, 3);

            collection.RemoveAt(1);

            Assert.That(collection.ToString(), Is.EqualTo("[1, 3]"));
        }

        [Test]
        public void Test_Collection_RemoveAtInvalidIndex()
        {
            Assert.That(() => { var collection = new Collections<int>(1, 2, 3); ; collection.RemoveAt(3); },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_RemoveAll()
        {
            var collection = new Collections<int>(1, 2, 3);

           while (collection.Count > 0)
            {
                collection.RemoveAt(0);
            }
            Assert.That(collection.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_Clear()
        {
            var collection = new Collections<int>(1, 2, 3);

            collection.Clear();

            Assert.That(collection.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ToString_Empty()
        {
            var collection = new Collections<int>();

            string expected = "[]";

            Assert.That(collection.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void Test_Collection_ToString_Single()
        {
            var collection = new Collections<string>("testString");

            string expected = "[testString]";

            Assert.That(collection.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void Test_Collection_ToString_Multiple()
        {
            var collection = new Collections<string>("testString1", "testString2");

            string expected = "[testString1, testString2]";

            Assert.That(collection.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collections<string>("Teddy", "Gerry");
            var collection = new Collections<int>(10, 20);
            var dates = new Collections<DateTime>();
            var nested = new Collections<object>(names, collection, dates);
            string nestedToString = nested.ToString();
            Assert.That(nestedToString,
              Is.EqualTo("[[Teddy, Gerry], [10, 20], []]"));
        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000000;
            var collection = new Collections<int>();
            collection.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(collection.Count == itemsCount);
            Assert.That(collection.Capacity >= collection.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                collection.RemoveAt(i);
            Assert.That(collection.ToString() == "[]");
            Assert.That(collection.Capacity >= collection.Count);
        }
    }
}
