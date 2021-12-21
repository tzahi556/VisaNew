using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class FileDetected
{
    Dictionary<string, byte[]> imageHeader = new Dictionary<string, byte[]>();

    public FileDetected()
    {

        // DICTIONARY OF ALL IMAGE FILE HEADER

        imageHeader.Add("JPG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 });
        imageHeader.Add("JPEG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 });
        imageHeader.Add("PNG", new byte[] { 0x89, 0x50, 0x4E, 0x47 });
        imageHeader.Add("TIF", new byte[] { 0x49, 0x49, 0x2A, 0x00 });
        imageHeader.Add("TIFF", new byte[] { 0x49, 0x49, 0x2A, 0x00 });
        imageHeader.Add("GIF", new byte[] { 0x47, 0x49, 0x46, 0x38 });
        imageHeader.Add("BMP", new byte[] { 0x42, 0x4D });
        imageHeader.Add("ICO", new byte[] { 0x00, 0x00, 0x01, 0x00 });
        imageHeader.Add("PDF", new byte[] { 0x25, 0x50, 0x44, 0x46 });


    }


    public bool IsFileValid(IFormFile file)
    {
        byte[] header;

        // GET FILE EXTENSION
        string fileExt;
        fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1).ToUpper();
        // CUSTOM VALIDATION GOES HERE BASED ON FILE EXTENSION IF ANY      
        byte[] tmp = imageHeader[fileExt];
        header = new byte[tmp.Length];


        var ms = new MemoryStream();
        file.OpenReadStream().CopyTo(ms);
        System.Buffer.BlockCopy(ms.ToArray(), 0, header, 0, header.Length);


       // header = ms.ToArray();

        //using (var reader = new StreamReader(file.OpenReadStream()))
        //{
        //    string contentAsString = reader.ReadToEnd();
        //   // header = new byte[contentAsString.Length * sizeof(char)];
        //    System.Buffer.BlockCopy(contentAsString.ToArray(), 0, header, 0, header.Length);


        //}
        


        // GET HEADER INFORMATION OF UPLOADED FILE
      //  fuImage.FileContent.Read(header, 0, header.Length);
        if (CompareArray(tmp, header))
        {
           
            return true;
        }
        else
        {

            return false;
        }


    }

    private bool CompareArray(byte[] a1, byte[] a2)
    {
        if (a1.Length != a2.Length)
            return false;
        for (int i = 0; i < a1.Length; i++)
        {
            if (a1[i] != a2[i])
                return false;
        }
        return true;
    }


    //public static int ConvertToInt(string Value)
    //{
    //    int res;
    //    bool IsOk = Int32.TryParse(Value, out res);
    //    if (!IsOk)
    //        return 0;

    //    else

    //        return res;
    //}

    //public static double ConvertToDoble(string Value)
    //{
    //    double res;
    //    bool IsOk = double.TryParse(Value, out res);
    //    if (!IsOk)
    //        return 0;

    //    else

    //        return res;
    //}

    //public static string GeneratePassword(int PasswordLength)
    //{

    //    string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
    //    Random randNum = new Random();
    //    char[] chars = new char[PasswordLength];
    //    int allowedCharCount = _allowedChars.Length;
    //    for (int i = 0; i < PasswordLength; i++)
    //    {
    //        chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
    //    }
    //    return new string(chars);

    //}


    //public static float GetCurrentVat()
    //{

    //    float Vat = BalcarNew.Program.configuration.GetValue<float>("Vat");
    //    return Vat;

    //}


    //public static string GetCreditGuardData(string Param)
    //{

    //    string res = BalcarNew.Program.configuration.GetValue<string>("CreditGuard:" + Param);
    //    return res;

    //}






}




