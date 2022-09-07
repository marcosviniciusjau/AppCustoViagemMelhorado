using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppCustoViagemMelhorado.Model;

namespace AppCustoViagemMelhorado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DadosViagem : ContentPage
    {
        public DadosViagem()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new ListaPedagios());

            }
            catch (Exception ex)
            {
                DisplayAlert("Ooops", ex.Message, "OK");
            }
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new ListaCustoViagens());

            }
            catch (Exception ex)
            {
                DisplayAlert("Ooops", ex.Message, "OK");
            }
        }

    }

}
