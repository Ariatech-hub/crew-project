using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Business.DTOs;
using Microsoft.EntityFrameworkCore;


namespace FarmToFork.Business.Utilities
{
    public static class ValidationUtility
    {

        public static bool IsModelExist<T>(T arg)
        {
            string name = arg == null ? nameof(arg) : arg.GetType().Name;

            if (arg is null)
            {
                throw new Exception($"Invalid {name}");
            }

            return true;
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length > 10 ||
                !phoneNumber.All(c => c is >= '0' and <= '9'))
            {

                throw new Exception("Invalid Phone Number");
            }

            return true;
        }

       
    }
}
