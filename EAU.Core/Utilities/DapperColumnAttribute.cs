using System;

namespace EAU.Utilities
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DapperColumnAttribute : Attribute
    {
        private string _name;

        public DapperColumnAttribute(string name)
        {
            this._name = name;
        }

        public string Name { get { return _name; } }
    }
}
