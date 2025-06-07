using System.Drawing;
using System.Text.RegularExpressions;

namespace employee_front_end.Model
{
    public class State
    {
            public int StateInt { get; set; } 
            public string Name { get; set; }
            public string Initials{ get; set; }
            public string Colour { get; set; }
            public string Reason { get; set; }
            public string StartShift { get; set; }
            public string EndShift { get; set; }
            public string StateDescription { get; set; }

        public State(int stateInt, string name)
        {
            Name = name;
            StateInt = stateInt;
            if (StateInt == 0)
            {
                StateDescription = "Checked in";
                Colour = "#20CB15";
            }
            else if (StateInt == 1)
            {
                StateDescription = "Scheduled to work today, not checked in";
                Colour = "#FF7657";
            }
            else if (StateInt == 2)
            {
                StateDescription = "Scheduled to work at" + StartShift;
                Colour = "#5193E8";
            }
            else if (StateInt == 3)
            {
                StateDescription = "Ended to work at" + EndShift;
                Colour = "#5193E8";
            }
            else if (StateInt == 4)
            {
                StateDescription = "scheduled to be absent because" + Reason;
                Colour = "#E5C215";
            }
            else if (StateInt == 5)
            {
                StateDescription = "Not scheduled to work";
                Colour = "#5193E8";
            }
            else if (StateInt == 404)
            {
                StateDescription = "Error";
                Colour = "#FFFFFF";
            }

            if(name is not null)
            {
                string patternLastName = @"\b(\w+)$";
                string patternFristName = @"(?:^|(?:[.!?]\s))(\w+)";

                var matchLastName = Convert.ToString(Regex.Match(name, patternLastName));
                var matchFristName = Convert.ToString(Regex.Match(name, patternFristName));
                if ((matchLastName != matchFristName) & matchLastName != "")
                {
                    Initials = matchFristName.Substring(0, 1) + matchLastName.Substring(0, 1);
                }
                else
                {
                    Initials = matchFristName.Substring(0, 2);
                }
            }
        }
    }
}
