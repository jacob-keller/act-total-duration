using System.Windows.Forms;
using Advanced_Combat_Tracker;
using System.Linq;
using System;

namespace ACT_Plugin
{
    public class ActTotalDuration : IActPluginV1
    {
        // The status label that appears in ACT's plugin tab
        private Label status;

        public void DeInitPlugin()
        {
            status.Text = "Plugin Exited";
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            // Track the status label's reference in our private variable
            status = pluginStatusText;

            // We don't remove the TotalDuration field during DeInitPlugin. Remove the fields now to prevent a conflict if we are re-initialising.
            EncounterData.ColumnDefs.Remove("TotalDuration");
            CombatantData.ColumnDefs.Remove("TotalDuration");

            var EncounterDurationColumn = new EncounterData.ColumnDef("TotalDuration", true, "VARCHAR", "TotalDuration",
                (Data) => { return GetDurationString(GetTotalEncounterDuration(Data)); },
                (Data) => { return GetDurationString(GetTotalEncounterDuration(Data)); }
                );

            // Add the new column definition for the encounter
            EncounterData.ColumnDefs.Add("TotalDuration", EncounterDurationColumn);

            var CombatantDurationColumn = new CombatantData.ColumnDef("TotalDuration", true, "VARCHAR", "TotalDuration",
                (Data) => { return GetDurationString(GetTotalCombatantDuration(Data)); },
                (Data) => { return GetDurationString(GetTotalCombatantDuration(Data)); },
                (Left, Right) => { return GetTotalCombatantDuration(Left).CompareTo(GetTotalCombatantDuration(Right)); }
                );

            CombatantData.ColumnDefs.Add("TotalDuration", CombatantDurationColumn);

            // Validate the new column in the Options tab
            ActGlobals.oFormActMain.ValidateTableSetup();

            status.Text = "Plugin Started";
        }
        private TimeSpan GetTotalEncounterDuration(EncounterData Data)
        {
            if (Data.EndTime < Data.StartTime)
            {
                return TimeSpan.Zero;
            }
            else
            {
                return Data.EndTime - Data.StartTime;
            }
        }

        private TimeSpan GetTotalCombatantDuration(CombatantData Data)
        {
            if (Data.EncEndTime < Data.EncStartTime)
            {
                return TimeSpan.Zero;
            }
            else
            {
                return Data.EncEndTime - Data.EncStartTime;
            }
        }

        private String GetDurationString(TimeSpan Duration)
        {
            if (Duration.TotalDays >= 1)
            {
                return Duration.ToString(@"d\.hh\:mm\:ss\.f");
            }
            else if (Duration.TotalHours >= 1)
            {
                return Duration.ToString(@"hh\:mm\:ss\.f");
            }
            else
            {
                return Duration.ToString(@"mm\:ss\.f");
            }
        }
    }
}
