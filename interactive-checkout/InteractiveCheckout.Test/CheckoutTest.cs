using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Interfaces;
using NSubstitute;

namespace InteractiveCheckout.Test;

public class CheckoutTest
{
    //[SetUp]
    //public void Setup()
    //{}

    [Test]
    public void send_email_when_user_confirms_termsAndConditions_and_newsletter()
    {
        var emailService = Substitute.For<IEmailService>();
        var polkaDotSocks = new Product("Polka-dot Socks");

        var checkout = new ForTestingCheckout(polkaDotSocks, emailService, true);
        checkout.ConfirmOrder();

        emailService.Received(1).SubscribeUserFor(polkaDotSocks);
    }

    [Ignore("")]
    [Test]
    public void excepcion_when_user_discard_termsAndConditions()
    {
        var emailService = Substitute.For<IEmailService>();
        var polkaDotSocks = new Product("Polka-dot Socks");

        var checkout = new ForTestingCheckout(polkaDotSocks, emailService, false);

        Assert.Throws<OrderCancelledException>(() => checkout.ConfirmOrder());
    }


    public class ForTestingCheckout : Checkout
    {
        private readonly bool _confirmation;

        public ForTestingCheckout(Product product, IEmailService emailService, bool confirmation) : base(product, emailService)
        {
            _confirmation = confirmation;
        }

        protected override UserConfirmation CreateUserConfirmation(string message)
        {
            return new UserConfirmationForTesting(message, _confirmation);
        }
    }

    public class UserConfirmationForTesting : UserConfirmation
    {
        private readonly bool _confirmation;
        public string messageReceived;

        public UserConfirmationForTesting(string message, bool confirmation) : base(message)
        {
            _confirmation = confirmation;
        }

        protected override string InputReader()
        {
            //if (_confirmation)
            //{
            //    return "y";
            //}

            //return "n";
            return "y";
        }

        protected override void Notify(string message)
        {
            messageReceived = message;
        }
    }
}