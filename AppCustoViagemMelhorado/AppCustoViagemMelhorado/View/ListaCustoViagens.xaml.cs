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
    public partial class ListaCustoViagens : ContentPage
    {
 

        ObservableCollection<Viagem> lista_custo_viagens = new ObservableCollection<Viagem>();


        public ListaCustoViagens()
        {
            InitializeComponent();


            lst_custo_viagens.ItemsSource = lista_custo_viagens;
        }

      
        protected override void OnAppearing()
        {

            if (lista_custo_viagens.Count == 0)
            {
        
                System.Threading.Tasks.Task.Run(async () =>
                {
                  
                    List<Viagem> temp = await App.Database.GetAll();

                    foreach (Viagem item in temp)
                    {
                        lista_custo_viagens.Add(item);
                    }

                });
            }
        }



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

     
        private void lst_custo_viagens_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        
            Navigation.PushAsync(new EditarViagem
            {
                BindingContext = (Viagem)e.SelectedItem
            });
        }
    }
}