namespace TestOOP
{
    public class Person
    {
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Student : Person
    {
        public Student(string name, int age, int score)
            : base(name, age)
        {
            Score = score;
        }

        public int Score { get; set; }
    }

    [TestClass]
    public class TestCast
    {
        /// <summary>
        /// // 파생 클래스로 캐스팅하면 예외가 발생한다.
        /// </summary>
        [TestMethod]
        public void TestCastBaseInstanceToDerivedType()
        {
            Person person = new Person("Jane", 20);

            Assert.ThrowsException<InvalidCastException>(() => 
            { 
                var student = (Student)person; 
            });
        }

        [TestMethod]
        public void TestIsOperator()
        {
            Person personTypePerson = new Person("Jane", 20);
            Assert.IsTrue(personTypePerson is Person);
            Assert.IsFalse(personTypePerson is Student);

            Person personTypeStudent = new Student("Mike", 22, 90);
            Assert.IsTrue(personTypeStudent is Person);
            Assert.IsTrue(personTypeStudent is Student);
        }


        [TestMethod]
        public void TestAsOperator()
        {
            Person personTypePerson = new Person("Jane", 20);
            Person personTypeStudent = new Student("Mike", 22, 90);

            Student? studentTypePersopn = personTypePerson as Student;
            Student? studentTypeStudent = personTypeStudent as Student;

            Assert.IsNull(studentTypePersopn);
            Assert.IsNotNull(studentTypeStudent);
        }
    }
}