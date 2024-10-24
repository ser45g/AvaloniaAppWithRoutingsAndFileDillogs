using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.SkiaSharpView.Avalonia;
using LiveChartsCore.SkiaSharpView;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using Avalonia.Controls;
using Avalonia.Media;

namespace TestRoutingProject.ViewModels
{
    public partial class ChartsViewModel : ViewModelBase, IModalDialogViewModel, ICloseable, IViewClosed
    {
        public bool? DialogResult => true;

        public event EventHandler? RequestClose;

        public ChartsViewModel(string initialMessage,string changedMessage, bool isDecyphered)
        {
            List<ISeries> initialSeries = new List<ISeries>();
            List<ISeries> changedSeries = new List<ISeries>();

            this._initialMessage = initialMessage;
            this._changedMessage = changedMessage;
            this._isDecyphered = isDecyphered;

            initialSeries.Add(SetupInitialColumnSeries());
            changedSeries.Add(SetupChangedColumnSeries());



            XAxesInitial = new List<Axis>() {
                new Axis(){MinZoomDelta=0.1, Labels=  _initialMessage.Distinct().Select(x=>x.ToString()).ToList(),ForceStepToMin=true, MinStep=1 }
            }.ToArray();

            YAxesInitial = new List<Axis>() {
                new Axis(){MinStep=2,ForceStepToMin=true, MinZoomDelta=0.1 }
            }.ToArray();

            XAxesChanged = new List<Axis>() {
                new Axis(){ Labels = _changedMessage.Distinct().Select(x => x.ToString()).ToList(),ForceStepToMin=true, MinStep=1}
            }.ToArray();

            YAxesChanged = new List<Axis>() {
                new Axis(){MinStep = 2,ForceStepToMin=true}
            }.ToArray();


            InitialSeries = initialSeries.ToArray();
            ChangedSeries = changedSeries.ToArray();


        }

        private ColumnSeries<int> SetupInitialColumnSeries()
        {
            ColumnSeries<int> columnSeries = new ColumnSeries<int>();
           
            List<char> dictinctiveNums = _initialMessage.Distinct().ToList();
            List<int> numberOfOccurencesNums = new List<int>();
            foreach (char c in dictinctiveNums)
            {
                int count = _initialMessage.Where(x => x == c).Count();
                numberOfOccurencesNums.Add(count);
            }


            columnSeries.Values = new List<int>(numberOfOccurencesNums);

            return columnSeries;
        }
        private ColumnSeries<int> SetupChangedColumnSeries()
        {
            ColumnSeries<int> columnSeries = new ColumnSeries<int>();
            
            List<char> dictinctiveNums = _changedMessage.Distinct().ToList();
            List<int> numberOfOccurencesNums = new List<int>();
            foreach (char c in dictinctiveNums)
            {
                int count = _changedMessage.Where(x => x == c).Count();
                numberOfOccurencesNums.Add(count);
            }



            columnSeries.Values = new List<int>(numberOfOccurencesNums);
            
            return columnSeries;
        }

        public void OnClosed()
        {
            //LiveChartsCore.SkiaSharpView.Avalonia.CartesianChart d= new CartesianChart();
            
        }

        [ObservableProperty]
        private ISeries[] _initialSeries;
        
        [ObservableProperty]
        private ISeries[] _changedSeries;

        [ObservableProperty]
        private Axis[] _xAxesInitial;
        
        [ObservableProperty]
        private Axis[] _xAxesChanged;
        
        [ObservableProperty]
        private Axis[] _yAxesInitial;
        
        [ObservableProperty]
        private Axis[] _yAxesChanged;

        private string _initialMessage;
        private string _changedMessage;
        private bool _isDecyphered;

        [RelayCommand]
        private void Close()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
