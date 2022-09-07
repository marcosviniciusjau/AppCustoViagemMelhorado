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
    public partial class DadosViagemMelhorada : ContentPage
    {
        App PropriedadesApp;


        public DadosViagemMelhorada()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;
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

        private void Button_Add_Clicked(object sender, EventArgs e)
        {
            try
            {
                PropriedadesApp.ArrayPedagios.Add(new Pedagio()
                {
                    Id = PropriedadesApp.ArrayPedagios.Count + 1,
                    Localizacao = txt_localizacao.Text,
                    Valor = Convert.ToDouble(txt_preco_pedagio.Text.Replace(".", ","))
                });

                DisplayAlert("Deu Certo!", "Pedágio Adicionado com Sucesso!", "OK");

                txt_localizacao.Text = string.Empty;
                txt_preco_pedagio.Text = "";
            }
            catch (Exception ex)
            {
                DisplayAlert("Ooops", ex.Message, "OK");
            }
        }

        private async void Button_Limpar_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool confirm = await DisplayAlert("Tem certeza?", "Limpar todos os campos?", "OK", "Cancelar");

                if (confirm)
                {
                    txt_origem.Text = "";
                    txt_destino.Text = "";
                    txt_consumo.Text = "";
                    txt_distancia.Text = "";
                    txt_localizacao.Text = "";
                    txt_preco_combustivel.Text = "";
                    txt_preco_pedagio.Text = "";

                    PropriedadesApp.ArrayPedagios.Clear();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ooops", ex.Message, "OK");
            }
        }

        private void Button_Calcular_Clicked(object sender, EventArgs e)
        {
            try
            {
                double valor_total_pedagios = PropriedadesApp.ArrayPedagios.Sum(item => item.Valor);

                double consumo = Convert.ToDouble(txt_consumo.Text);
                double preco_combustivel = Convert.ToDouble(txt_preco_combustivel.Text.Replace(".", ","));
                double distancia = Convert.ToDouble(txt_distancia.Text);

                double consumo_veiculo = (distancia / consumo) * preco_combustivel;

                double custo_total = consumo_veiculo + valor_total_pedagios;

                string mensagem = string.Format(
                    "Sua viagem de {0} até {1} custará {2}, sendo em combustível {3} e pedágio {4}",
                    txt_origem.Text.ToUpper(),
                    txt_destino.Text.ToUpper(),
                    custo_total.ToString("C"),
                    consumo_veiculo.ToString("C"),
                    valor_total_pedagios.ToString("C")
                );

                DisplayAlert("Custo da Viagem", mensagem, "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Ooops", ex.Message, "OK");
            }
        }
    }
}