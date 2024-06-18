namespace Checkout;

public class Checkout
{
    public Receipt CreateReceipt(Money amount)
    {
        var vat = amount.Percentage(20);

        var receipt = new Receipt(amount, vat, amount.Add(vat));

        Store(receipt);

        return receipt;
    }

    protected virtual void Store(Receipt receipt)
    {
        ReceiptRepository.Store(receipt);
    }

    // Se almacenan los datos en el repository -> retorna recep
}