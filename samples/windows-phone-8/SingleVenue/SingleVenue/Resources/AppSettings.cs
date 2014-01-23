using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;

namespace SingleVenue.Resources
{
    public class AppSettings
    {
        #region singelton

        private static AppSettings instance;

        public static AppSettings Instance
        {
            get
            {
                if (instance == null)
                    instance = new AppSettings();

                return instance;
            }
        }

        #endregion

        private IsolatedStorageSettings settings;

        // keys for app settings
        private const string VoiceCommandInitializedKey = "VoiceCommandInitialized";

        // default value for app settings
        private const bool VoiceCommandInitializedDefault = false;

        private AppSettings()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;
        }

        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (settings.Contains(Key))
            {
                // If the value has changed
                if (settings[Key] != value)
                {
                    // Store the new value
                    settings[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                settings.Add(Key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value.
            if (settings.Contains(Key))
            {
                value = (T)settings[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }
            return value;
        }

        public void Save()
        {
            settings.Save();
        }

        public bool VoiceCommandInitialized
        {
            get
            {
                return GetValueOrDefault<bool>(VoiceCommandInitializedKey, VoiceCommandInitializedDefault);
            }

            set
            {
                if (AddOrUpdateValue(VoiceCommandInitializedKey, value))
                {
                    Save();
                }
            }
        }
    }
}
