using calcivarS5B.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calcivarS5B.Utils
{
    public class PersonRepository
    {
        string dbPath;
        public string status { get; set; }

        private SQLiteConnection conn;
        public PersonRepository(string path)
        {
            dbPath = path;
        }

        public void Init()
        {
            if (conn is not null) return;
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Persona>();
        }
        public void AddNewPerson(string name)
        {
            try
            {
                int result = 0;
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es reuqerido");

                Persona persona = new Persona();
                persona.Name = name;

                result = conn.Insert(persona);

                status = string.Format("Dato insertado");

            }
            catch (Exception ex) {
                status = string.Format("Error al ingresa");
            }
        }
        public List<Persona> GetAllPeople()
        {
            List<Persona> personas = new List<Persona>();
            try
            {
                Init();
                personas =  conn.Table<Persona>().ToList();
            }
            catch (Exception)
            {
                status = string.Format("Error al obtener los datos");
            }
            return personas;
        }
        public Persona GetPersonById(int id)
        {
            try
            {
                Init();
                return conn.Find<Persona>(id);
            }
            catch (Exception)
            {
                status = "Error al obtener la persona";
                return null;
            }
        }
        public void UpdatePerson(int id, string name)
        {
            try
            {
                Init();
                var persona = conn.Find<Persona>(id);
                if (persona == null)
                {
                    status = "Persona no encontrada";
                    return;
                }

                persona.Name = name;
                int result = conn.Update(persona);
                status = "Dato actualizado";
            }
            catch (Exception)
            {
                status = "Error al actualizar el dato";
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();
                var persona = conn.Find<Persona>(id);
                if (persona == null)
                {
                    status = "Persona no encontrada";
                    return;
                }

                int result = conn.Delete(persona);
                status = "Dato eliminado";
            }
            catch (Exception)
            {
                status = "Error al eliminar el dato";
            }
        }
    }
}
