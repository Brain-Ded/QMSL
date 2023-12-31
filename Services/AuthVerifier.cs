﻿namespace QMSL.Services
{
    //Vlad
    public static class AuthVerifier
    {
        public static bool TextVerification(string Name)
        {
            char[] symbolsToCheck = { '@', '#', '%', '$', '\\', '/' };

            foreach (char character in Name)
            {
                if (Array.IndexOf(symbolsToCheck, character) != -1)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool LoginVerification(string Email, string Password)
        {
            return (CheckForSpecialSymbolsE(Email) && CheckForSpecialSymbolsP(Password));
        }
        public static bool RegisterVerification(string Email, string Password, int Age, string Sex, string PhoneNumber, 
            string Name, string Surname, string? FatherName) 
        {
            if(FatherName != null && !FatherName.Equals(""))
            return (CheckForSpecialSymbolsE(Email) && CheckForSpecialSymbolsP(Password) 
                && (Sex.Equals("Male") || Sex.Equals("Female")) && PhoneNumber.StartsWith("+")
                && CheckForSpecialSymbolsP(Name) && CheckForSpecialSymbolsP(Surname) && CheckForSpecialSymbolsP(FatherName) && Age >= 0);
        else 
                return (CheckForSpecialSymbolsE(Email) && CheckForSpecialSymbolsP(Password)
                && (Sex.Equals("Male") || Sex.Equals("Female")) && PhoneNumber.StartsWith("+")
                && CheckForSpecialSymbolsP(Name) && CheckForSpecialSymbolsP(Surname));
        }

        private static bool CheckForSpecialSymbolsE(string Email) 
        {
            char[] symbolsToCheck = {'#', '%', '$', '\\', '/' };

            foreach (char character in Email)
            {
                if (Array.IndexOf(symbolsToCheck, character) != -1)
                {
                    return false;
                }
            }

            return Email.Contains("@");
        }
        private static bool CheckForSpecialSymbolsP(string Password) 
        {
            char[] symbolsToCheck = { '@', '#', '%', '$', '\\', '/', ' ' };

            foreach (char character in Password)
            {
                if (Array.IndexOf(symbolsToCheck, character) != -1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
