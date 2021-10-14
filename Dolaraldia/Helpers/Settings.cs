using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dolaraldia.Helpers
{
    public static class Settings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        const string priceDolarAct = "0";
        const string dateDolarAct = "dateDolarAct";
        const string hourDolarAct = "hourDolarAct";
        const string valuePorcentDolarAct = "valuePorcentDolarAct";
        const string colorTextDolarAct = "colorTextDolarAct";
        const string iconViewDolarAct = "iconViewDolarAct";
        const string historyDolarAct = "historyDolarAct";
        static readonly string stringDefault = string.Empty;
        public static string PriceDolarAct
        {
            get
            {
                return AppSettings.GetValueOrDefault(priceDolarAct, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(priceDolarAct, value);
            }
        }
        public static string DateDolarAct
        {
            get
            {
                return AppSettings.GetValueOrDefault(dateDolarAct, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(dateDolarAct, value);
            }
        }
        public static string HourDolarAct
        {
            get
            {
                return AppSettings.GetValueOrDefault(hourDolarAct, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(hourDolarAct, value);
            }
        }
        public static string ValuePorcentDolarAct
        {
            get
            {
                return AppSettings.GetValueOrDefault(valuePorcentDolarAct, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(valuePorcentDolarAct, value);
            }
        }
        public static string ColorTextDolarAct
        {
            get
            {
                return AppSettings.GetValueOrDefault(colorTextDolarAct, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(colorTextDolarAct, value);
            }
        }
        public static string IconViewDolarAct
        {
            get
            {
                return AppSettings.GetValueOrDefault(iconViewDolarAct, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(iconViewDolarAct, value);
            }
        }
        public static string HistoryDolarAct
        {
            get
            {
                return AppSettings.GetValueOrDefault(historyDolarAct, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(historyDolarAct, value);
            }
        }

    }
}
