using BalcarNew.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Helper
{

    //public static int GetUserCredit(int IDCustomer)
    //{

    //    BalcarDBContext Context = new BalcarDBContext();

    //    var UserCredit = Context.BizCustomerCredits.Where(x => x.IDCustomer == IDCustomer).FirstOrDefault();

    //    if (UserCredit == null)
    //        return 0;
    //    else
    //        return (int)UserCredit.Credits;
    //}

    public static int ConvertToInt(string Value)
    {
        int res;
        bool IsOk = Int32.TryParse(Value, out res);
        if (!IsOk)
            return 0;

        else

            return res;
    }

    public static double ConvertToDoble(string Value)
    {
        double res;
        bool IsOk = double.TryParse(Value, out res);
        if (!IsOk)
            return 0;

        else

            return res;
    }

    public static string GeneratePassword(int PasswordLength)
    {

        string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
        Random randNum = new Random();
        char[] chars = new char[PasswordLength];
        int allowedCharCount = _allowedChars.Length;
        for (int i = 0; i < PasswordLength; i++)
        {
            chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        }
        return new string(chars);

    }


    public static float GetCurrentVat()
    {

        float Vat = BalcarNew.Program.configuration.GetValue<float>("Vat");
        return Vat;

    }


    public static string GetCreditGuardData(string Param)
    {

        string res = BalcarNew.Program.configuration.GetValue<string>("CreditGuard:" + Param);
        return res;

    }

    public static object GetSingleConfiguration(string Param)
    {

        object res = BalcarNew.Program.configuration.GetValue<object>(Param);
       

        return res;

    }


    public static string EnglishCarName(string hebTozertName)
    {

        var En_tozeret = "";
        if (hebTozertName.Contains("אלפא"))
            En_tozeret = "Alfa-Romeo";
        else if (hebTozertName.Contains("אוסטין"))
            En_tozeret = "Aston-Martin";
        else if (hebTozertName.Contains("אודי"))
            En_tozeret = "Audi";
        else if (hebTozertName.Contains("אאודי"))
            En_tozeret = "Audi";
        else if (hebTozertName.Contains("בנטלי"))
            En_tozeret = "Bentley";
        else if (hebTozertName.Contains("ב.מ.ו"))
            En_tozeret = "BMW";
        else if (hebTozertName.Contains("ב מ ו"))
            En_tozeret = "BMW";
        else if (hebTozertName.Contains("בוגטי"))
            En_tozeret = "Bugatti";
        else if (hebTozertName.Contains("ביואיק"))
            En_tozeret = "Buick";
        else if (hebTozertName.Contains("קאדילאק"))
            En_tozeret = "Cadillac";
        else if (hebTozertName.Contains("שברולט"))
            En_tozeret = "Chevrolet";
        else if (hebTozertName.Contains("קרייזלר"))
            En_tozeret = "Chrysler";
        else if (hebTozertName.Contains("סיטרואן"))
            En_tozeret = "citroen";
        else if (hebTozertName.Contains("דאצ'יה"))
            En_tozeret = "Dacia";
        else if (hebTozertName.Contains("דייהטסו"))
            En_tozeret = "Daihatsu";
        else if (hebTozertName.Contains("דודג"))
            En_tozeret = "Dodge";
        else if (hebTozertName.Contains("פרארי"))
            En_tozeret = "Ferrari";
        else if (hebTozertName.Contains("פיאט"))
            En_tozeret = "Fiat";
        else if (hebTozertName.Contains("פורד"))
            En_tozeret = "Ford";
        else if (hebTozertName.Contains("GMC"))
            En_tozeret = "GMC";
        else if (hebTozertName.Contains("AMG"))
            En_tozeret = "AMG";
        else if (hebTozertName.Contains("הונדה"))
            En_tozeret = "Honda";
        else if (hebTozertName.Contains("האמר"))
            En_tozeret = "Hummer";
        else if (hebTozertName.Contains("יונדאי"))
            En_tozeret = "Hyundai";
        else if (hebTozertName.Contains("אינפיניטי"))
            En_tozeret = "Infiniti";
        else if (hebTozertName.Contains("איסוזו"))
            En_tozeret = "Isuzu";
        else if (hebTozertName.Contains("יגואר"))
            En_tozeret = "Jaguar";
        else if (hebTozertName.Contains("Jeep"))
            En_tozeret = "Jeep";
        else if (hebTozertName.Contains("ג'יפ"))
            En_tozeret = "Jeep";
        else if (hebTozertName.Contains("למבורגיני"))
            En_tozeret = "Lamborghini";
        else if (hebTozertName.Contains("לנצ'יה"))
            En_tozeret = "Lancia";
        else if (hebTozertName.Contains("לנד רובר"))
            En_tozeret = "Land-Rover";
        else if (hebTozertName.Contains("לקסוס"))
            En_tozeret = "Lexus";
        else if (hebTozertName.Contains("מזראטי"))
            En_tozeret = "Maserati";
        else if (hebTozertName.Contains("מזדה"))
            En_tozeret = "Mazda";
        else if (hebTozertName.Contains("מרצדס"))
            En_tozeret = "Mercedes";
        else if (hebTozertName.Contains("מיני"))
            En_tozeret = "Mini";
        else if (hebTozertName.Contains("מיצובישי"))
            En_tozeret = "Mitsubishi";
        else if (hebTozertName.Contains("ניסאן"))
            En_tozeret = "Nissan";
        else if (hebTozertName.Contains("אופל"))
            En_tozeret = "Opel";
        else if (hebTozertName.Contains("פיג'ו"))
            En_tozeret = "Peugeot";
        else if (hebTozertName.Contains("רנו"))
            En_tozeret = "Renault";
        else if (hebTozertName.Contains("סאאב"))
            En_tozeret = "SAAB";
        else if (hebTozertName.Contains("סיאט"))
            En_tozeret = "SEAT";
        else if (hebTozertName.Contains("סקודה"))
            En_tozeret = "Skoda";
        else if (hebTozertName.Contains("סמארט"))
            En_tozeret = "Smart";
        else if (hebTozertName.Contains("סאנגיונג"))
            En_tozeret = "SsangYong";
        else if (hebTozertName.Contains("סובארו"))
            En_tozeret = "Subaru";
        else if (hebTozertName.Contains("סוזוקי"))
            En_tozeret = "Suzuki";
        else if (hebTozertName.Contains("טסלה"))
            En_tozeret = "Tesla";
        else if (hebTozertName.Contains("טויוטה"))
            En_tozeret = "Toyota";
        else if (hebTozertName.Contains("פולקסווגן"))
            En_tozeret = "Volkswagen";
        else if (hebTozertName.Contains("גולף"))
            En_tozeret = "Golf";
        else if (hebTozertName.Contains("וולוו"))
            En_tozeret = "Volvo";
        else if (hebTozertName.Contains("פונטיאק"))
            En_tozeret = "Pontiac";
        else if (hebTozertName.Contains("רובר"))
            En_tozeret = "Rover";
        else if (hebTozertName.Contains("פורשה"))
            En_tozeret = "Porsche";
        else if (hebTozertName.Contains("לוטוס"))
            En_tozeret = "Lotus";
        else if (hebTozertName.Contains("דייהו"))
            En_tozeret = "Daewoo";
        else if (hebTozertName.Contains("דימלרקריזלר") || hebTozertName.Contains("דימלרקריזלר-אר"))
            En_tozeret = "daimler_chrysler";
        else if (hebTozertName.Contains("ימהה"))
            En_tozeret = "Yamaha";
        else if (hebTozertName.Contains("הארלי ד"))
            En_tozeret = "HARLEY DAVIDSON";
        else if (hebTozertName.Contains("דוקאטי"))
            En_tozeret = "Ducati";
        else if (hebTozertName.Contains("קוואסקי"))
            En_tozeret = "Kawasaki";
        else if (hebTozertName.Contains("אבארט"))
            En_tozeret = "Abarth";
        else if (hebTozertName.Contains("די אס"))
            En_tozeret = "DS";
        else if (hebTozertName.Contains("לינקולן"))
            En_tozeret = "Lincoln";
        else if (hebTozertName.Contains("קיה"))
            En_tozeret = "Kia";
        return En_tozeret;
    }



}




