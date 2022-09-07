using AppCustoViagemMelhorado.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCustoViagemMelhorado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarPedagio : ContentPage
    {
        public EditarPedagio()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            // Obtém qual foi o Produto anexado no BindingContext da página no momento que ela foi criada e enviada para navegação.

            try
            {
                Pedagio pedagio_anexado = BindingContext as Pedagio;

                //Preencherá a Model com os valores dos entrys
                Pedagio p = new Pedagio
                {
                    Id = pedagio_anexado.Id,
                    Localizacao = txt_localizacao.Text,
                    Valor= Convert.ToDouble(txt_valor.Text),

                   
                };

                //Aqui atualizará o banco de dados com as novas informações da model
                await App.Database.Update(p);

                await DisplayAlert("Sucesso!", "Pedagio Editado", "OK");

                await Navigation.PushAsync(new ListaPedagios());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}