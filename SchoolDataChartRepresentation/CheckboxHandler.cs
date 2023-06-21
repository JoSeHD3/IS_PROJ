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
        public bool IsChecked { get; set; }
        public CheckBox Checkbox { get => checkbox; set => checkbox = value; }
        public string Province { get => province; set => province = value; }

        public CheckboxHandler(string province)
        {
            this.Province = province;
            this.IsChecked = false;
            initializeCheckbox();
        }

        private void initializeCheckbox()
        {
            Checkbox = new CheckBox();
            Checkbox.Content = Province;
        }

        override public string ToString()
        {
            return Province;
        }
    }
}
