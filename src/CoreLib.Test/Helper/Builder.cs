using System;
using System.Linq;
using System.Linq.Expressions;

namespace CoreLib.Test.Helper
{
    public abstract class Builder<T> where T : class
    {
        private T _item;

        protected Builder()
        {
            _item = CreateItem();
        }

        /// <summary>
        /// Override/specify a value for a property of the object to be built.
        /// </summary>
        /// <param name="expression">An expression that specifies the property to modify. Must be of the form (x => x.SomeProperty)</param>
        /// <param name="value">The value to assign to the property</param>
        /// <typeparam name="TValue"></typeparam>
        /// <returns>The builder instance</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Builder<T> With<TValue>(Expression<Func<T, TValue>> expression, TValue value)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
            {
                throw new InvalidOperationException("Improperly formatted expression");
            }

            var propertyName = body.Member.Name;
            var property = typeof(T).GetProperties().FirstOrDefault(p => p.Name == propertyName);
            property?.SetValue(_item, value);
            return this;
        }

        public T Build()
        {
            if (_item == null)
                _item = CreateItem();
            return _item;
        }

        public void Reset()
        {
            _item = CreateItem();
        }

        public abstract T CreateItem();
    }
}