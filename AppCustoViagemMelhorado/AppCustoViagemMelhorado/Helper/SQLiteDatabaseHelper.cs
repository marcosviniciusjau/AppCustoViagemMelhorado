using AppCustoViagemMelhorado.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCustoViagemMelhorado.Helper
{

    // A Definição desta classe funcionará como uma abstração de acesso ao arquivo db3 do SQLite. Esta classe contém as informações de "conexão" e os métodos para realizar o CRUD (Create,Read, Update e Delete).Todos os métodos são Async, isso significa que todos são executados via Threadso que, em teoria) não travará a interface do app enquanto os dados são gravados no arquivo db3.

    public class SQLiteDatabaseHelper
    {

        // Campo da classe que armazenará a "conexão" com o arquivo db3.Isso siginifica que o arquivo db3 será aberto e armazenado aqui para que essa classe possa usar os métodos da classe do SQLite para gravar e ler dados do arquivo.

        readonly SQLiteAsyncConnection _conn;

        //Método construtor da classe que receberá um parâmetro chamado path para "conectar" ao arquivo db3.


        public SQLiteDatabaseHelper(string path)
        {
            // Abrindo uma nova "conexão" com o arquivo db3 através do caminho recebido note a utilização da biblioteca SQLite "instalada" no projeto via pacote Nuget


            _conn = new SQLiteAsyncConnection(path);

            // Aqui será a criação da tabela com base no Model Produto de acordo com o arquivo Produto.cs na pasta Model. Apesar do Async na criação da tabela é chamado o método Wait() que define a espera da criação da tabela (se ela ainda não existir) antes de efetuar as outras operações, por exemplo, insert.

            _conn.CreateTableAsync<Pedagio>().Wait();
        }


        // Aqui é o metódo que fará a inserção de um novo registro na tabela
        public Task<int> Insert(Pedagio p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<int> Insert(Viagem p)
        {
            return _conn.InsertAsync(p);
        }

        // Aqui é o método que fará a atualização do registro na tabela

        // Aqui é o método que fará a atualização do registro na tabela


        public Task<List<Pedagio>> Update(Pedagio p)
        {
            string sql = "UPDATE Pedagio SET Localizacao=?, Valor=? WHERE Id= ? ";
            return _conn.QueryAsync<Pedagio>(sql, p.Localizacao, p.Valor, p.Id);
        }

        public Task<List<Viagem>> Update(Viagem p)
        {
            string sql = "UPDATE Viagem SET Origem=?, Destino=?  Distancia=?, Consumo=? Preco=? WHERE Id= ? ";
            return _conn.QueryAsync<Viagem>(sql, p.Origem, p.Destino, p.Distancia, p.Consumo, p.Preco, p.Id);
        }

        // Aqui é o Método que fará o retorno de todas as linhas contidas no arquivo db3 referentes a tabela Produto. Veja que o método executa a listagem de forma assíncrona.

        public Task<List<Pedagio>> GetAll()
        {
            return _conn.Table<Pedagio>().ToListAsync();
        }

        public Task<List<Viagem>> GetAllRows()
        {
            return _conn.Table<Viagem>().ToListAsync();
        }

        // Aqui é o método que fará a exclusão do registro na tabela pelo id.
        public Task<int> Delete(int id)
        {
            return _conn.Table<Pedagio>().DeleteAsync(i => i.Id == id);
        }


        // Aqui é o método que fará a pesquisa na tabela com base em uma string.

    }
}
