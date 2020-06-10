using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace ShamanHelper
{
    class NwnEventsProcessor
    {
        // Ex.: [CHAT WINDOW TEXT] [Mon Jun 08 20:36:28] Bestow Curse: 2  свободно: 1
        private Regex SHAMAN_SPELL_EVENT_PATTERN = new Regex(@"\[CHAT WINDOW TEXT\]\s\[.+\]\s(.+[^:]):\s(\d)\s\sсвободно:\s(\d)\s\s\((.+)\)");
        // Ex.: [CHAT WINDOW TEXT] [Tue Jun 09 17:58:08] --------------------
        private Regex SHAMAN_STONE_USE_EVENT_PATTERN = new Regex(@"\[CHAT WINDOW TEXT\]\s\[.+\]\s--------------------");

        private ShamanEngine shamanEngine;
        private NwnEventsState eventsState;
        private NwnApi nwnApi;

        private static NwnEventsProcessor instance;

        private NwnEventsProcessor()
        {
            shamanEngine = ShamanEngine.Instance;
            shamanEngine.LoadProfile("vasiliev");
            eventsState = NwnEventsState.Instance;
            nwnApi = new NwnApi();
        }

        public static NwnEventsProcessor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NwnEventsProcessor();
                }
                return instance;
            }
        }

        public bool ProcessEvent(string nwnEvent)
        {
            return ProcessSpellUpdateEvent(nwnEvent) ||
                ProcessShamanStoneUseEvent(nwnEvent) ||
                ProcessRitualStartEvent(nwnEvent) ||
                ProcessRitualEndEvent(nwnEvent);
        }

        private bool ProcessSpellUpdateEvent(string nwnEvent)
        {
            if (!string.IsNullOrWhiteSpace(nwnEvent))
            {
                Match m = SHAMAN_SPELL_EVENT_PATTERN.Match(nwnEvent);
                if (m.Success && eventsState.RitualState == NwnEventsState.RITUAL_STATE_ACTIVATED)
                {
                    try
                    {
                        string spellName = m.Groups[1].Value;
                        int preparedSpellsCount = Convert.ToInt32(m.Groups[2].Value);
                        int availablePreparationsCount = Convert.ToInt32(m.Groups[3].Value);
                        string message = m.Groups[4].Value;
                        Spell spell = new Spell(spellName);
                        spell.PreparedSpellsCount = preparedSpellsCount;
                        spell.AvailablePreparationsCount = availablePreparationsCount;
                        shamanEngine.UpdateSpell(spell);
                        return true;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Spell update error: " + e.Message);
                    }

                }
            }
            return false;
        }

        private bool ProcessShamanStoneUseEvent(string nwnEvent)
        {
            if (!string.IsNullOrWhiteSpace(nwnEvent))
            {
                Match m = SHAMAN_STONE_USE_EVENT_PATTERN.Match(nwnEvent);
                if (m.Success)
                {
                    if (!eventsState.ShamanStoneUsedEventProcessed)
                    {
                        eventsState.ShamanStoneUsedEventProcessed = true;
                        new Thread(() => {
                            Thread.Sleep(1500);
                            eventsState.ShamanStoneUsedEventProcessed = false;
                        }).Start();
                        if (eventsState.RitualState == NwnEventsState.RITUAL_STATE_ACTIVATED)
                        {
                            // Shaman stone usage generates series of same events, so we just react on first of them.
                            if (!eventsState.ShamanStoneUsed)
                            {
                                eventsState.ShamanStoneUsed = true;
                                shamanEngine.StartSpellPreparation();
                            }
                            return true;
                        }
                        else
                        {
                            nwnApi.WhisperToChat("Activate shamanic ritual first!");
                        }
                    }
                }
            }
            return false;
        }

        private bool ProcessRitualStartEvent(string nwnEvent)
        {
            if (!string.IsNullOrWhiteSpace(nwnEvent))
            {
                if (nwnEvent.Contains("Вы готовитесь к ритуалу"))
                {
                    eventsState.RitualState = NwnEventsState.RITUAL_STATE_ACTIVATED;
                    eventsState.ShamanStoneUsed = false;
                    shamanEngine.ResetSpellsBeforeRitual();
                    Console.WriteLine("Ritual activated");
                    return true;
                }
            }
            return false;
        }

        private bool ProcessRitualEndEvent(string nwnEvent)
        {
            if (!string.IsNullOrWhiteSpace(nwnEvent))
            {
                if (nwnEvent.Contains("Вы прервали ритуал"))
                {
                    if (eventsState.RitualState == NwnEventsState.RITUAL_STATE_ACTIVATED)
                    {
                        Console.WriteLine("Ritual interrupted");
                    }
                    shamanEngine.StopSpellPreparation();
                    eventsState.RitualState = NwnEventsState.RITUAL_STATE_NOT_ACTIVATED;
                    eventsState.ShamanStoneUsed = false;
                    return true;
                }
            }
            return false;
        }
    }
}
