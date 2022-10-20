
using AppCustoViagemMelhorado.Model;


using SQLite;

using System.Collections.Generic;

using System.Threading.Tasks;

namespace AppCustoViagemMelhorado.Helper
{
 
    public class SQLiteDatabaseHelper
    {
       
        readonly SQLiteAsyncConnection _conn;


        
        public SQLiteDatabaseHelper(string path)
        {
          
            _conn = new SQLiteAsyncConnection(path);

            _conn.CreateTableAsync<Pedagio>().Wait();
        }


      
        public Task<int> Insert(Pedagio p)
        {
            return _conn.InsertAsync(p);
        }


     
        public Task<List<Pedagio>> Update(Pedagio p)
        {
            string sql = "UPDATE Pedagio SET Localizacao=?, Valor=? WHERE id= ? ";
            return _conn.QueryAsync<Pedagio>(sql, p.Localizacao, p.Valor, p.Id);
        }


    
        public Task<List<Pedagio>> GetAll()
        {
            return _conn.Table<Pedagio>().ToListAsync();
        }


        public Task<int> Delete(int id)
        {

            return _conn.Table<Pedagio>().DeleteAsync(i => i.Id == id);
        }



    }
}