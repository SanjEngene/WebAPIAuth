using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace WebAPIAuth.ClientModels
{
    public class MangaArtistPostModel
    {
        [Required]
        [StringLength(45)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(45)]
        public string LastName { get; set; }
        [Required]
        public StringDate BirthdayDate { get; set; }
    }
    public class StringDate
    {
        private string _dateString;
        public StringDate(string dateString)
        {
            if (isValid(dateString))
                _dateString = dateString;
            else
                _dateString = "00.00.0000";
        }
        private static bool isValid(string dateString)
        {
            if (dateString == null)
                return false;
            Regex regex = new Regex(@"([0-2][0-9]|3[0-1])\.(0[0-9]|1[0-2])\.[1-9][0-9]*");
            return regex.IsMatch(dateString);
        }

        public static explicit operator DateTime(StringDate obj)
        {
            if (obj == null)
                throw new NullReferenceException();

            if (obj._dateString == "00.00.0000")
                return new DateTime(0, 0, 0);

            int[] date = obj._dateString.Split(".").Select(i => int.Parse(i)).ToArray();
            return new DateTime(date[2], date[1], date[0]);
        }
    }
}
