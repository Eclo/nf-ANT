using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using MFDeploy.Services.Dialog;
using MFDeploy.Services.BusyService;
using MFDeploy.Services.NetMicroFrameworkService;
using Microsoft.NetMicroFramework.Tools.MFDeployTool.Engine;
using System.Collections.ObjectModel;
using MFDeploy.Utilities;
using Microsoft.Practices.ServiceLocation;

namespace MFDeploy.ViewModels
{
    public class MainPageViewModel : MyViewModelBase
    {
        //private instance of Main to get general stuff
        private MainViewModel MainVM { get { return ServiceLocator.Current.GetInstance<MainViewModel>(); } }

        public MainPageViewModel(IMyDialogService dlg, IBusyService busy)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }

            this.DialogSrv = dlg;
            this.BusySrv = busy;

        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }



        #region Navigation
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
           
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
            MainVM.PageHeader = Res.GetString("MP_PageHeader");

        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        #endregion

    }
}

