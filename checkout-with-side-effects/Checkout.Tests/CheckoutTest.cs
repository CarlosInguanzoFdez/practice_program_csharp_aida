using NUnit.Framework;

namespace Checkout.Tests;

public class CheckoutTest
{
    [Test]
    public void store_receipt()
    {
        var checkout = new ForTestingCheckOut();

        checkout.CreateReceipt(new Money(100));

        Assert.That(checkout._receiptStored, Is.EqualTo(new Receipt(new Money(100), new Money(20), new Money(120))));
    }

    public class ForTestingCheckOut : Checkout
    {
        public Receipt _receiptStored;

        protected override void Store(Receipt receipt)
        {
            _receiptStored = receipt;
        }
    }
}