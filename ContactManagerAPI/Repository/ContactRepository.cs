using ContactManagerAPI.Context;
using ContactManagerAPI.Models;
using ContactManagerAPI.Repository.Interfaces;
using Npgsql;
using System.Data;

namespace ContactManagerAPI.Repository
{
    public class ContactRepository : IContactRepository
    {
        public ContactRepository() { }
        public async Task<Contact> CreateAsync(Contact contact)
        {
            NpgsqlConnection conn = null;

            try
            {
                conn = Connection.getInstance().CreateConnection();
                NpgsqlCommand cmd = new NpgsqlCommand(
                    "INSERT INTO public.contacts (name, phone_number) VALUES (@Name, @PhoneNumber) RETURNING id", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", contact.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);

                await conn.OpenAsync();
                contact.Id = (int)await cmd.ExecuteScalarAsync(); 

                return contact;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    await conn.CloseAsync();
                }
            }
        }


        public async Task DeleteAsync(int id)
        {
            NpgsqlConnection conn = null;

            try
            {
                conn = Connection.getInstance().CreateConnection();
                NpgsqlCommand cmd = new NpgsqlCommand(
                    "DELETE FROM public.contacts WHERE id = @Id", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    await conn.CloseAsync();
                }
            }
        }


        public async Task<List<Contact>> GetAsync()
        {
            NpgsqlDataReader result;
            DataTable table = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection();
            List<Contact> contacts = new List<Contact>();
            try
            {
                conn = Connection.getInstance().CreateConnection();
                NpgsqlCommand cmd = new NpgsqlCommand("Select * from public.contacts", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                result = cmd.ExecuteReader();
                table.Load(result);
                contacts = table.AsEnumerable()
                .Select(row => new Contact
                {
                    Id = row.Field<int>("id"),
                    Name = row.Field<string>("name"),
                    PhoneNumber = row.Field<string>("phone_number")
                })
                .ToList();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State.Equals(ConnectionState.Open)) conn.Close();
            }
            return contacts;
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            NpgsqlDataReader result;
            DataTable table = new DataTable();
            NpgsqlConnection conn = null;
            Contact contact = null;

            try
            {
                conn = Connection.getInstance().CreateConnection();
                NpgsqlCommand cmd = new NpgsqlCommand("Select * from public.contacts WHERE id = @Id", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);

                await conn.OpenAsync();
                result = await cmd.ExecuteReaderAsync();
                table.Load(result);


                contact = table.AsEnumerable()
                               .Select(row => new Contact
                               {
                                   Id = row.Field<int>("id"),
                                   Name = row.Field<string>("name"),
                                   PhoneNumber = row.Field<string>("phone_number")
                               })
                               .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    await conn.CloseAsync();
                }
            }

            return contact;
        }


        public async Task<Contact> UpdateAsync(Contact contact)
        {
            NpgsqlConnection conn = null;

            try
            {
                conn = Connection.getInstance().CreateConnection();
                NpgsqlCommand cmd = new NpgsqlCommand(
                    "UPDATE public.contacts SET name = @Name, phone_number = @PhoneNumber WHERE id = @Id", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", contact.Id);
                cmd.Parameters.AddWithValue("@Name", contact.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return contact;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    await conn.CloseAsync();
                }
            }
        }

    }
}
