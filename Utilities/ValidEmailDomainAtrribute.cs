using System.ComponentModel.DataAnnotations;

namespace Top_lista_vremena.Utilities
{
    public class ValidEmailDomainAtrribute :ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidEmailDomainAtrribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }

        public override bool IsValid(object value)
        {
            string[] tempDomain = value.ToString().Split('@');
            return tempDomain[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}
