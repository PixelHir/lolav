namespace LolAV.Properties {
    
    
    // Ta klasa umożliwia obsługę określonych zdarzeń w klasie ustawień:
    //  Zdarzenie SettingChanging jest wywoływane przed zmianą wartości ustawień.
    //  Zdarzenie PropertyChanged jest wywoływane po zmianie wartości ustawień.
    //  Zdarzenie SettingsLoaded jest wywoływane po załadowaniu wartości ustawień.
    //  Zdarzenie SettingsSaving jest wywoływane przed zapisaniem wartości ustawień.
    internal sealed partial class Settings {
        
        public Settings() {
            // // Aby dodać obsługę zdarzeń dla zapisu i zmiany ustawień, należy usunąć komentarz w poniższych wierszach:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Dodaj tutaj kod obsługi zdarzenia SettingChangingEvent.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Dodaj tutaj kod obsługi zdarzenia SettingsSaving.
        }
    }
}
