using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppCustoViagemMelhorado.Model;

namespace AppCustoViagemMelhorado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPedagios : ContentPage
    {
        // A ObservableCollection é uma classe que armazena um array de objetos do tipo de Produto.Utilizamos essa classe quando estamos apresentando um array de objetos ao usuário. Diferencial dessa classe é que toda vez que um item é add, removido ou modificado no array de objetos a interface gráfica também é atualizada. Assim as modificações feitas no array sempre estão na vista do usuário.

        ObservableCollection<Pedagio> lista_pedagios = new ObservableCollection<Pedagio>();


        public ListaPedagios()
        {
            InitializeComponent();
         

            lst_pedagios.ItemsSource = lista_pedagios;
        }

        //Fará a navegação para a tela de um novo produto

        private void ToolbarItem_Clicked_Novo(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new NovoPedagio());

            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }


        //Fará a soma de todos os produtos
        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            double soma = lista_pedagios.Sum(i => i.Valor);

            string msg = "O total do pedágio é: R$ " + soma;

            DisplayAlert("SOMA", msg, "OK");
        }


        protected override void OnAppearing()
        {

            if (lista_pedagios.Count == 0)
            {
                //Inicializando a Thread que irá buscar o array de objetos no arquivo db3 via classe SQLiteDatabaseHelper encapsulada na propriedade Database da classe App.

                System.Threading.Tasks.Task.Run(async () =>
                {
                    //Retornando o array de objetos vindos do db3, foi usada uma variável tem do tipo List para que abaixo no foreach possamos percorrer a lista temporária e add os itens à ObservableCollection

                    List<Pedagio> temp = await App.Database.GetAll();

                    foreach (Pedagio item in temp)
                    {
                        lista_pedagios.Add(item);
                    }
                    // Após carregar os registros para a ObservableCollection removemos o loading da tela.

                   
                });
            }
        }


        //Trata o evento Clicked do MenuItem da ViewCell.ContextActions perguntando ao usuário se ele realmente deseja remover o item do arquivo db3


        private async void MenuItem_Clicked(object sender, EventArgs e)
        {

            MenuItem disparador = (MenuItem)sender;


            Pedagio pedagio_selecionado = (Pedagio)disparador.BindingContext;


            bool confirmacao = await DisplayAlert("Tem Certeza?", "Remover Item?", "Sim", "Não");

            if (confirmacao)
            {

                await App.Database.Delete(pedagio_selecionado.Id);


                lista_pedagios.Remove(pedagio_selecionado);
            }
        }


      
        // Tratará o evento ItemSelected da ListView navegando para a página de detalhes.
        private void lst_pedagios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Forma contraída de definir o BindingContext da página EditarProduto como sendo o Produto que foi selecionado na ListView (item da ListView) e em seguida já redicionando na navegação.

            Navigation.PushAsync(new EditarPedagio
            {
                BindingContext = (Pedagio)e.SelectedItem
            });
        }
    }
}