using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShamanHelper
{
    class KnownSpells
    {
        private static KnownSpells instance;

        private Hashtable knownSpells;
        private bool isLoaded;

        private KnownSpells() {
            knownSpells = new Hashtable(0);
            IsLoaded = false;
        }

        public static KnownSpells Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KnownSpells();
                }
                return instance;
            }
        }

        public bool Load()
        {
            string path = Path.Combine(Environment.CurrentDirectory, Preferences.KNOWN_SPELLS_PATH);

            if (!File.Exists(path))
            {
                Console.WriteLine("Error load known spells db!");
                return false;
            }

            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(path);
                XmlNodeList xnList = xml.SelectNodes("/KnownSpells/SpellDescription");
                foreach (XmlNode xn in xnList)
                {
                    string spellName = xn.Attributes["spellname"].Value;
                    string message = xn.Attributes["message"].Value;

                    if (!string.IsNullOrEmpty(spellName) && !string.IsNullOrEmpty(message))
                    {
                        knownSpells.Add(spellName, message);
                    }
                }
                return true;
            }
            catch
            {
            }
            return false;            
        }

        public bool IsKnownSpell(string spellName)
        {
            return knownSpells.ContainsKey(spellName);
        }

        public string GetShamanicMessage(string spellName)
        {
            return knownSpells[spellName].ToString();
        }

        public Hashtable Spells
        {
            get { return knownSpells; }
        }

        public bool IsLoaded
        {
            get { return isLoaded; }
            set { isLoaded = value; }
        }
    }
}
