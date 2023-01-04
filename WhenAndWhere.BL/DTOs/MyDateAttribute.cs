using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WhenAndWhere.BL.DTOs
{
    public class MyDateAttribute : ValidationAttribute
    {
        public string EndDatePropertyName { get; set; }
        public string StartDatePropertyName { get; set; }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo endDateProperty = validationContext.ObjectType.GetProperty(EndDatePropertyName);

            DateTime endDate = (DateTime)endDateProperty.GetValue(validationContext.ObjectInstance, null);


            PropertyInfo startDateProperty = validationContext.ObjectType.GetProperty(StartDatePropertyName);

            DateTime startDate = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance, null);


            // Do comparison
            // return ValidationResult.Success; // if success
            if (startDate <= endDate)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Second date must be after first date."); // if fail
        }
    }
}
