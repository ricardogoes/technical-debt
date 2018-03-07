using System;
using System.Collections.Generic;

namespace TechnicalDebtSample.MakingItExtensible
{
    class AddressLabelFormat
    {
        public string Font { get; set; }
        public int FontSize { get; set; }
        public bool RightToLeft { get; set; }
        public string[] Lines { get; set; }
        public int LineSpacing { get; set; }
    }

    internal interface IAddressLabelFormatter
    {
        AddressLabelFormat GetAddressFormat(Address address);
    }


    /// <summary>
    /// Stage 1 - extract static method
    /// </summary>
    internal class ShippingLabelPrinter2
    {
        private readonly PrinterConfig printerConfig;

        public ShippingLabelPrinter2()
        {
            printerConfig = PrinterConfig.Instance;
        }

        public void PrintLabel(Address address)
        {
            var printer = new Printer(printerConfig.Port);
            var format = GetAddressFormat(address);
            printer.Font = format.Font;
            printer.FontSize = format.FontSize;
            printer.LineSpacing = format.LineSpacing;
            printer.RightToLeft = format.RightToLeft;
            foreach (var line in format.Lines)
            {
                printer.PrintLine(line);
            }
        }

        public static AddressLabelFormat GetAddressFormat(Address address)
        {
            var format = new AddressLabelFormat();
            var lines = new List<string>();
            format.Font = "Times New Roman";
            format.FontSize = 24;
            format.LineSpacing = 6;

            string town = address.Town;
            bool printDistrict = true;
            if (address.CountryCode == "CH")
            {
                format.Font = "Kai Bold";
                format.FontSize = 18;
                format.LineSpacing = 8;
            }
            if (address.CountryCode == "IT")
            {
                town = town.ToUpper();
            }
            if (address.CountryCode == "IR")
            {
                format.RightToLeft = true;
                format.FontSize = 15;
                printDistrict = address.District != address.Town;
            }

            lines.Add(address.Name + ",");
            lines.Add(address.AddressLine1 + ",");
            if (!String.IsNullOrEmpty(address.AddressLine2))
                lines.Add(address.AddressLine2 + ",");
            lines.Add(town + ",");
            if (printDistrict)
                lines.Add(address.District.ToUpper());
            lines.Add(address.PostalCode);
            lines.Add(address.Country.ToUpper());
            format.Lines = lines.ToArray();
            return format;
        }

    }

    class ShippingLabelPrinter
    {
        private readonly IAddressLabelFormatter formatter;
        private readonly PrinterConfig printerConfig;

        public ShippingLabelPrinter(IAddressLabelFormatter formatter)
        {
            this.formatter = formatter;
            printerConfig = PrinterConfig.Instance;
        }

        public void PrintLabel(Address address)
        {
            var printer = new Printer(printerConfig.Port);
            var format = formatter.GetAddressFormat(address);
            printer.Font = format.Font;
            printer.FontSize = format.FontSize;
            printer.LineSpacing = format.LineSpacing;
            printer.RightToLeft = format.RightToLeft;
            foreach (var line in format.Lines)
            {
                printer.PrintLine(line);
            }
        }
    }

    class AddressLabelFormatter : IAddressLabelFormatter
    {
        readonly Dictionary<string, Func<Address, AddressLabelFormat>> formatters
            = new Dictionary<string, Func<Address, AddressLabelFormat>>()
            {
                { "CH", ChinaFormatter },
                { "IT", ItalyFormatter },
                { "SP", SpainFormatter },
            };


        public AddressLabelFormat GetAddressFormat(Address address)
        {
            var formatter = GetFormatterForCountry(address.CountryCode);
            return formatter(address);            
        }

        // ... 

        private Func<Address, AddressLabelFormat> GetFormatterForCountry(string countryCode)
        {
            return formatters[countryCode];
        }
        private static AddressLabelFormat ChinaFormatter(Address arg)
        {
            throw new NotImplementedException();
        }

        private static AddressLabelFormat SpainFormatter(Address arg)
        {
            throw new NotImplementedException();
        }

        private static AddressLabelFormat ItalyFormatter(Address arg)
        {
            throw new NotImplementedException();
        }


        private AddressLabelFormat CreateDefaultFormat()
        {
            throw new NotImplementedException();
        }
    }

}
