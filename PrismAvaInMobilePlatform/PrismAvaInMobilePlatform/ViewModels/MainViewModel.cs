using Avalonia.Controls;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using System;
using System.Threading.Tasks;

namespace PrismAvaInMobilePlatform.ViewModels
{
    internal partial class MainViewModel : ViewModelBase
    {
        [RelayCommand]
        void SwitchTheme()
        {
            if (App.Current == null) return;

            var currentActual = App.Current.ActualThemeVariant;
            App.Current.RequestedThemeVariant = currentActual == ThemeVariant.Dark
                ? ThemeVariant.Light
                : ThemeVariant.Dark;
        }

        [RelayCommand]
        async Task TakePhoto()
        {
#if ANDROID
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Camera>();

            if (status == PermissionStatus.Granted)
            {
                var photo = await MediaPicker.Default.CapturePhotoAsync();
            }
#endif
        }

        [RelayCommand]
        async Task TakeVideo()
        {
#if ANDROID
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Camera>();

            if (status == PermissionStatus.Granted)
            {
                var video = await MediaPicker.Default.CaptureVideoAsync();
            }
#endif
        }

        [RelayCommand]
        async Task SelectSinglePhoto()
        {
#if ANDROID
            await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "请选择一个文件",
                FileTypes = FilePickerFileType.Images
            });
#endif
        }

        [RelayCommand]
        async Task SelectMultiplePhoto()
        {
#if ANDROID
            await FilePicker.Default.PickMultipleAsync(new PickOptions
            {
                PickerTitle = "请选择多个文件",
                FileTypes = FilePickerFileType.Images
            });
#endif
        }

        [RelayCommand]
        async Task OpenFlashlight()
        {
#if ANDROID
            var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Flashlight>();

            if (status == PermissionStatus.Granted)
                await Flashlight.Default.TurnOnAsync();
#endif
        }

        [RelayCommand]
        async Task CloseFlashlight()
        {
#if ANDROID
            var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Flashlight>();

            if (status == PermissionStatus.Granted)
                await Flashlight.Default.TurnOffAsync();
#endif
        }

        [RelayCommand]
        async Task Vibrate()
        {
#if ANDROID
            var status = await Permissions.CheckStatusAsync<Permissions.Vibrate>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.Vibrate>();

            if (status == PermissionStatus.Granted)
                Vibration.Default.Vibrate();
#endif
        }

        [RelayCommand]
        async Task OpenUri()
        {
#if ANDROID
            await Browser.Default.OpenAsync("https://0use.net");
#endif
        }

#if ANDROID
        public string MyDeviceInfo => $"设备类型:{DeviceInfo.Model}\n设备名:{DeviceInfo.Name}";
#endif
    }
}