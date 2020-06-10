using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;

namespace ShamanHelper
{
    class ShamanEngine
    {
        private Thread preparationThread;
        private bool isPreparationInProgress;
        private List<Spell> profileSpells;
        private NwnApi nwnApi;
        private KnownSpells knownSpells;

        private static ShamanEngine instance;

        private ShamanEngine()
        {
            profileSpells = new List<Spell>(0);
            nwnApi = new NwnApi();
        }

        public static ShamanEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShamanEngine();
                }
                return instance;
            }
        }

        public void LoadProfile(string profileName)
        {
            profileSpells.Clear();

            knownSpells = KnownSpells.Instance;
            knownSpells.Load();

            XmlDocument xml = new XmlDocument();
            xml.Load(Path.Combine(Environment.CurrentDirectory, Preferences.PROFILES_DIR, profileName + ".xml"));

            XmlNodeList xnList = xml.SelectNodes("/Spells/Spell");
            foreach (XmlNode xn in xnList)
            {
                try
                {
                    string spellName = xn.Attributes["spellname"].Value;
                    int preparedSpellsLimit = Convert.ToInt32(xn.Attributes["preparedSpellsLimit"].Value);
                    if (string.IsNullOrEmpty(spellName))
                    {
                        throw new Exception("Invalid spell name: " + spellName);
                    }
                    if (preparedSpellsLimit > 0 && knownSpells.IsKnownSpell(spellName))
                    {
                        Spell spell = new Spell(spellName, knownSpells.GetShamanicMessage(spellName), preparedSpellsLimit);
                        Console.WriteLine("Profile spell loaded: " + spell.ToString());
                        profileSpells.Add(spell);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Spell not loaded: " + e.Message);
                }
            }
        }

        public void ResetSpellsBeforeRitual()
        {
            foreach (Spell profileSpell in profileSpells)
            {
                // Shaman stone prints only spells with available preparations in ritual mode. So we need to reset
                // all of them to prepared condition.
                profileSpell.PreparedSpellsCount = profileSpell.PreparedSpellsLimit;
                profileSpell.AvailablePreparationsCount = 0;
            }
        }

        public void UpdateSpell(Spell spellUpdate)
        {
            if (string.IsNullOrEmpty(spellUpdate.SpellName))
            {
                return;
            }
            foreach (Spell profileSpell in profileSpells)
            {
                if (spellUpdate.SpellName.Equals(profileSpell.SpellName))
                {
                    profileSpell.PreparedSpellsCount = spellUpdate.PreparedSpellsCount;
                    int availablePreparations = profileSpell.PreparedSpellsLimit - spellUpdate.PreparedSpellsCount;
                    profileSpell.AvailablePreparationsCount = availablePreparations > 0 ? availablePreparations : 0;
                    Console.WriteLine("Spell updated: "+ profileSpell.ToString());
                }
            }
        }

        public void StartSpellPreparation()
        {
            if (PreparationInProgress)
            {
                return;
            }
            PreparationInProgress = true;
            preparationThread = new Thread(new ThreadStart(PrepareProfileSpells));
            preparationThread.Start();
            while (!preparationThread.IsAlive) ; // Wait until thread start
            Console.WriteLine("Spells preparation started");
        }

        public void StopSpellPreparation()
        {
            if (preparationThread == null)
            {
                PreparationInProgress = false;
                return;
            }
            preparationThread.Abort();
            preparationThread.Join();
            PreparationInProgress = false;
            preparationThread = null;
            Console.WriteLine("Spells preparation interrupted");
            nwnApi.WhisperToChat("Shamanic ritual was interrupted");
        }

        private bool HasSpellsToPrepare()
        {
            if (Spells.Count == 0)
            {
                return false;
            }
            int availablePreparationsTotal = 0;
            foreach (Spell spell in Spells)
            {
                availablePreparationsTotal += spell.AvailablePreparationsCount;
            }
            return (availablePreparationsTotal > 0);
        }

        private void PrepareProfileSpells()
        {
            Thread.Sleep(2000);
            if (!HasSpellsToPrepare())
            {
                nwnApi.WhisperToChat("No spells to prepare...");
                return;
            }
            nwnApi.WhisperToChat("Shamanic ritual will begins within 6 seconds...");
            Thread.Sleep(6000);
            Spell lastSpell = Spells.Last();
            foreach (Spell spell in Spells)
            {
                if (spell.AvailablePreparationsCount > 0)
                {
                    PrepareSpell(spell);
                    if (!spell.Equals(lastSpell))
                    {
                        Thread.Sleep(6000);
                    }
                }
            }
            nwnApi.WhisperToChat("Shamanic ritual completed");
        }

        private void PrepareSpell(Spell spell)
        {
            int count = spell.AvailablePreparationsCount;
            for (int i = 0; i < count; i++)
            {
                nwnApi.WhisperToChat(spell.Message);
                Console.WriteLine("Preparing: " + spell.SpellName);
                if (count > 1 && i != (count - 1))
                {
                    Thread.Sleep(6000);
                }
            }
        }

        private bool HasSpell(Spell spell)
        {
            return HasSpell(spell.SpellName);
        }

        private bool HasSpell(string spellName)
        {
            if (String.IsNullOrEmpty(spellName))
            {
                return false;
            }
            bool found = false;
            foreach (Spell s in profileSpells)
            {
                if (spellName.Equals(s.SpellName))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        private bool PreparationInProgress
        {
            get { return isPreparationInProgress; }
            set { isPreparationInProgress = value; }
        }

        public IReadOnlyCollection<Spell> Spells
        {
            get { return profileSpells.AsReadOnly(); }
        }
    }
}
