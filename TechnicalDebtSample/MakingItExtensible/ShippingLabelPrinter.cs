﻿using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace TechnicalDebtSample.MakingItExtensible
{
    class ShippingLabelPrinter1
    {
        private readonly PrinterConfig printerConfig;

        public ShippingLabelPrinter1()
        {
            printerConfig = PrinterConfig.Instance;
        }

        public void PrintLabel(Address address)
        {
            var printer = new Printer(printerConfig.Port);
            printer.Font = "Times New Roman";
            printer.FontSize = 24;
            printer.LineSpacing = 6;

            string town = address.Town;
            bool printDistrict = true;
            if (address.CountryCode == "CH")
            {
                printer.Font = "Kai Bold";
                printer.FontSize = 18;
                printer.LineSpacing = 8;
            }
            if (address.CountryCode == "IT")
            {
                town = town.ToUpper();
            }
            if (address.CountryCode == "IR")
            {
                printer.RightToLeft = true;
                printer.FontSize = 15;
                if (address.District == address.Town)
                {
                    printDistrict = false;
                }
            }
            printer.PrintLine(address.Name + ",");
            printer.PrintLine(address.AddressLine1 + ",");
            if (!String.IsNullOrEmpty(address.AddressLine2))
                printer.PrintLine(address.AddressLine2 + ",");
            printer.PrintLine(town + ",");
            if (printDistrict)
                printer.PrintLine(address.District.ToUpper());
            printer.PrintLine(address.PostalCode);            
            printer.PrintLine(address.Country.ToUpper());            
        }
    }


    [TestFixture]
    public class ShippingLabelPrinterTests
    {
        [Test]
        public void PrintsCorrectLabel()
        {
            var labelPrinter = new ShippingLabelPrinter();
            
        }
    }


    internal class PrinterConfig
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

    internal class Printer
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
        public bool RightToLeft { get; set; }
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
        public string CountryCode { get; set; }
    }
}
