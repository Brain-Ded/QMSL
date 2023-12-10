namespace QMSL.Services
{
    //Vlad
    public static class AuthVerifier
    {
        public static bool NameVerification(string Name)
        {
            return CheckForSpecialSymbolsE(Name);
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
                && CheckForSpecialSymbolsP(Name) && CheckForSpecialSymbolsE(Surname));
        }

        private static bool CheckForSpecialSymbolsE(string Email) 
        {
            return Email.Contains("@") && !Email.Any(x => (x.Equals("\\") 
            || x.Equals(",") || x.Equals("?") || x.Equals(" ")));
        }
        private static bool CheckForSpecialSymbolsP(string Password) 
        {
            return !Password.Any(x => (x.Equals("\\") || x.Equals(",") 
            || x.Equals("?") || x.Equals("@") || x.Equals(" ")));
        }
    }
}
