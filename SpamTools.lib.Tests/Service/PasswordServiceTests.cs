using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpamTools.lib.Service;
using System.Diagnostics;

namespace SpamTools.lib.Tests.Service
{
    [TestClass]
    public class PasswordServiceTests
    {

        [AssemblyInitialize]
        public static void AssembplyInitialize(TestContext context)
        {
            Debug.WriteLine($"Инициализация сборки для стеста " + context.TestName);
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Debug.WriteLine($"Инициализация класса для стеста " + context.TestName);
        }

        [TestInitialize]
        public void TestInittialize()
        {
            Debug.WriteLine($"Инициализация модульного теста");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Debug.WriteLine($"Очистка данных модульного теста");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Debug.WriteLine($"Очистка данных класса модульного теста");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Debug.WriteLine($"Очистка сборки модульного теста");
        }

        [TestMethod, Timeout(100)]
        [Description("Тестирвоание процессе кодирования строки")]
        [Priority(1)]
        public void Encode_123_234_Test()
        {
            //AAA
            // Arange, Act, Assert

            //Arange
            var str = "123";
            var expected_encrypted_str = "234";
            var key = 1;

            //Act
            var actual_encrypted_str = PasswordService.Encode(str, key);

            //Assert
            Assert.IsNotNull(actual_encrypted_str);
            Assert.IsInstanceOfType(actual_encrypted_str, typeof(string));
            //Assert.ThrowsException<>()
            Assert.AreEqual(expected_encrypted_str, actual_encrypted_str, "Ошибка кодирования строки");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Encode_Throw_ArgumentNullException_Test()
        {
            PasswordService.Encode(null, 1);
        }

        [TestMethod]
        public void Decode_234_123_Test()
        {
            var str = "234";
            var expected_decrypted_str = "123";
            var key = 1;

            var actual_decrypted_str = PasswordService.Decode(str, key);

            Assert.AreEqual(expected_decrypted_str, actual_decrypted_str);
            //CollectionAssert.
            //StringAssert.
            //throw new AssertFailedException();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Decode_Throw_ArgumentNullException_Test()
        {
            PasswordService.Decode(null, 1);
        }
    }
}
