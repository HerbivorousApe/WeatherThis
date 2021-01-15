using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherThis.Web.Models
{
    public class ButtonModel
    {
        public bool State { get; set; }

        public ButtonModel(bool state)
        {
            State = state;
        }
    }
}
