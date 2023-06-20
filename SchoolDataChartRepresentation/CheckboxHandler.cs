using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SchoolDataChartRepresentation
{
    internal class CheckboxHandler
    {
        private string province;
        private CheckBox checkbox;

        public CheckBox Checkbox { get => checkbox; set => checkbox = value; }

        public CheckboxHandler(string province)
        {
            this.province = province;

            initializeCheckbox();
        }

        private void initializeCheckbox()
        {
            Checkbox = new CheckBox();
            Checkbox.Content = province;
        }

        override public string ToString()
        {
            return province;
        }
    }
}
