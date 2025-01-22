using System.ComponentModel;
using System.Windows;
using PeopleVilleEngine;
using PeopleVilleEngine.Time;

namespace VisualVillage
{
    public partial class MainWindow : Window
    {
        private Village _village;
        private TimeKeeper _timeKeeper;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            InitializeVillage();
        }

        private void InitializeVillage()
        {
            _village = new Village();
            _timeKeeper = TimeKeeper.GetInstance(_village);
        }
    }
}
