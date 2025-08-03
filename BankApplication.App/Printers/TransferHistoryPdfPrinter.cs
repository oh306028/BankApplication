using BankApplication.App.Modules.BankAccount.Transfers.Models;
using BankApplication.App.Services.BankAccount;
using BankApplication.Data.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BankApplication.App.Printers
{
    public class TransferHistoryPdfPrinter : IDocument
    {
        public List<TransferDetails> Transfers { get; } 
        public TransferHistoryPdfPrinter(List<TransferDetails> transfers)
        {
            Transfers = transfers;  
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header().Text("Historia Przelewów").FontSize(18).Bold().AlignCenter();

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(100);
                        columns.RelativeColumn(1.5f);
                        columns.RelativeColumn(1.5f);
                        columns.ConstantColumn(70);
                        columns.RelativeColumn(3f);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Data").Bold().FontSize(10);
                        header.Cell().Text("Od").Bold().FontSize(10);
                        header.Cell().Text("Do").Bold().FontSize(10);
                        header.Cell().Text("Kwota").Bold().FontSize(10);
                        header.Cell().Text("Tytuł").Bold().FontSize(10);
                    });

                    foreach (var transfer in Transfers)
                    {
                        table.Cell()
                             .Text(transfer.TransferDate.ToString("dd.MM.yyyy, HH:mm"))
                             .FontSize(10);

                        table.Cell()
                            .Text(transfer.Sender)
                            .FontSize(10);

                        table.Cell()
                            .Text(transfer.Receiver)
                            .FontSize(10);

                        table.Cell()
                            .Text(transfer.Amount.ToString())
                            .FontSize(10);

                        table.Cell()
                            .Padding(2)
                            .Text(transfer.Title ?? "")
                            .FontSize(10);
                    }
                });

                page.Footer().AlignRight().Text(text =>
                {
                    text.Span("Wygenerowano: ").SemiBold().FontSize(9);
                    text.Span(DateTime.Now.ToString("g")).FontSize(9);
                });
            });
        
        }
    }
}
