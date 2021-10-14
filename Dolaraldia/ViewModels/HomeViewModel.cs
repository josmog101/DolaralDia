using Acr.UserDialogs;
using Dolaraldia.Helpers;
using Dolaraldia.Models;
using Dolaraldia.Services;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Dolaraldia.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ApiService _apiService;
        ToastLength toastLength = ToastLength.Short;
        public ObservableCollection<HistoryItemViewModel> _listHistory;
        private string _txtBs, _txtBsD,_txtDolar, _iconView, _valuePorcent, _priceDolar, _dateDolar, _hourDolar;
        private string _valuePorcentDolar, _colorTextDolar, _iconViewDolar;
        // private Double _valorDolar, _valorDolarOld;
        private bool _band, _bandBs, _bandBsD;
        private Color _colorTextPorcent, _colorTextPriceAct;
        private bool _isVisibleLoad, _isVisibleLoadData,_isVisibleReloadData, _isVisibleTextNotData, _isVisibletDataHistoryView;
        public ObservableCollection<HistoryItemViewModel> ListHistory
        {
            get { return this._listHistory; }
            set { SetValue(ref this._listHistory, value); }
        }
        public string TxtDolar
        {
            get { return this._txtDolar; }
            set { SetValue(ref this._txtDolar, value); }
        }
        public string TxtBs
        {
            get { return this._txtBs; }
            set { SetValue(ref this._txtBs, value); }
        }
        public string TxtBsD
        {
            get { return this._txtBsD; }
            set { SetValue(ref this._txtBsD, value); }
        }
        public string PriceDolar
        {
            get { return this._priceDolar; }
            set { SetValue(ref this._priceDolar, value); }
        }
        public string DateDolar
        {
            get { return this._dateDolar; }
            set { SetValue(ref this._dateDolar, value); }
        }
        public string HourDolar
        {
            get { return this._hourDolar; }
            set { SetValue(ref this._hourDolar, value); }
        }
        public string ValuePorcentDolar
        {
            get { return this._valuePorcentDolar; }
            set { SetValue(ref this._valuePorcentDolar, value); }
        }
        public string ColorTextDolar
        {
            get { return this._colorTextDolar; }
            set { SetValue(ref this._colorTextDolar, value); }
        }
        public string IconViewDolar
        {
            get { return this._iconViewDolar; }
            set { SetValue(ref this._iconViewDolar, value); }
        }
        public string IconView
        {
            get { return this._iconView; }
            set { SetValue(ref this._iconView, value); }
        }
        public string ValuePorcent
        {
            get { return this._valuePorcent; }
            set { SetValue(ref this._valuePorcent, value); }
        }
        public Color ColorTextPorcent
        {
            get { return this._colorTextPorcent; }
            set { SetValue(ref this._colorTextPorcent, value); }
        }
        public Color ColorTextPriceAct
        {
            get { return this._colorTextPriceAct; }
            set { SetValue(ref this._colorTextPriceAct, value); }
        }
        public bool IsVisibleLoad
        {
            get { return this._isVisibleLoad; }
            set { SetValue(ref this._isVisibleLoad, value); }
        }
        public bool IsVisibleLoadData
        {
            get { return this._isVisibleLoadData; }
            set { SetValue(ref this._isVisibleLoadData, value); }
        }
        public bool IsVisibleReloadData
        {
            get { return this._isVisibleReloadData; }
            set { SetValue(ref this._isVisibleReloadData, value); }
        }
        public bool IsVisibleTextNotData
        {
            get { return this._isVisibleTextNotData; }
            set { SetValue(ref this._isVisibleTextNotData, value); }
        }
        public bool IsVisibletDataHistoryView
        {
            get { return this._isVisibletDataHistoryView; }
            set { SetValue(ref this._isVisibletDataHistoryView, value); }
        }
        public HomeViewModel()
        {
            _apiService = new ApiService();
            IsVisibleLoad = true;
            IsVisibleLoadData = false;
            IsVisibleReloadData = false;
            IsVisibleTextNotData = false;
            _band = false;
            _bandBs = false;
            _bandBsD = false;
            TxtDolar = "0";
            TxtBs = "0";
            TxtBsD = "0";
            if (!string.IsNullOrEmpty(Settings.PriceDolarAct))
            {
                PriceDolar = Settings.PriceDolarAct;
                DateDolar = Settings.DateDolarAct;
                HourDolar = Settings.HourDolarAct;
                ValuePorcentDolar = Settings.ValuePorcentDolarAct;
                ColorTextDolar = Settings.ColorTextDolarAct;
                IconViewDolar = Settings.IconViewDolarAct;
                var _dh = JsonConvert.DeserializeObject(Settings.HistoryDolarAct);
                if (Settings.HistoryDolarAct!="null")
                {
                    MainViewModel.GetInstace().DataApi = new ApiData();
                    MainViewModel.GetInstace().DataApi.PriceDataHistory = JsonConvert.DeserializeObject<List<DataHistory>>(Settings.HistoryDolarAct);
                    if (MainViewModel.GetInstace().DataApi.PriceDataHistory != null)
                    {
                        IsVisibletDataHistoryView = true;
                        IsVisibleTextNotData = false;
                        ListHistory = new ObservableCollection<HistoryItemViewModel>(ToListHistoryItemViewModel().Reverse());
                    }
                    else
                    {
                        IsVisibletDataHistoryView = false;
                        IsVisibleTextNotData = true;
                        ListHistory = null;
                    }
                }
                else
                {
                    IsVisibletDataHistoryView = false;
                    IsVisibleTextNotData = true;
                    ListHistory = null;
                }
                
                if (ColorTextDolar == "Green")
                {
                    ColorTextPriceAct = Color.FromHex("#3061F2");
                }
                else
                {
                    ColorTextPriceAct = Color.FromHex("#cc100D");
                }
            }
            else
            {
                PriceDolar = "0";
                DateDolar = "01/01/1900";
                HourDolar = "01:00 pm";
                ValuePorcentDolar = "0";
                ColorTextDolar = "Black";
                IconViewDolar = "";
            }
            
            LoadData();
        }

        public async void LoadData()
        {
            try
            {
                IsVisibleLoad = true;
                IsVisibleReloadData = false;
                var resp = await _apiService.GetData<ApiData>(
                                App.Current.Resources["ApiSecurity"].ToString());
                if (!resp.IsSuccess)
                {
                    IsVisibleLoad = false;
                    IsVisibleLoadData = true;
                    IsVisibleReloadData = false;
                    return;
                }
                MainViewModel.GetInstace().DataApi = (ApiData)resp.Result;
                var _mvm = MainViewModel.GetInstace().DataApi;
                PriceDolar = _mvm.PriceNow;
                DateDolar = _mvm.DateNow;
                HourDolar = _mvm.HourNow;
                ValuePorcentDolar = _mvm.ValuePorcentNow;
                ColorTextDolar = _mvm.ColorTextNow;
                if (_mvm.ColorTextNow == "Green")
                {
                    ColorTextPriceAct = Color.FromHex("#3061F2");
                }
                else
                {
                    ColorTextPriceAct = Color.FromHex("#cc100D");
                }
                IconViewDolar = _mvm.IconViewNow;
                Settings.DateDolarAct = _mvm.DateNow;
                Settings.HourDolarAct = _mvm.HourNow;
                Settings.PriceDolarAct = _mvm.PriceNow;
                Settings.ValuePorcentDolarAct = _mvm.ValuePorcentNow;
                Settings.ColorTextDolarAct = _mvm.ColorTextNow;
                Settings.IconViewDolarAct = _mvm.IconViewNow;
                Settings.HistoryDolarAct = JsonConvert.SerializeObject(_mvm.PriceDataHistory);
                if (MainViewModel.GetInstace().DataApi.PriceDataHistory != null)
                {
                    IsVisibletDataHistoryView = true;
                    IsVisibleTextNotData = false;
                    ListHistory = new ObservableCollection<HistoryItemViewModel>(ToListHistoryItemViewModel().Reverse());
                }
                else
                {
                    IsVisibleTextNotData = true;
                    IsVisibletDataHistoryView = false;
                    Settings.HistoryDolarAct = string.Empty;
                    ListHistory = null;
                }
                IsVisibleLoad = false;
                IsVisibleLoadData = true;
                IsVisibleReloadData = false;
            }
            catch (Exception ex)
            {
                IsVisibleLoad = false;
                IsVisibleLoadData = true;
                IsVisibleReloadData = false;
            }
        }

        private IEnumerable<HistoryItemViewModel> ToListHistoryItemViewModel()
        {
            return MainViewModel.GetInstace().DataApi.PriceDataHistory.Select(l=> new HistoryItemViewModel
            {
                DateGroup = l.DateGroup,
                PriceData = l.PriceData,
                Status = l.Status
            });
        }
        /*
        private void CalculatePorcent()
        {
            if (_valorDolar > _valorDolarOld)
            {
                IconView = "ic_up_green";
                var v1 = _valorDolar - _valorDolarOld;
                var v2 = v1 / _valorDolarOld;
                ValuePorcent = (v2 * 100).ToString("N2") + "%";
                ColorTextPorcent = Color.Green;
            }
            if (_valorDolar < _valorDolarOld)
            {
                IconView = "ic_down_red";
                var v1 = _valorDolar - _valorDolarOld;
                var v2 = v1 / _valorDolarOld;
                ValuePorcent = (v2 * 100).ToString("N2") + "%";
                ColorTextPorcent = Color.Red;
            }
            if (_valorDolar == _valorDolarOld)
            {
                IconView = "";
                ValuePorcent = "0%";
                ColorTextPorcent = Color.Black;
            }
        }
        */
        public void ConverToBs()
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtDolar))
                {
                    if (_band == false)
                    {
                        var result = Convert.ToDouble(PriceDolar) * Convert.ToDouble(TxtDolar);
                        var resultDigital = result;/// 1000000.00;
                        var result2 = (1000000.00*result).ToString();
                        if (result2 == "0")
                        {
                            TxtBs = "0";
                            TxtBsD = "0";
                        }
                        else
                        {
                            TxtBs = result2;
                            TxtBsD = resultDigital.ToString();
                        }
                        _band = true;
                    }
                    else
                    {
                        _band = false;
                    }
                }
                else
                {
                    TxtBs = "0";
                    TxtBsD = "0";
                }
                
            }
            catch (Exception)
            {

            }
        }

        public void ConverToDolar()
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtBs))
                {
                    if (_bandBs == false)
                    {
                        var result = Convert.ToDouble(TxtBs) / Convert.ToDouble(PriceDolar);
                        var resultDigital = Convert.ToDouble(TxtBs) / 1000000.00;
                        var result2 = (result/1000000.00).ToString();
                        if (result2 == "0")
                        {
                            TxtDolar = "0";
                            TxtBsD = "0";
                        }
                        else
                        {
                            TxtDolar = result2;
                            TxtBsD = resultDigital.ToString();
                        }
                        _bandBs = true;
                    }
                    else
                    {
                        _bandBs = false;
                    }
                }
                else
                {
                    TxtBs = "0";
                }
            }
            catch (Exception)
            {

            }
        }
        public void ConverToDigital()
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtBsD))
                {
                    if (_bandBsD == false)
                    {
                        var result = Convert.ToDouble(TxtBsD) * 1000000.00;
                        var resultDolar = (result / Convert.ToDouble(PriceDolar)/1000000.00);
                        var result2 = result.ToString();
                        if (result2 == "0")
                        {
                            TxtBs = "0";
                            TxtDolar = "0";
                        }
                        else
                        {
                            TxtBs = result2;
                            TxtDolar = resultDolar.ToString();
                        }
                        _bandBsD = true;
                    }
                    else
                    {
                        _bandBsD = false;
                    }
                }
                else
                {
                    TxtBsD = "0";
                }
            }
            catch (Exception)
            {

            }
        }
        public string ConvertToAmount(string _value)
        {
            var _amountConv = _value.Replace(",", "_");
            var _amountConv2 = _amountConv.Replace(".", ",");
            return _amountConv2.Replace("_", ".");
        }
        public string MyNumberFormatBs(string _priceCurrency)
        {
            NumberFormatInfo nfi = new CultureInfo("es-ES", false).NumberFormat;
            nfi.CurrencyPositivePattern = 0;
            //nfi.CurrencyDecimalSeparator = ",";

            nfi.CurrencySymbol = "Bs. ";
            return String.Format(nfi, "{0:C2}", Decimal.Parse(_priceCurrency));

        }
        public ICommand CopyTextCommand
        {
            get
            {
                return new RelayCommand(CopyText);
            }

        }

        private async void CopyText()
        {
            await Clipboard.SetTextAsync(MyNumberFormatBs(PriceDolar));
            CrossToastPopUp.Current.ShowToastMessage("Copiado", toastLength);
            //DependencyService.Get<IMessage>().ShortAlert("Copiado");

        }
        public ICommand CopyTextDolarBsCommand
        {
            get
            {
                return new RelayCommand(CopyTextDolarBs);
            }

        }

        private async void CopyTextDolarBs()
        {
            if (!string.IsNullOrEmpty(TxtDolar) && TxtDolar!="0")
            {
                await Clipboard.SetTextAsync(MyNumberFormatBs(TxtDolar));
                CrossToastPopUp.Current.ShowToastMessage("Copiado", toastLength);
            }
        }
        public ICommand CopyTextBsCommand
        {
            get
            {
                return new RelayCommand(CopyTextBs);
            }

        }

        private async void CopyTextBs()
        {
            if (!string.IsNullOrEmpty(TxtDolar) && TxtDolar != "0")
            {
                await Clipboard.SetTextAsync(MyNumberFormatBs(TxtBs));
                CrossToastPopUp.Current.ShowToastMessage("Copiado", toastLength);
            }
        }

        public ICommand CopyTextBsDigitalCommand
        {
            get
            {
                return new RelayCommand(CopyTextBsDigital);
            }

        }

        private async void CopyTextBsDigital()
        {
            if (!string.IsNullOrEmpty(TxtBsD) && TxtBsD != "0")
            {
                await Clipboard.SetTextAsync(MyNumberFormatBs(TxtBsD));
                CrossToastPopUp.Current.ShowToastMessage("Copiado", toastLength);
            }
        }
        public ICommand ClearDataCommand
        {
            get
            {
                return new RelayCommand(ClearData);
            }

        }

        private void ClearData()
        {
            TxtDolar = "0";
            TxtBs = "0";
            TxtBsD = "0";
        }
    }
}
