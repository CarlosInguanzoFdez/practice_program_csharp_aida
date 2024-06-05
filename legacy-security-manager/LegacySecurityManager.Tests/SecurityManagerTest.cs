using System.Security.Cryptography.X509Certificates;
using LegacySecurityManager;
using NSubstitute;
using NUnit.Framework;

namespace LegacySecurityManager.Tests
{
    public class SecurityManagerTest
    {
        private Notifier _notifier;
        private Reader? _reader;
        private SecurityManager _securityManager;

        [SetUp]
        public void SetUp()
        {
            _notifier = Substitute.For<Notifier>();
            _reader = Substitute.For<Reader>();
            _securityManager = new SecurityManager(_notifier, _reader);
        }

        [Test]
        public void password_and_confirm_password_are_not_equals_show_error() {
            _reader.Read().Returns("Carlos", "Carlos Inguanzo", "password", "notEqualsPassword");

            _securityManager.CreateUserInstance();

            AssertThatLastMessageIs("The passwords don't match");
        }

        [Test]
        public void password_less_than_8_characters_show_error()
        {
            _reader.Read().Returns("Raúl", "Corchero", "passwor", "passwor");

            _securityManager.CreateUserInstance();

            AssertThatLastMessageIs("Password must be at least 8 characters in length");
        }

        [Test]
        public void create_user_show_saving_details_message()
        {
            _reader.Read().Returns("Manuel", "Gallud", "password", "password");

            _securityManager.CreateUserInstance();

            AssertThatLastMessageIs("Saving Details for User (Manuel, Gallud, drowssap)\n");
        }

        private void AssertThatLastMessageIs(string lastMessage) {
            Received.InOrder(() =>
            {
                _notifier.Received(1).Notify("Enter a username");
                _notifier.Received(1).Notify("Enter your full name");
                _notifier.Received(1).Notify("Enter your password");
                _notifier.Received(1).Notify("Re-enter your password");
                _notifier.Received(1).Notify(lastMessage);
            });
        }
    }
}