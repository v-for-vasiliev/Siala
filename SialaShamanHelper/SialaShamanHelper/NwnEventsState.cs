namespace ShamanHelper
{
    class NwnEventsState
    {
        public const int RITUAL_STATE_ACTIVATED = 1;
        public const int RITUAL_STATE_NOT_ACTIVATED = 2;

        private int ritualState;
        private bool shamanStoneUsed;
        private bool shamanStoneUsedEventProcessed;

        private static NwnEventsState instance;

        private NwnEventsState()
        {
            RitualState = RITUAL_STATE_NOT_ACTIVATED;
            ShamanStoneUsed = false;
            shamanStoneUsedEventProcessed = false;
        }

        public static NwnEventsState Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NwnEventsState();
                }
                return instance;
            }
        }

        public int RitualState
        {
            get { return ritualState; }
            set { ritualState = value; }
        }

        public bool ShamanStoneUsed
        {
            get { return shamanStoneUsed; }
            set { shamanStoneUsed = value; }
        }

        public bool ShamanStoneUsedEventProcessed
        {
            get { return shamanStoneUsedEventProcessed; }
            set { shamanStoneUsedEventProcessed = value; }
        }
    }
}
