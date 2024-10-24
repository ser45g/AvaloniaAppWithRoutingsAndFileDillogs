using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cyphers;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FileSystem;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRoutingProject.Business;
using TestRoutingProject.Models;
using TestRoutingProject.Services;

namespace TestRoutingProject.ViewModels
{
    public partial class UserControl3ViewModel:UserControl2Page
    {
        private readonly IDialogService _dialogService;
        private readonly IStorageService _storage;
        private readonly CyphersManager _cyphersManager;

        [ObservableProperty]
        private List<string> _cypherNames;
      
        
        [ObservableProperty]
        private List<string> _chartTypeNames;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ShowInitialAndCypheredCharts))]
        [NotifyCanExecuteChangedFor(nameof(ShowChartsCommand))]
        private int _selectedChartTypeNameIndex;


        private bool ShowInitialAndCypheredCharts => ChartTypeNames[SelectedChartTypeNameIndex] == "Исходное и зашифрованное сообщение";

        
        public bool CanShowCharts
        {
            get
            {
                if (ShowInitialAndCypheredCharts) {
                    return InitialMessage != null && EncryptedMessage != null;
                }
                else {
                    return InitialMessage != null && DecryptedMessage != null; 
                }
            }
        }

        [ObservableProperty]
        private int _selectedCypherNameIndex; 

        [ObservableProperty]
        private string? _message; 
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ShowChartsCommand))]
        private string? _initialMessage; 

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ShowChartsCommand))]
        private string? _encryptedMessage; 

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ShowChartsCommand))]
        private string? _decryptedMessage;

        [ObservableProperty]
        private bool _doesOverwrite;

        public UserControl3ViewModel(IDialogService dialogService, IStorageService storage, CyphersManager cyphersManager)
        {
            _dialogService = dialogService;
            _storage = storage;
            this._cyphersManager = cyphersManager;
            
            CypherNames = new List<string>(_cyphersManager.GetKeys());
            ChartTypeNames = new List<string>() {  "Исходное и зашифрованное сообщение","Исходное и расшифрованное сообщение"};
        }                                         

        [RelayCommand]
        private void CypherDecypher()
        {
            if (Message == null) { return; }
            InitialMessage = Message;

            ICypher cypher = _cyphersManager.GetCypher(CypherNames[SelectedCypherNameIndex]);


            string result;
            if (IsDecypherMode == false) {  
                result = cypher.Encrypt(Message);
                EncryptedMessage = result;
                DecryptedMessage = null;
                
            }
            else
            {
                result = cypher.Decrypt(Message);
                DecryptedMessage = result;
                EncryptedMessage = null;
            }
            Message = result;
        }

        [RelayCommand(CanExecute =nameof(CanShowCharts))]
        private async Task ShowCharts()
        {
            ChartsViewModel vm=new ChartsViewModel(InitialMessage, ShowInitialAndCypheredCharts? EncryptedMessage:DecryptedMessage,!ShowInitialAndCypheredCharts);

            await _dialogService.ShowDialogAsync(App.MainViewModel,vm);

        }
        [RelayCommand]
        private async Task SetUpCypher()
        {
            string cypherName = CypherNames[SelectedCypherNameIndex];
            ICypher cypher = (_cyphersManager.GetCypher(cypherName));
            if (cypher is CeaserCypher) {
                CeaserCypherSettingsViewModel vm = new(cypher as CeaserCypher);

                bool? dialogResult=await _dialogService.ShowDialogAsync(App.MainViewModel, vm);
                if(dialogResult==true)
                    _cyphersManager.SetCypher<CeaserCypher>(cypherName, new CeaserCypher(vm.Shift));
                
                return; 
            }
            if(cypher is InverseCypher)
            {
                NoSettingsAvailableViewModel vm = new();
                await _dialogService.ShowDialogAsync(App.MainViewModel,vm);
                return;
            }
            if(cypher is ReplacementCypher)
            {
                ReplacementCypherSettingsViewModel vm = new(cypher as ReplacementCypher);

                bool? dialogResult = await _dialogService.ShowDialogAsync(App.MainViewModel, vm);
                if (dialogResult == true)
                    _cyphersManager.SetCypher<ReplacementCypher>(cypherName, ReplacementCypher.Instantiate(vm.Order.ToArray()));

                return;
            }
        }

        [ObservableProperty]
        private bool _isCypherMode=true;

        [ObservableProperty]
        private bool _isDecypherMode;

        [RelayCommand]
        private async Task OpenFile()
        {
            try
            {
                var file = await _dialogService.ShowOpenFileDialogAsync(App.MainViewModel);
                if(file==null)
                    return;
                if ( file?.Path == null)
                {
                    throw new ArgumentNullException();
                }

                using Stream stream = await file.OpenReadAsync();
                using StreamReader reader = new StreamReader(stream);
                Message = await reader.ReadToEndAsync();

            }
            catch (Exception ex) { 
                 await _dialogService.ShowMessageBoxAsync(App.MainViewModel, "Возникли ошибки при открытии файла", "Ошибка открытия файла", MessageBoxButton.Ok, MessageBoxImage.Error);
            } 
        }
        
        [RelayCommand]
        private async Task SaveFile()
        {
            try
            {
                var file = await _dialogService.ShowSaveFileDialogAsync(App.MainViewModel);
                if (file==null) 
                    return;
                if(file?.Path == null)
                {
                    throw new ArgumentNullException();
                } 
                    
                if (DoesOverwrite)
                {
                    await File.WriteAllTextAsync(file.Path.AbsolutePath, Message);
                }
                else
                {
                    await File.AppendAllTextAsync(file.Path.AbsolutePath, Message);
                }
            }
            catch (Exception ex) {
                await _dialogService.ShowMessageBoxAsync(App.MainViewModel, "Возникли ошибки при сохранении файла", "Ошибка сохранения файла", MessageBoxButton.Ok, MessageBoxImage.Error);
            }
            
           
        }
    }
}
