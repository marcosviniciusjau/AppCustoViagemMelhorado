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
                //Preencherá a model do Produto com os dados digitados pelo usuário
                Pedagio p = new Pedagio
                {
                    Id = pedagio_anexado.Id,
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

                    await App.Database.Clear(p);
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
                double soma = lista_produtos.Sum(i => i.Valor);

                
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