using System;
using System.Collections.Generic;

namespace PG1_uppgift
{
    class Vault
    {
        private Dictionary<string, string> vault { get; set; }
        
        public Vault()
        {
            vault = new Dictionary<string, string>();
        }
        //Sets the property password if it is already in the vault, otherwise adds the property and password.
        public void setValue(string property, string pwd)
        {
            if(vault.ContainsKey(property))
            {    
                vault[property] = pwd;
            }
            else
            {
                vault.Add(property, pwd);
            }
        }
        public void GetSpecificValue(string property)
        {
            if(vault.ContainsKey(property))
            {
                                Console.WriteLine(property + ": " + vault[property]);
            }
            else
            {
                Console.WriteLine("Property doesn't exist");
            }

        }
        public void GetAllValues()
        {
            foreach(var x in vault)
            {
                Console.WriteLine(x.Key + ": " + x.Value);
            }
        }
        public void Drop(string property)
        {
            vault.Remove(property);
        }
        public Dictionary<string, string> getVault()
        {
            return vault;
        }
    }
}
