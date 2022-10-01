using AppCustoViagemMelhorado.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCustoViagemMelhorado.Helper
{

    // A Definição desta classe funcionará como uma abstração de acesso ao arquivo db3 do SQLite. Esta classe contém as informações de "conexão" e os métodos para realizar o CRUD (Create,Read, Update e Delete).Todos os métodos são Async, isso significa que todos são executados via Threadso que, em teoria) não travará a interface do app enquanto os dados são gravados no arquivo db3.

    public class SQLiteDatabaseHelperViagem
    {

        // Campo da classe que armazenará a "conexão" com o arquivo db3.Isso siginifica que o arquivo db3 será aberto e armazenado aqui para que essa classe possa usar os métodos da classe do SQLite para gravar e ler dados do arquivo.

        readonly SQLiteAsyncConnection _conn;

        //Método construtor da classe que receberá um parâmetro chamado path para "conectar" ao arquivo db3.


        public SQLiteDatabaseHelperViagem(string path)
        {
            // Abrindo uma nova "conexão" com o arquivo db3 através do caminho recebido note a utilização da biblioteca SQLite "instalada" no projeto via pacote Nuget


            _conn = new SQLiteAsyncConnection(path);

         
            _conn.CreateTableAsync<Viagem>().Wait();
        }


    
        public Task<int> Insert(Viagem v)
        {
            return _conn.InsertAsync(v);
        }


        public Task<List<Viagem>> Update(Viagem v)
        {
            string sql = "UPDATE Viagem SET Origem=?, Destino=?  Distancia=?, Consumo=? Preco=? WHERE Id= ? ";
            return _conn.QueryAsync<Viagem>(sql, v.Origem, v.Destino, v.Distancia, v.Consumo, v.Preco, v.Id);
        }

   
        public Task<List<Viagem>> GetAll()
        {
            return _conn.Table<Viagem>().ToListAsync();
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Viagem>().DeleteAsync(i => i.Id == id);
        }


    }
}
