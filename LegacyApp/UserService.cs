using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsFirstNameCorrect(firstName) || IsSecondNameCorrect(lastName))
            {
                return false;
            }
            
            if (IsEmailCorrect(email))
            {
                return false;
            }
    
            if (GetCurrentAge(dateOfBirth) < 21)
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                user.CreditLimit = CreditLimitByImportance(user,2);
            }
            else
            {
                user.HasCreditLimit = true;
                user.CreditLimit = CreditLimitByImportance(user,1);;
                
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private static int CreditLimitByImportance(User user, int multiplied)
        {
            using var userCreditService = new UserCreditService();
            int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            creditLimit = creditLimit * multiplied;
            return creditLimit;
        }

        private bool IsFirstNameCorrect(string firstName)
        {
            return string.IsNullOrEmpty(firstName);
        }

        private bool IsSecondNameCorrect(string lastName)
        {
            return string.IsNullOrEmpty(lastName);
        }
        private bool IsEmailCorrect(string email)
        {
            return !email.Contains("@") && !email.Contains(".");
        }

        private int GetCurrentAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }
    }
}
