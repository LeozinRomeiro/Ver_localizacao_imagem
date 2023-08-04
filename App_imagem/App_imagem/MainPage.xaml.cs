using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_imagem
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        async void Button_tirar_Clicked(object sender, EventArgs e)
        {
            var resultado = await MediaPicker.CapturePhotoAsync();
            if (resultado != null)
            {
                var fluxo = await resultado.OpenReadAsync();
                imagem.Source = ImageSource.FromStream(() => fluxo);
            }
        }

        async void Button_ver_Clicked(object sender, EventArgs e)
        {
            try
            {
                var localizacao = await Geolocation.GetLocationAsync(new GeolocationRequest()
                { DesiredAccuracy = GeolocationAccuracy.Best });

                if (localizacao != null)
                {
                    label_latitude.Text += localizacao.Latitude.ToString();
                    label_longitude.Text += localizacao.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException erro)
            {
                await DisplayAlert("Falhou", erro.Message, "Ok");
            }
            catch (PermissionException erro)
            {
                await DisplayAlert("Falhou", erro.Message, "Ok");
            }
            catch (Exception erro)
            {
                await DisplayAlert("Erro", erro.Message, "Ok");
            }

            await Mostrar();

        }

        public async Task Mostrar()
        {
            var localizacao = await Geolocation.GetLocationAsync(new GeolocationRequest()
            { DesiredAccuracy = GeolocationAccuracy.Best });
            var localizacao_info = new Location(localizacao.Latitude, localizacao.Longitude);
            var opcao = new MapLaunchOptions();
            opcao.Name += "Meu local";
            await Map.OpenAsync(localizacao_info, opcao);
        }

    }
}
