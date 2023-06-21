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
                "Wyniki Matur - Matematyka",
                "Wyniki Matur - Biologia",
                "Wyniki Matur - Chemia",
                "Wielkosc Dotacji"
            };

            SelectedItem = ComboBoxItems.FirstOrDefault();
        }

        public void HandleComboBoxSelection(string selectedItems)
        {

        }

    }
}
