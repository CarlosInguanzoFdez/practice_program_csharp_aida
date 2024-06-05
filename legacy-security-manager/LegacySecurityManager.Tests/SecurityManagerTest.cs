using LegacySecurityManager;
using NUnit.Framework;

namespace LegacySecurityManager.Tests
{
    public class SecurityManagerTest
    {
        [Test]
        public void password_and_confirm_password_are_not_equals_show_error()
        {
            var inputs = new List<string>() { "Carlos", "Carlos Inguanzo", "password", "notEqualsPassword" };
            var securityManager = new SecurityManagerForTesting(inputs);

            securityManager.CreateUserInstance();

            AssertThatLastMessageIs("The passwords don't match", securityManager);
        }

        [Test]
        public void password_less_than_8_characters_show_error()
        {
            var inputs = new List<string>() { "Raúl", "Corchero", "passwor", "passwor" };
            var securityManager = new SecurityManagerForTesting(inputs);

            securityManager.CreateUserInstance();

            AssertThatLastMessageIs("Password must be at least 8 characters in length", securityManager);
        }

        [Test]
        public void create_user_show_saving_details_message() {
            var inputs = new List<string>() { "Manuel", "Gallud", "password", "password" };
            var securityManager = new SecurityManagerForTesting(inputs);

            securityManager.CreateUserInstance();

            AssertThatLastMessageIs("Saving Details for User (Manuel, Gallud, drowssap)\n", securityManager);
        }

        private void AssertThatLastMessageIs(string lastMessage, SecurityManagerForTesting securityManager)
        {
            var expectedMessages = GetExpectedMessages(lastMessage);
            Assert.That(securityManager.PrintedMessages, Is.EquivalentTo(expectedMessages));
        }

        private List<string> GetExpectedMessages(string lastMessage)
        {
            return new List<string>() { "Enter a username", "Enter your full name", "Enter your password", "Re-enter your password", lastMessage };
        }
    }

    internal class SecurityManagerForTesting : SecurityManager
    {
        private readonly Queue<string> _inputs;
        public List<string> PrintedMessages;

        public SecurityManagerForTesting(List<string> inputs) : base(new ConsoleNotifier(), new ConsoleReader())
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