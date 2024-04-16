using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerStatusChecklist
{
    public static class ComboBoxOptions
    {
        public static readonly string available = "Available";
        public static readonly string maybe = "Maybe";
        public static readonly string unavailable = "Unavailable";
        public static readonly string noResponse = "No Response";
        public static readonly string removePlayer = "Remove Player";

        // ComboBox if filled with items from this list.
        public static readonly List<string> playerSelectActions = new List<string>() { available, maybe, unavailable, noResponse, removePlayer };
    }
}