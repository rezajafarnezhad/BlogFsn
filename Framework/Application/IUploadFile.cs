using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace Framework.Application
{
    public interface IUploadFile
    {
        string Upload(IFormFile file);
        void DeleteFile(string file);
    }


    public static class ConvertDate 
    {
        public static string ToFarsiFull(this DateTime date)
        {
            var pc = new PersianCalendar();
            return
                $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00} {date.Hour:00}:{date.Minute:00}:{date.Second:00}";
        }
    }
   
}