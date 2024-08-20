using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areatriangulo4700154
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Triangulo>().Wait();
        }

        public async Task<List<Triangulo>> GetTriangulos()
        {
            return await _connection.Table<Triangulo>().ToListAsync();
        }

        public async Task<Triangulo> GetById(int id)
        {
            return await _connection.Table<Triangulo>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Triangulo triangulo)
        {
            triangulo.Area = CalcularArea(triangulo.Lado1, triangulo.Lado2, triangulo.Lado3);
            await _connection.InsertAsync(triangulo);
        }

        public async Task Update(Triangulo triangulo)
        {
            triangulo.Area = CalcularArea(triangulo.Lado1, triangulo.Lado2, triangulo.Lado3);
            await _connection.UpdateAsync(triangulo);
        }

        public async Task Delete(Triangulo triangulo)
        {
            await _connection.DeleteAsync(triangulo);
        }

        private double CalcularArea(double lado1, double lado2, double lado3)
        {
            double s = (lado1 + lado2 + lado3) / 2;
            return Math.Sqrt(s * (s - lado1) * (s - lado2) * (s - lado3));
        }
    }
}
