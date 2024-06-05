using LegacySecurityManager;
using NUnit.Framework;

namespace LegacySecurityManager.Tests
{
    public class SecurityManagerTest
    {
        [Test]
        public void password_and_confirm_password_are_not_equals_show_error()
        {
            var inputs = new List<string>() { "userName", "fullName", "password", "notEqualsPassword" };
            var securityManager = new SecurityManagerForTesting(inputs);

            securityManager.CreateUserInstance();

            var expectedMessages = new List<string>(){ "Enter a username", "Enter your full name", "Enter your password", "Re-enter your password", "The passwords don't match" };
            Assert.That(securityManager.PrintedMessages, Is.EquivalentTo(expectedMessages));
        }

        [Test]
        public void password_less_than_8_characters_show_error()
        {
            var inputs = new List<string>() { "userName", "fullName", "passwo", "passwo" };
            var securityManager = new SecurityManagerForTesting(inputs);

            securityManager.CreateUserInstance();

            var expectedMessages = new List<string>() { "Enter a username", "Enter your full name", "Enter your password", "Re-enter your password", "Password must be at least 8 characters in length" };
            Assert.That(securityManager.PrintedMessages, Is.EquivalentTo(expectedMessages));
        }
    }

    internal class SecurityManagerForTesting : SecurityManager
    {
        private readonly Queue<string> _inputs;
        public List<string> PrintedMessages;

        public SecurityManagerForTesting(List<string> inputs)
        {
            _inputs = new Queue<string>(inputs);
            PrintedMessages = new List<string>();
        }

        protected override string ReadMessage()
        {
            return _inputs.Dequeue();
        }

        protected override void Print(string message)
        {
            PrintedMessages.Add(message);
        }
    }
}