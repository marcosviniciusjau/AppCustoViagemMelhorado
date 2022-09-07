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
    public partial class ListaCustoViagens : ContentPage
    {
        // A ObservableCollection é uma classe que armazena um array de objetos do tipo de Produto.Utilizamos essa classe quando estamos apresentando um array de objetos ao usuário. Diferencial dessa classe é que toda vez que um item é add, removido ou modificado no array de objetos a interface gráfica também é atualizada. Assim as modificações feitas no array sempre estão na vista do usuário.

        ObservableCollection<Viagem> lista_custo_viagens = new ObservableCollection<Viagem>();


        public ListaCustoViagens()
        {
            InitializeComponent();

            lst_custo_viagens.ItemsSource = lista_custo_viagens;
        }

        //Fará a navegação para a tela de um novo produto

        private void ToolbarItem_Clicked_Novo(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new NovaViagem());

            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        protected override void OnAppearing()
        {

            if (lista_custo_viagens.Count == 0)
            {
                //Inicializando a Thread que irá buscar o array de objetos no arquivo db3 via classe SQLiteDatabaseHelper encapsulada na propriedade Database da classe App.

                System.Threading.Tasks.Task.Run(async () =>
                {
                    //Retornando o array de objetos vindos do db3, foi usada uma variável tem do tipo List para que abaixo no foreach possamos percorrer a lista temporária e add os itens à ObservableCollection

                    List<Viagem> temp = await App.Database.GetAllRows();

                    foreach (Viagem item in temp)
                    {
                        lista_custo_viagens.Add(item);
                    }

                });
            }
        }


        //Trata o evento Clicked do MenuItem da ViewCell.ContextActions perguntando ao usuário se ele realmente deseja remover o item do arquivo db3


        private async void MenuItem_Clicked(object sender, EventArgs e)
        {

            MenuItem disparador = (MenuItem)sender;


            Viagem custo_viagens_selecionado = (Viagem)disparador.BindingContext;


            bool confirmacao = await DisplayAlert("Tem Certeza?", "Remover Item?", "Sim", "Não");

            if (confirmacao)
            {

                await App.Database.Delete(custo_viagens_selecionado.Id);


                lista_custo_viagens.Remove(custo_viagens_selecionado);
            }
        }

        //Receberá os novos valores digitados
        private void lst_custo_viagens_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Forma contraída de definir o BindingContext da página EditarProduto como sendo o Produto que foi selecionado na ListView (item da ListView) e em seguida já redicionando na navegação.

            Navigation.PushAsync(new EditarViagem
            {
                BindingContext = (Viagem)e.SelectedItem
            });
        }
    }
}