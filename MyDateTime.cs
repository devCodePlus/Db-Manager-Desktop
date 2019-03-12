
using System;


public static class MyDateTime
{
    //private static string curentDateTime = MyDateTime.getPersianDate().Replace("/", "") + "-" + MyDateTime.getTime(DateTime.Now).Replace(":", "");



    //get TimeStamp completely
    public static string getTimeStamp(DateTime myValue)
    {
        string value = myValue.ToString("yyyy/MM/dd-HH:mm:ss-ffff");

        return value;
    }


    //get only date
    public static string getDate(DateTime myValue)
    {
        string value = myValue.ToString("yyyy/MM/dd");

        return value;
    }



    //get Time only
    public static string getTime(DateTime myValue)
    {
        string value = myValue.ToString("HH:mm:ss");

        return value;
    }


    // get Name of day in persian
    public static string getPersianDayInWeek()
    {
        string Resme = "";
        string Res;

        System.Globalization.PersianCalendar DateFme = new System.Globalization.PersianCalendar();
        Res = DateFme.GetDayOfWeek(DateTime.Now).ToString();

        switch (Res)
        {
            case "Saturday":
                {
                    Resme = "شنبه";
                    break;
                }
            case "Sunday":
                {
                    Resme = "یکشنبه";
                    break;
                }
            case "Monday":
                {
                    Resme = "دوشنبه";
                    break;
                }
            case "Tuesday":
                {
                    Resme = "سه شنبه";
                    break;
                }
            case "Wednesday":
                {
                    Resme = "چهار‌شنبه";
                    break;
                }
            case "Thursday":
                {
                    Resme = "پنجشنبه";
                    break;
                }
            case "Friday":
                {
                    Resme = "جمعه";
                    break;
                }
        }
        return Resme;
    }



    //get Date in Persian
    public static string getPersianDate()
    {
        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

        int year = pc.GetYear(DateTime.Now);
        int month = pc.GetMonth(DateTime.Now);
        int day = pc.GetDayOfMonth(DateTime.Now);

        string persianDate = year.ToString("0#") + "/" + month.ToString("0#") + "/" + day.ToString("0#");

        return persianDate;
    }



}

