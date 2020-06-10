using System;

namespace ShamanHelper
{
    class Spell
    {
        private string spellName;
        private string message;
        private int preparedSpellsLimit;
        private int preparedSpellsCount;
        private int availablePreparationsCount;

        public Spell(string spellName, string message = null, int preparedSpellsLimit = Preferences.MAX_SHAMANIZED_SPELLS_COUNT)
        {
            this.spellName = spellName;
            this.message = message == null ? "Shamanic message unknown (" + spellName + ")" : message;
            this.preparedSpellsLimit = preparedSpellsLimit;
            this.preparedSpellsCount = 0;
            this.availablePreparationsCount = this.preparedSpellsLimit;
        }

        public string SpellName
        {
            get { return spellName; }
            set { spellName = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public int PreparedSpellsLimit
        {
            get { return preparedSpellsLimit; }
            set { preparedSpellsLimit = value; }
        }

        public int PreparedSpellsCount
        {
            get { return preparedSpellsCount; }
            set { preparedSpellsCount = value; }
        }

        public int AvailablePreparationsCount
        {
            get { return availablePreparationsCount; }
            set { availablePreparationsCount = value; }
        }

        public override string ToString()
        {
            return String.Format("[ SpellName={0}, PreparedSpellsLimit={1}, PreparedSpellsCount={2}, AvailablePreparationsCount={3} ]",
                SpellName, PreparedSpellsLimit, PreparedSpellsCount, AvailablePreparationsCount);
        }
    }
}
