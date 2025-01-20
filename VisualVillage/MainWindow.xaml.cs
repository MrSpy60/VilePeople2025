using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Time;

namespace VisualVillage
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Village _village;
        private const int CellSize = 30; // Size of each grid cell
        private HashSet<(int, int)> _occupiedPositions = new HashSet<(int, int)>();
        private Dictionary<ILocation, (int x, int y)> _buildingPositions = new Dictionary<ILocation, (int x, int y)>();
        private DispatcherTimer _timer;
        private TimeKeeper _timeKeeper;
        private int _villagerCount;

        public event PropertyChangedEventHandler PropertyChanged;

        public int VillagerCount
        {
            get => _villagerCount;
            set
            {
                _villagerCount = value;
                OnPropertyChanged(nameof(VillagerCount));
            }
        }

        public MainWindow()
        {
            Show();
            InitializeComponent();
            DataContext = this;
            InitializeVillage();
            InitializeTimer();
        }

        private void InitializeVillage()
        {
            _village = new Village();
            _timeKeeper = TimeKeeper.GetInstance(_village);
            VillageCanvas.Loaded += (s, e) => AssignBuildingPositions();
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.4) };
            _timer.Tick += (s, e) => { _timeKeeper.PassTime(); UpdateDisplay(); };
            _timer.Start();
        }

        private void AssignBuildingPositions()
        {
            foreach (var location in _village.Locations)
            {
                _buildingPositions[location] = GetRandomUnoccupiedPosition();
            }
        }

        private void UpdateDisplay()
        {
            VillageCanvas.Children.Clear();
            _occupiedPositions.Clear();

            foreach (var location in _village.Locations)
            {
                if (_buildingPositions.TryGetValue(location, out var position))
                {
                    AddElementToCanvas(new Rectangle { Width = 20, Height = 20, Fill = Brushes.White }, position);
                }
            }

            foreach (var villager in _village.Villagers)
            {
                AddElementToCanvas(new Ellipse { Width = 10, Height = 10, Fill = Brushes.Blue }, GetRandomUnoccupiedPosition());
            }

            VillagerCount = _village.Villagers.Count;
        }

        private void AddElementToCanvas(UIElement element, (int x, int y) position)
        {
            Canvas.SetLeft(element, position.x);
            Canvas.SetTop(element, position.y);
            VillageCanvas.Children.Add(element);
            _occupiedPositions.Add(position);
        }

        private (int x, int y) GetRandomUnoccupiedPosition()
        {
            var random = new Random();
            int maxX = (int)(VillageCanvas.ActualWidth / CellSize);
            int maxY = (int)(VillageCanvas.ActualHeight / CellSize);

            (int x, int y) position;
            int attempts = 0;
            do
            {
                if (attempts++ > 100) throw new Exception("Unable to find unoccupied position");
                position = (random.Next(maxX) * CellSize, random.Next(maxY) * CellSize);
            } while (_occupiedPositions.Contains(position) || position.x >= VillageCanvas.ActualWidth - CellSize || position.y >= VillageCanvas.ActualHeight - CellSize);

            _occupiedPositions.Add(position);
            return position;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
