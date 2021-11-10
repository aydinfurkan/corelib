using System;
using System.Linq;
using FluentValidation;

namespace CoreLib.Validations
{
    public static class ValidationHelper
    {
        public static void ThrowException<TValidator, TField, TException>(TValidator validator, ValidationContext<TValidator> context, TField arg3) where TException : Exception
        {
            var ex = (TException) Activator.CreateInstance(typeof(TException), ToCamelCase(context.PropertyName));
            if (ex != null) throw ex;
            throw new Exception();
        }
        private static string ToCamelCase(string str) {
            return string.Join('.',
                str.Split('.').Select(x => x.Substring(0, 1).ToLower() + x.Substring(1)));
        }
    }
}