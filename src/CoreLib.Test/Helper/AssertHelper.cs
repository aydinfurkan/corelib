using System;
using FluentValidation;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CoreLib.Test.Helper
{
    public static class AssertHelper
    {
        public static void JsonEqual(object expected, object actual)
        {
            Assert.AreEqual(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
        }

        public static void Validate<T>(AbstractValidator<T> validator, T model, Type expectedException)
        {
            if (expectedException != null)
            {
                Assert.Throws(expectedException, () => validator.Validate(model));
            }
            else
            {
                var result = validator.Validate(model);
                Assert.True(result.IsValid);
            }
        }
    }
}