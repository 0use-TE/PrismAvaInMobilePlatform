using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Styling;
using GameDevTools.Services;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using Prism.Commands;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace PrismAvaInMobilePlatform.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public DelegateCommand SwitchThemeCommand { get; set; }
        public DelegateCommand<Control> SetNotificationHostCommand { get; set; }
        public AsyncDelegateCommand TakePhotoCommand { get; set; }
        public AsyncDelegateCommand TakeVideoCommand { get; set; }
        public AsyncDelegateCommand SelectSinglePhotoCommand { get; set; }
        public AsyncDelegateCommand SelectMultiplePhotoCommand { get; set; }
        public AsyncDelegateCommand OpenFlashlightCommand { get; set; }
        public AsyncDelegateCommand CloseFlashlightCommand { get; set; }
        public AsyncDelegateCommand VibrateCommand { get; set; }
        public AsyncDelegateCommand OpenUriCommand { get; set; }
        public string MyDeviceInfo
        {
            get => field;
            set => SetProperty(ref field, value);
        }
        private readonly INotificationService _notificationService;

        public MainViewModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
            SwitchThemeCommand = new DelegateCommand(() =>
            {
                if (App.Current == null) return;

                var currentActual = App.Current.ActualThemeVariant;
                if (currentActual == ThemeVariant.Dark)
                    App.Current.RequestedThemeVariant = ThemeVariant.Light;
                else
                    App.Current.RequestedThemeVariant = ThemeVariant.Dark;
            });

            SetNotificationHostCommand = new DelegateCommand<Control>(control =>
            {
                var topLevel = TopLevel.GetTopLevel(control);
                if (topLevel == null)
                    throw new ArgumentNullException(nameof(topLevel));

                _notificationService.SetHostWindow(topLevel);
            });
#if ANDROID

            TakePhotoCommand = new AsyncDelegateCommand(async () =>
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

                if (status != PermissionStatus.Granted)
                    status = await Permissions.RequestAsync<Permissions.Camera>();

                if (status == PermissionStatus.Granted)
                {
                    var photo = await MediaPicker.Default.CapturePhotoAsync();
                    if (photo != null)
                    {
                        string localPath = photo.FullPath;
                    }
                    else
                    {
                    }
                }
            });

            TakeVideoCommand = new AsyncDelegateCommand(async () =>
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

                if (status != PermissionStatus.Granted)
                    status = await Permissions.RequestAsync<Permissions.Camera>();

                if (status == PermissionStatus.Granted)
                {
                    var video= await MediaPicker.Default.CaptureVideoAsync();
                }
            });

        SelectSinglePhotoCommand = new AsyncDelegateCommand(async () =>
            {
                await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "请选择一个文件",
                    FileTypes = FilePickerFileType.Images
                });
            });

            SelectMultiplePhotoCommand = new AsyncDelegateCommand(async () =>
            {
                await FilePicker.Default.PickMultipleAsync(new PickOptions
                {
                    PickerTitle = "请选择多个文件",
                    FileTypes = FilePickerFileType.Images
                });
            });

            OpenFlashlightCommand = new AsyncDelegateCommand(async () =>
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
                if (status != PermissionStatus.Granted)
                    status = await Permissions.RequestAsync<Permissions.Flashlight>();

                if (status == PermissionStatus.Granted)
                    await Microsoft.Maui.Devices.Flashlight.Default.TurnOnAsync();
            });

            CloseFlashlightCommand = new AsyncDelegateCommand(async () =>
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
                if (status != PermissionStatus.Granted)
                    status = await Permissions.RequestAsync<Permissions.Flashlight>();

                if (status == PermissionStatus.Granted)
                    await Microsoft.Maui.Devices.Flashlight.Default.TurnOffAsync();
            });

            VibrateCommand = new AsyncDelegateCommand(async () =>
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Vibrate>();
                if (status != PermissionStatus.Granted)
                    status = await Permissions.RequestAsync<Permissions.Vibrate>();

                if (status == PermissionStatus.Granted)
                    Microsoft.Maui.Devices.Vibration.Default.Vibrate();
            });
            OpenUriCommand = new AsyncDelegateCommand(async () =>
            {
                await Browser.Default.OpenAsync("https://0use.net");
            });

            MyDeviceInfo = $"设备类型:{DeviceInfo.Model}\n设备名:{DeviceInfo.Name}";

#endif
        }
    }
}
