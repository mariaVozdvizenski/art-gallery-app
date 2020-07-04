using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using ee.itcollege.mavozd.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using PublicApi.DTO.v1;
using Invoice = DAL.App.DTO.Invoice;

namespace BLL.App.Services
{
    public class InvoiceService : BaseEntityService<IAppUnitOfWork, IInvoiceRepository, IInvoiceServiceMapper, Invoice, DTO.Invoice>,
        IInvoiceService
    {
        public InvoiceService(IAppUnitOfWork uow) : base(uow, uow.Invoices, new InvoiceServiceMapper())
        {
        }

        public async Task<IEnumerable<DTO.Invoice>> GetInvoicesForCurrentOrderAsync(Guid orderId)
        {
            var allInvoices = await UOW.Invoices.GetAllAsync();
            var orderDALInvoices = allInvoices.Where(e => e.OrderId == orderId);
            return orderDALInvoices.Select(e => Mapper.Map(e));
        }

        public async Task CalculateInvoiceDataAsync(DTO.Invoice invoice)
        {
            var invoices = await UOW.Invoices.GetAllAsync();
            var count = invoices.Count();
            invoice.InvoiceDate = DateTime.Now;
            invoice.InvoiceNumber = count == 0 ? 0 : count + 1;
            invoice.InvoiceStatusCodeId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            invoice.InvoiceDetails ??= "No details.";
        }

        public async Task GenerateInvoiceAsync(Guid invoiceId, InvoiceCreate extraData)
        {
            var invoice = await Repository.FirstOrDefaultAsync(invoiceId);
            string fileName = $"../WebApp/ApiControllers/1.0/generated/{invoice.InvoiceNumber}.pdf";
            FileInfo file = new FileInfo(fileName);

            PdfDocument pdf = new PdfDocument(new PdfWriter(file));
            Document document = new Document(pdf);
            
            Paragraph header = new Paragraph("Invoice")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(header);
            
            Paragraph subheader = new Paragraph($"nr. {invoice.InvoiceNumber}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(15);
            document.Add(subheader);
            
            document.Add(new LineSeparator(new SolidLine()));
            
            document.Add(new Paragraph(new Text("\n")));

            var fromToTable = CreateFromToTable(extraData);
            document.Add(fromToTable);

            document.Add(new Paragraph().Add(new Text("\n\n")));

            document.Add(new Paragraph($"Invoice date: {DateTime.Now}"));
            document.Add(new Paragraph($"Due date: {DateTime.Now.Add(TimeSpan.FromDays(14))}").SetBold());
            document.Add(new Paragraph("Payment terms: 14 days").SetBold());
            document.Add(new Paragraph($"Customers username: {invoice.Order!.AppUser!.UserName}"));

            document.Add(new Paragraph(new Text("\n\n")));
            document.Add(new LineSeparator(new SolidLine()));

            Table items = CreateItemsTable(invoice);
            document.Add(items);
            
            document.Add(new LineSeparator(new SolidLine()));
            document.Add(new Paragraph(new Text("\n")));
            var total = CalculateTotal(invoice);
            document.Add(new Paragraph($"Total: {total}€")
                .SetBold()
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(15));
            
            document.Close();
        }

        private static decimal CalculateTotal(Invoice invoice)
        {
            return invoice.Order!.OrderItems!.Sum(orderItem => orderItem.Painting!.Price * orderItem.Quantity);
        }
        
        private static Table CreateFromToTable(InvoiceCreate extraData) {
            
            Table table = new Table(2, true);
            
            Cell cell11 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("From"));
            Cell cell12 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("To"));
            
            table.AddHeaderCell(cell11);
            table.AddHeaderCell(cell12);

            var tableDataCells = new List<Cell>();
            
            Cell cell21 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("ArtGallery"));
            tableDataCells.Add(cell21);
            
            Cell cell22 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"{extraData.FirstName} {extraData.LastName}"));
            tableDataCells.Add(cell22);

            Cell cell31 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("+372 4784653"));
            tableDataCells.Add(cell31);

            Cell cell32 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"{extraData.TelephoneNumber}"));
            tableDataCells.Add(cell32);
            
            Cell cell41 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Estonia, Tallinn, Fake Address 12"));
            tableDataCells.Add(cell41);

            Cell cell42 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph($"{extraData.Country}, {extraData.City}, {extraData.Address}"));
            tableDataCells.Add(cell42);

            foreach (var cell in tableDataCells)
            {
                table.AddCell(cell);
            }
            return table;
        }

        private static Table CreateItemsTable(Invoice invoice)
        {
            Table items = new Table(3, true);
            
            Cell itemCell11 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Item").SetBold());
            
            Cell itemCell12 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Unit Price").SetBold());
            
            Cell itemCell13 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Quantity").SetBold());

            items.AddHeaderCell(itemCell11);
            items.AddHeaderCell(itemCell12);
            items.AddHeaderCell(itemCell13);

            foreach (var orderItem in invoice.Order!.OrderItems!)
            {
                items.AddCell(new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(Border.NO_BORDER)
                    .Add(new Paragraph($"{orderItem.Painting!.Title}")));
                
                items.AddCell(new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(Border.NO_BORDER)
                    .Add(new Paragraph($"{orderItem.Painting!.Price}€")));
                
                items.AddCell(new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(Border.NO_BORDER)
                    .Add(new Paragraph($"{orderItem.Quantity}")));
            }
            return items;
        }
    }
}