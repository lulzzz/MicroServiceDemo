using DNIC.AccountCenter.Core.Domain.Settings;
using DNIC.AccountCenter.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DNIC.AccountCenter.Services.Settings
{
    public interface ISettingService : IBaseService<Setting>
    {
        void SaveSetting<T>(T settings) where T : ISetting, new();

        void SetSetting<T>(string key, T value);

        T LoadSetting<T>() where T : ISetting, new();

        bool SettingExists<T, TPropType>(T settings,
         Expression<Func<T, TPropType>> keySelector) where T : ISetting, new();

        IList<Setting> GetAllSettings();

        T GetSettingByKey<T>(string key, T defaultValue = default(T), bool loadSharedValueIfNotFound = false);

        void ClearCache();

        void DeleteSetting<T, TPropType>(T settings,
         Expression<Func<T, TPropType>> keySelector) where T : ISetting, new();

        void DeleteSetting<T>() where T : ISetting, new();

        void DeleteSettings(IList<Setting> settings);
    }
}
