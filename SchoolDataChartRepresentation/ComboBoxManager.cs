using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDataChartRepresentation
{
    internal class ComboBoxManager
    {
        public List<string> ComboBoxItems { get; set; }
        public string SelectedItem { get; set; }
        public ComboBoxManager()
        {
            ComboBoxItems = new List<string>
            {
                "Wyniki Matur",
                "Ilość dotacji",
                "Różnica w zależności od dotacji",
                "2014-2020"
            };

            SelectedItem = ComboBoxItems.FirstOrDefault();
        }

        public void HandleComboBoxSelection(string selectedItems)
        {

        }

    }
}
