﻿namespace Saladpuk.EMVCo.Contracts
{
    /// <summary>
    /// มาตรฐานในการอ่านข้อมูลจาก QR code
    /// </summary>
    public interface IQrReader
    {
        /// <summary>
        /// แปลความหมายของข้อความให้อยู่ในรูปแบบ QR EMVCo
        /// </summary>
        /// <param name="code">รหัส QR code ที่ต้องการอ่าน</param>
        /// <returns>รายละเอียดของ QR</returns>
        IQrInfo Read(string code);
    }
}
