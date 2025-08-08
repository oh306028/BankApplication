using BankApplication.Data.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BankApplication.App.Printers
{
    public class BankAccountDetailsPrinter : IDocument
    {
        public BankAccount BankAccount { get; }

        public BankAccountDetailsPrinter(BankAccount bankAccount)
        {
            BankAccount = bankAccount;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .PaddingBottom(20)
                    .AlignCenter()
                    .Text("Szczegóły rachunku bankowego")
                    .FontSize(20)
                    .Bold();


                page.Content().Column(column =>
                {
                    column.Spacing(10);

                    column.Item().PaddingBottom(5).Text("Dane klienta")
                        .FontSize(16)
                        .Bold()
                        .Underline();

                    column.Item().Text($"Imię i nazwisko: {BankAccount.Client?.FullName ?? "Brak danych"}");
                    column.Item().Text($"Email: {BankAccount.Client?.Email ?? "Brak danych"}");
                    column.Item().Text($"Telefon: {BankAccount.Client?.Phone ?? "Brak danych"}");

                    var client = BankAccount.Client;
                    if (client != null)
                    {
                        string address = $"{client.Country}, {client.City}, {client.PostalCode}, {client.Number}";
                        column.Item().Text($"Adres: {address}");
                    }
                    else
                    {
                        column.Item().Text("Adres: Brak danych");
                    }

                    column.Item().PaddingTop(20);

                    column.Item().PaddingBottom(5).Text("Dane rachunku")
                        .FontSize(16)
                        .Bold()
                        .Underline();

                    column.Item().Text($"Numer rachunku: {BankAccount.BankAccountNumber}");
                    column.Item().Text($"Saldo: {BankAccount.Balance:N2} {BankAccount.Currency}");
                    column.Item().Text($"Typ rachunku: {BankAccount.DisplayType}");
                    if (BankAccount.InteresetRate.HasValue)
                        column.Item().Text($"Oprocentowanie: {BankAccount.InteresetRate.Value:P2}");
                    else
                        column.Item().Text("Oprocentowanie: brak");

                });

                page.Footer().AlignRight().Text(text =>
                {
                    text.Span("Wydrukowano: ").SemiBold().FontSize(9);
                    text.Span(DateTime.Now.ToString("g")).FontSize(9);
                });
            });
        }
    }
}
