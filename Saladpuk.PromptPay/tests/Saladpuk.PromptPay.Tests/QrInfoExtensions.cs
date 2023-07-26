﻿using FluentAssertions;
using Saladpuk.EMVCo.Contracts;
using Saladpuk.EMVCo.Models;
using Saladpuk.PromptPay.Contracts.Models;
using Saladpuk.PromptPay.Models;
using System.Linq;
using emv = Saladpuk.EMVCo.Contracts.EMVCoCodeConventions;

namespace Saladpuk.PromptPay.Tests
{
    public static class QrInfoExtensions
    {
        public static PromptPayQrInfo InitializeDefault(this PromptPayQrInfo qr, bool staticQr = true, CurrencyCode currency = CurrencyCode.THB, string country = "TH")
        {
            var PointOfInitiationMethod = staticQr ? emv.Static : emv.Dynamic;
            qr.Segments.Add(new QrDataObject("000201"));
            qr.Segments.Add(new QrDataObject($"0102{PointOfInitiationMethod}"));
            qr.Segments.Add(new QrDataObject($"5303{((int)currency).ToString("000")}"));
            qr.Segments.Add(new QrDataObject($"5802{country}"));
            return qr;
        }

        public static IQrInfo SetPlainCreditTransfer(this PromptPayQrInfo qr)
        {
            qr.Segments.Add(new QrDataObject("29200016A000000677010111"));
            qr.CreditTransfer = new CreditTransfer();
            return qr;
        }

        public static IQrInfo SetPlainBillPayment(this PromptPayQrInfo qr)
        {
            qr.Segments.Add(new QrDataObject("30200016A000000677010112"));
            qr.BillPayment = new BillPayment();
            return qr;
        }

        public static void ValidateWith(this IQrInfo qr, IQrInfo expected, bool skipChecksum = true)
        {
            if (skipChecksum)
            {
                qr.Segments.Remove(qr.Segments.FirstOrDefault(it => it.IdByConvention == QrIdentifier.CRC));
                expected.Segments.Remove(expected.Segments.FirstOrDefault(it => it.IdByConvention == QrIdentifier.CRC));
            }
            qr.Should().BeEquivalentTo(expected);
        }
    }
}
