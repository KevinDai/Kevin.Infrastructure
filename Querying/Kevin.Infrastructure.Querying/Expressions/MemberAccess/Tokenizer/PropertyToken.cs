﻿namespace Kevin.Infrastructure.Querying.Expressions
{
    internal class PropertyToken : IMemberAccessToken
    {
        private readonly string propertyName;

        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }

        public PropertyToken(string propertyName)
        {
            this.propertyName = propertyName;
        }
    }
}
