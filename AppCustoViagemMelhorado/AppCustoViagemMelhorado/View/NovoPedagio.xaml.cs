using AppCustoViagemMelhorado.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCustoViagemMelhorado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovoPedagio : ContentPage
    {
        public NovoPedagio()
        {
            InitializeComponent();
        }
        //Tratará o evento do clicked do ToolbarItem
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Preencherá a model do Produto com os dados digitados pelo usuário
                Pedagio p = new Pedagio
                {
                 
                    Localizacao = txt_localizacao.Text,
                    Valor = Convert.ToDouble(txt_valor.Text),


                };
               

                //Fará a inserção dos dados no banco de dados
                await App.Database.Insert(p);


                //Avisará do sucesso da operação
                await DisplayAlert("Sucesso!", "Pedagio Cadastrado", "OK");

                //Navegará para a pagina ListaProdutos
                await Navigation.PushAsync(new ListaPedagios());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}