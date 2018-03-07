using System;
using System.IO;
using System.Reflection.Emit;
using Moq;
using NUnit.Framework;

namespace TechnicalDebtSample.GettingItUnderTest
{
   /* class ShippingLabelPrinter
    {
        private readonly PrinterConfig printerConfig;

        public ShippingLabelPrinter()
        {
            printerConfig = PrinterConfig.Instance;
        }

        public void PrintLabel(Address address)
        {
            var printer = new Printer(printerConfig.Port);
            printer.Font = "Times New Roman";
            printer.FontSize = 24;
            printer.LineSpacing = 6;
            printer.PrintLine(address.Name + ",");
            printer.PrintLine(address.AddressLine1 + ",");
            if (!String.IsNullOrEmpty(address.AddressLine2))
                printer.PrintLine(address.AddressLine2 + ",");
            printer.PrintLine(address.Town + ",");
            printer.PrintLine(address.District.ToUpper());
            printer.PrintLine(address.PostalCode);            
            printer.PrintLine(address.Country.ToUpper());            
        }
    }*/

    class ShippingLabelPrinter
    {
        private readonly IPrinterConfig printerConfig;

        public ShippingLabelPrinter() : this(PrinterConfig.Instance)
        {
            
        }

        public ShippingLabelPrinter(IPrinterConfig printerConfig)
        {
            this.printerConfig = printerConfig;
        }

        public void PrintLabel(Address address)
        {
            var printer = CreatePrinter();
            printer.Font = "Times New Roman";
            printer.FontSize = 24;
            printer.LineSpacing = 6;
            printer.PrintLine(address.Name + ",");
            printer.PrintLine(address.AddressLine1 + ",");
            if (!String.IsNullOrEmpty(address.AddressLine2))
                printer.PrintLine(address.AddressLine2 + ",");
            printer.PrintLine(address.Town + ",");
            printer.PrintLine(address.District.ToUpper());
            printer.PrintLine(address.PostalCode);            
            printer.PrintLine(address.Country.ToUpper());            
        }

        protected virtual IPrinter CreatePrinter()
        {
            return new Printer(printerConfig.Port);
        }
    }


    [TestFixture]
    public class ShippingLabelPrinterTests
    {
        [Test]
        public void ShippingLabelPrinterTest()
        {
            var printerConfig = new Mock<IPrinterConfig>().Object;
            var labelPrinter = 
                new TestableShippingLabelPrinter(printerConfig);
            var address = CreateTestAddress();
            labelPrinter.PrintLabel(address);
        }

        private Address CreateTestAddress()
        {
            throw new NotImplementedException();
        }
    }

    class TestableShippingLabelPrinter 
        : ShippingLabelPrinter
    {
        protected override IPrinter CreatePrinter()
        {
            return new Mock<IPrinter>().Object;
        }

        // ...

        public TestableShippingLabelPrinter(IPrinterConfig printerConfig1)
        {
            throw new NotImplementedException();
        }


    }


    internal class PrinterConfig : IPrinterConfig
    {
        private static PrinterConfig instance;
        private PrinterConfig(string configPath)
        {
            File.OpenRead(configPath);
        }

        public static PrinterConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PrinterConfig("PrintOptions.xml");
                }
                return instance;
            }
        }

        public int Port { get; set; }
    }

    internal interface IPrinterConfig
    {
        int Port { get; set; }
    }

    internal interface IPrinter
    {
        void PrintLine(string line);
        string Font { get; set; }
        int FontSize { get; set; }
        int LineSpacing { get; set; }
    }

    internal class Printer : IPrinter
    {
        public Printer(int port)
        {
            
        }

        public void PrintLine(string line)
        {
        }

        public string Font { get; set; }
        public int FontSize { get; set; }
        public int LineSpacing { get; set; }
    }

    class Address
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
